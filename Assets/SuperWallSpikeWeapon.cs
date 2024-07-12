using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperWallSpikeWeapon : WeaponBase
{
    [SerializeField]
    List<GameObject> lst_wallSpike = new List<GameObject>();

    int count = 0;

    int numWallActive = 3;

    int coutWallActive = 0;

    CharacterStats characterStats;

    playerMove playerMove;

    int wallTravel = 4;

    AudioManager audioManager;

    private void Start()
    {
        SetCharacterStats();
        BuffWeaponSizeByPersent(GetComponentInParent<CharacterInfo_1>().weaponSize);
        playerMove = GetComponentInParent<playerMove>();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public override void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = weaponStats.timeAttack;
            coutWallActive += numWallActive;
        }

        if (coutWallActive > 0)
        {
            coutWallActive--;

            if (coutWallActive % 3 == 0)
            {
                ActiveWallSpikeBehind();
            }
            else
            {
                ActiveWallSpikeRandomPos();
            }

            count++;

            if (count >= lst_wallSpike.Count)
            {
                count = 0;
            }

        }
    }

    private void ActiveWallSpikeRandomPos()
    {
        audioManager.PlaySFX(audioManager.WallSpike);

        Vector3 positionAttack = RandomPositionWall();

        Vector2 lookDir = positionAttack - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        lst_wallSpike[count].SetActive(true);
        lst_wallSpike[count].GetComponent<SuperWallSpikeChildren>().SetPositionAttack(positionAttack, transform.position, rotation);
    }

    private void ActiveWallSpikeBehind()
    {
        audioManager.PlaySFX(audioManager.WallSpike);

        Vector3 positionAttack = transform.position + new Vector3(playerMove.scaleX == 1 ? -wallTravel : wallTravel, Random.Range(-1, 2), 0);

        Vector2 lookDir = positionAttack - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        lst_wallSpike[count].SetActive(true);
        lst_wallSpike[count].GetComponent<SuperWallSpikeChildren>().SetPositionAttack(positionAttack, transform.position, rotation);
    }

    private Vector3 RandomPositionWall()
    {
        if (Random.value < 0.5f)
        {
            return (transform.position + new Vector3(Random.value < 0.5f ? -wallTravel : wallTravel, Random.value < 0.5f ? Random.Range(-wallTravel, 0) : Random.Range(1, wallTravel + 1), 0));
        }
        else
        {
            return (transform.position + new Vector3(Random.value < 0.5f ? Random.Range(-wallTravel, 0) : Random.Range(1, wallTravel + 1), Random.value < 0.5f ? -wallTravel : wallTravel, 0));
        }

    }

    public override void Attack()
    {
        return;
    }

    public override void LevelUp()
    {
        return;
    }

    public override void SetCharacterStats()
    {
        characterStats = GetComponentInParent<CharacterInfo_1>().characterStats;
        for (int i = 0; i < lst_wallSpike.Count; i++)
        {
            lst_wallSpike[i].GetComponent<SuperWallSpikeChildren>().SetData(characterStats, weaponStats);
        }
    }
}
