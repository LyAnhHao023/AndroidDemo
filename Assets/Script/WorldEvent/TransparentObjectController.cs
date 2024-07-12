using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObjectController : MonoBehaviour
{
    public GameObject player;
    private Material transparentMaterial;
    [SerializeField]
    private float minAlpha = 0.5f; // Giá trị alpha tối thiểu khi đè lên người chơi

    private void Start()
    {
        transparentMaterial = gameObject.GetComponent<Renderer>().material;
    }

    private void FixedUpdate()
    {
        // Kiểm tra va chạm giữa vật thể và người chơi
        if (IsOverlapping(gameObject, player))
        {
            // Giảm dần độ không trong suốt của vật thể
            transparentMaterial.color = new Color(
                transparentMaterial.color.r,
                transparentMaterial.color.g,
                transparentMaterial.color.b,
                Mathf.Max(minAlpha, transparentMaterial.color.a - Time.deltaTime)
            );
        }
        else
        {
            if (transparentMaterial.color.a >= 1) return;
            // Trả về độ trong suốt ban đầu
            transparentMaterial.color = new Color(
                transparentMaterial.color.r,
                transparentMaterial.color.g,
                transparentMaterial.color.b,
                Mathf.Min(1f, transparentMaterial.color.a + Time.deltaTime)
            );
        }
    }

    private bool IsOverlapping(GameObject gameObject, GameObject player)
    {
        // Kiểm tra va chạm giữa hai vật thể
        var gameObjectB = gameObject.GetComponent<Renderer>().bounds;
        var playerB = player.GetComponentInChildren<Renderer>().bounds;
        return gameObjectB.Intersects(playerB);
    }
}
