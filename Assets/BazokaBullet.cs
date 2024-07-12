using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazokaBullet : MonoBehaviour
{
    [SerializeField]
    float timeAutoDestroy = 10f;
    [SerializeField] GameObject ExplodePrefab;
    private Vector3 baseSizeBullet;
    public void SetDmg(int dmg, bool isCrit)
    {
        ExplodePrefab.GetComponent<BazokaBulletExplode>().SetDmg(dmg, isCrit);
    }

    public void BuffSizeBulletByPersent(float persent)
    {
        transform.localScale += new Vector3(baseSizeBullet.x * persent, baseSizeBullet.y * persent, baseSizeBullet.y * persent);
    }


    private void Awake()
    {
        Destroy(gameObject, timeAutoDestroy);
        baseSizeBullet=transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            GetComponent<Renderer>().enabled = false;
            ExplodePrefab.SetActive(true);
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
            Destroy(gameObject, 0.4f);
        }

    }
}
