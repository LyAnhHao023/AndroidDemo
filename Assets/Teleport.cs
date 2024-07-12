using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    playerMove playerMove;
    [SerializeField]
    GameObject weapons;
    [SerializeField]
    GameObject playerAnimator;
    [SerializeField] GameObject TeleportEffect;

    private Material playerMaterial;

   
    private void Start()
    {
        playerMaterial = playerAnimator.GetComponentInChildren<Renderer>().material;
        playerMaterial.color = new Color(
                playerMaterial.color.r,
                playerMaterial.color.g,
                playerMaterial.color.b,
                -0.5f
            );
        weapons.SetActive( false );
        playerMove.isCanMove = false;
        StartCoroutine(PlayerMoveActive());

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        playerMaterial.color = new Color(
                playerMaterial.color.r,
                playerMaterial.color.g,
                playerMaterial.color.b,
                Mathf.Min(1f, playerMaterial.color.a + Time.deltaTime)
            );
    }

    private IEnumerator PlayerMoveActive()
    {
        yield return new WaitForSeconds(3f);
        playerMove.isCanMove = true;
        weapons.SetActive(true);
        gameObject.GetComponent<Animator>().enabled = false;
        yield return new WaitForSeconds(1f);
        TeleportEffect.SetActive(false);
    }
}
