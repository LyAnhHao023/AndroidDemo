using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoOfWindMagic : MonoBehaviour
{
    [SerializeField] Transform player;
    CharacterStats characterStats;
    WeaponStats weaponStats;

    Animator animator;

    [SerializeField]
    float timeDeActive = 3f;

    float timer;
    Vector3 centerPoint;
    List<int> IdEnemy=new List<int>();

    public float radius = 2f; // Bán kính của đường tròn
    public float rotationSpeed = 45f;

    bool flag=false;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GetComponentInParent<CharacterInfo_1>().transform;
    }
    private void OnEnable()
    {
        transform.position = RandomPos();
        centerPoint = transform.position;
        StartCoroutine(DeActive());
        flag = Random.value < 0.5f;
    }

    public void SetData(CharacterStats characterStats, WeaponStats weaponStats)
    {
        this.characterStats = characterStats;
        this.weaponStats = weaponStats;
    }

    private void Update()
    {
        timer-= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0.5f;
            IdEnemy.Clear();
        }

        MoveAlongCircle();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
        if (enemy != null && !IdEnemy.Contains(collision.GetInstanceID()))
        {
            IdEnemy.Add(collision.GetInstanceID());
            bool isCrit = Random.value * 100 < characterStats.crit;
            float dmg = isCrit ?
                (weaponStats.dmg + characterStats.strenght) * characterStats.critDmg : (weaponStats.dmg + characterStats.strenght);

            PostDmg((int)dmg, enemy.transform.position, isCrit);

            bool isDead = enemy.EnemyTakeDmg((int)dmg);
            if (isDead)
            {
                GetComponentInParent<CharacterInfo_1>().KilledMonster();
            }
        }
    }

    private void MoveAlongCircle()
    {
        // Tính toán góc xoay mới dựa trên tốc độ xoay và thời gian trôi qua
        float angle = rotationSpeed * Time.time;

        float x;
        float y;

        // Tính toán vị trí mới của vật thể trên đường tròn
        if (flag)
        {
            x = centerPoint.x + Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            y = centerPoint.y + Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
        }
        else
        {
            y = centerPoint.x + Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            x = centerPoint.y + Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
        }

        // Cập nhật vị trí của vật thể
        transform.position = new Vector2(x, y);
    }

    private void PostDmg(int dmg, Vector3 TargetPosition, bool isCrit)
    {
        MessengerSystem.instance.DmgPopUp(dmg.ToString(), TargetPosition, isCrit);
    }


    private Vector3 RandomPos()
    {
        int x = Random.Range(0, 12);
        int y = Random.Range(0, 5);

        return new Vector3(x + player.position.x, y + player.position.y, 0);
    }

    private IEnumerator DeActive()
    {
        yield return new WaitForSeconds(timeDeActive);
        animator.SetBool("Off", true);
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }
}
