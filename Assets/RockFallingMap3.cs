using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class RockFallingMap3 : MonoBehaviour
{

    [SerializeField]
    GameObject player;
    [SerializeField]
    Vector3 AreaFalling;
    [SerializeField]
    int dmg;
    [SerializeField]
    GameObject warningZonePrefab;

    private Collider2D colliderOJ;

    bool isSkillBoss=false;

    Vector3 currentPos;

    private void Awake()
    {
        colliderOJ = GetComponent<Collider2D>();
    }

    public void SetPlayerTarget(GameObject player)
    {
        this.player = player;
        isSkillBoss=true;
    }

    private void OnEnable()
    {
        StartCoroutine(DeActive(1.30f, warningZonePrefab));
        StartCoroutine(DeActive(1.65f, gameObject));
        colliderOJ.enabled = false;
        warningZonePrefab.SetActive(true);
        currentPos = RandomPosition()+player.transform.position;
        gameObject.transform.position = currentPos;
    }

    private void Update()
    {
        gameObject.transform.position = currentPos;
    }

    private Vector3 RandomPosition()
    {
        Vector3 position= Vector3.zero;
        do
        {
            position.x = Random.Range(-AreaFalling.x, AreaFalling.x);
            position.y = Random.Range(-AreaFalling.y, AreaFalling.y);

        } while(IsObstacle(position));
        return position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterInfo_1 playerGO = collision.GetComponent<CharacterInfo_1>();
        EnemyBase enemy = collision.GetComponent<EnemyBase>();
        if(playerGO != null)
        {
            playerGO.TakeDamage(dmg);
            return;
        }
        if(enemy != null && !isSkillBoss)
        {
            bool isDead = enemy.EnemyTakeDmg(dmg);
            PostDmg(dmg, transform.position, false);
            if (isDead)
            {
                player.GetComponent<CharacterInfo_1>().KilledMonster();
            }
        }
    }

    private void PostDmg(int dmg, Vector3 TargetPosition, bool isCrit)
    {
        MessengerSystem.instance.DmgPopUp(dmg.ToString(), TargetPosition, isCrit);
    }

    private IEnumerator DeActive(float time, GameObject gameObject)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
        if(gameObject== warningZonePrefab)
        {
            colliderOJ.enabled = true;
        }
    }

    public bool IsObstacle(Vector3 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, new Vector3(0, 0, 1));
        if (hit.collider != null)
        {
            return hit.collider.gameObject.layer == 6;
        }
        else
        {
            return false;
        }
    }
}
