using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChildrenBoom : MonoBehaviour
{
    [SerializeField]
    GameObject imgBombPrefab;
    [SerializeField]
    GameObject exploderPrefab;
    Rigidbody2D rb;

    [SerializeField]
    float timeDisActive;

    float timer;

    bool isMoove = true;

    Vector3 direction;

    AudioManager audioManager;

    private void Awake()
    {
        exploderPrefab.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        timer = timeDisActive;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos - transform.position).normalized;
        rb.AddForce(direction * 60f, ForceMode2D.Impulse);
        Invoke("StopMove", 0.2f);

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0 )
        {
            timer = timeDisActive;
            DisActivateGameObject();
        }
    }

    public void StopMove()
    {
        rb.velocity = Vector3.zero;
        gameObject.layer = 3;
        isMoove = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            audioManager.PlaySFX(audioManager.Bomb);
            GetComponent<Collider2D>().enabled = false;

            imgBombPrefab.SetActive(false);
            exploderPrefab.SetActive(true);
        }else if (collision.gameObject.layer==3&& isMoove)
        {//layer cua Bomb
            Rigidbody2D rbcol=collision.GetComponent<Rigidbody2D>();
            rbcol.AddForce(direction * 30,ForceMode2D.Impulse);
        }else if (collision.gameObject.layer == 6)
        {//layer cua obstacle
            rb.velocity=Vector3.zero;
        }
    }

    private void DisActivateGameObject()
    {
        imgBombPrefab.SetActive(false);
        exploderPrefab.SetActive(true);
        Destroy(gameObject,0.4f);
    }
}
