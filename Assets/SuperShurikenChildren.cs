using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperShurikenChildren : MonoBehaviour
{
    [SerializeField] Transform player;
    CharacterStats characterStats;
    WeaponStats weaponStats;

    float timer;
    List<int> IdEnemy = new List<int>();

    public float radius = 2f; // Bán kính của đường tròn
    public float rotationSpeed = 45f;

    int angleRos=0;

    [SerializeField]
    bool styleMove=false;

    AudioManager audioManager;

    private void Awake()
    {
        player = GetComponentInParent<CharacterInfo_1>().transform;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void SetData(CharacterStats characterStats, WeaponStats weaponStats)
    {
        this.characterStats = characterStats;
        this.weaponStats = weaponStats;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
        if (enemy != null && !IdEnemy.Contains(collision.GetInstanceID()))
        {
            audioManager.PlaySFX(audioManager.Shuriken);

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

    void Update()
    {
        angleRos += 5;
        if(angleRos > 360)
        {
            angleRos = 0;
        }
        transform.rotation=Quaternion.Euler(0,0, angleRos);
        MoveAlongCircle();

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0.5f;
            IdEnemy.Clear();
        }

    }

    private void PostDmg(int dmg, Vector3 TargetPosition, bool isCrit)
    {
        MessengerSystem.instance.DmgPopUp(dmg.ToString(), TargetPosition, isCrit);
    }

    private void MoveAlongCircle()
    {
        // Tính toán góc xoay mới dựa trên tốc độ xoay và thời gian trôi qua
        float angle = rotationSpeed * Time.time;

        float x;
        float y;

        // Tính toán vị trí mới của vật thể trên đường tròn
        if (styleMove)
        {
            x = player.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            y = player.position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
        }
        else
        {
            x = player.position.x + Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            y = player.position.y + Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        }
        // Cập nhật vị trí của vật thể
        transform.position = new Vector2(x, y);

    }
}
