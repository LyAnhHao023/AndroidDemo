using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemiesSpawGroup
{
    public EnemyData enemyData;
    public int count;
    public bool isBoss;

    public float repeatedTimer;
    public float timeBetweenRepeated;
    public int repeatedCount;

    public float timeStop;

    public EnemiesSpawGroup(EnemyData enemyData, int count, bool isBoss)
    {
        this.enemyData = enemyData;
        this.count = count;
        this.isBoss = isBoss;
    }

    public void SetRepeatedSpaw(float timeBetweenRepeated,int repeatedCount, float timeStop)
    {
        this.timeBetweenRepeated = timeBetweenRepeated;
        this.repeatedCount = repeatedCount;
        repeatedTimer = timeBetweenRepeated;
        this.timeStop = timeStop;
    }
}

[Serializable]
public class TimeStamp
{
    public int state;
    public float time;
}

public class SpawEnemy : MonoBehaviour
{

    [SerializeField] Vector2 spawArea;
    [SerializeField] GameObject player;
    [SerializeField]
    GameObject ParentDropItem;

    [SerializeField] float timeToBuffEnemy = 30f;
    [SerializeField]
    Timer stageTime;

    [SerializeField]
    public List<TimeStamp> timeStamp;

    float timer;
    float statsBuffByTime=1;
    float buffPercent = 0.2f;

    List<EnemiesSpawGroup> lst_EnemiesSpawGroups;
    List<EnemiesSpawGroup> lst_ReSpawEnemy;

    EnenmyStats enenmyStatsBuff;

    public float reduceTimeSpaw=0;

    private void Start()
    {
        timer = timeToBuffEnemy;

        if (StaticData.LevelType == 2)
        {
            buffPercent = 0.4f;
            timeToBuffEnemy = 40f;
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            statsBuffByTime += buffPercent;
            timer = timeToBuffEnemy;
        }

        ProcessSpaw();
        ProcessRepeatedSpawGroup();
    }

    public void PlusOrMinusEnemyStats(int hp, int dmg, int speed, float timeAttack, float chanceDropCoin, float chanceDropHeath, float chanceDropExp)
    {
        if (enenmyStatsBuff == null) { enenmyStatsBuff=new EnenmyStats(hp, dmg, speed, timeAttack, chanceDropCoin, chanceDropHeath, chanceDropExp); return; }
        enenmyStatsBuff.hp += hp;
        enenmyStatsBuff.dmg += dmg;
        enenmyStatsBuff.speed += speed;
        enenmyStatsBuff.timeAttack += timeAttack;
        enenmyStatsBuff.chanceDropHeath += chanceDropHeath;
        enenmyStatsBuff.chanceDropCoin += chanceDropCoin;
        enenmyStatsBuff.chanceDropExp += chanceDropExp;

    }

    private void ProcessRepeatedSpawGroup()
    {
        if (lst_ReSpawEnemy != null && lst_ReSpawEnemy.Count > 0)
        {
            for (int i = lst_ReSpawEnemy.Count - 1; i >= 0; i--)
            {
                lst_ReSpawEnemy[i].repeatedTimer -= Time.deltaTime;
                if (lst_ReSpawEnemy[i].repeatedTimer <= 0)
                {
                    lst_ReSpawEnemy[i].repeatedTimer = lst_ReSpawEnemy[i].timeBetweenRepeated - reduceTimeSpaw;
                    if(lst_ReSpawEnemy[i].repeatedTimer <= 1)
                    {
                        lst_ReSpawEnemy[i].repeatedTimer = 1;
                        lst_ReSpawEnemy[i].count += Random.Range(0,3);
                    }
                    AddGroupToSpaw(lst_ReSpawEnemy[i].enemyData, lst_ReSpawEnemy[i].count, lst_ReSpawEnemy[i].isBoss);
                    lst_ReSpawEnemy[i].repeatedCount--;
                    if (lst_ReSpawEnemy[i].repeatedCount <= 0 && (lst_ReSpawEnemy[i].timeStop <= stageTime.elapsedTime || lst_ReSpawEnemy[i].timeStop == 0))
                    {
                        lst_ReSpawEnemy.RemoveAt(i);
                    }
                }
            }
        }
    }

    private void ProcessSpaw()
    {
        if (lst_EnemiesSpawGroups != null&& lst_EnemiesSpawGroups.Count > 0)
        {
            CreateNewEnemy(lst_EnemiesSpawGroups[0].enemyData, lst_EnemiesSpawGroups[0].isBoss);
            lst_EnemiesSpawGroups[0].count -= 1;
            if (lst_EnemiesSpawGroups[0].count <= 0)
            {
                lst_EnemiesSpawGroups.RemoveAt(0);
            }
        }

    }

    public void AddGroupToSpaw(EnemyData enemyData, int count,bool isBoss)
    {
        EnemiesSpawGroup enemiesSpawGroup = new EnemiesSpawGroup(enemyData,count,isBoss);

        if (lst_EnemiesSpawGroups == null) { lst_EnemiesSpawGroups=new List<EnemiesSpawGroup>(); }

        lst_EnemiesSpawGroups.Add(enemiesSpawGroup);
    }

    public void CreateNewEnemy(EnemyData enemy, bool boss)
    {
        Vector3 position= CreateRandomPosition();
        position += player.transform.position;
        while (IsObstacle(position))
        {
            position = CreateRandomPosition();
            position += player.transform.position;
        }
        GameObject createEnemy=Instantiate(enemy.EnemyBasePrefab, transform);

        createEnemy.transform.position = position;

        EnemyBase newEnemyBase= createEnemy.GetComponent<EnemyBase>();
        newEnemyBase.SetData(enemy);
        newEnemyBase.SetTarget(player);
        newEnemyBase.SetParentDropItem(ParentDropItem);
        newEnemyBase.StatsPlus(enenmyStatsBuff);
        newEnemyBase.StatsBuffByTime(statsBuffByTime);
    }

    //Tạo vị trí ngẫu nhiên
    private Vector3 CreateRandomPosition()
    {
        Vector3 position = new Vector3(0, 0, 0);
        float value = Random.value > 0.5f? -1:1;
        if (Random.value > 0.5)
        {
            position.x = Random.Range(-spawArea.x, spawArea.x);
            position.y = spawArea.y*value;
        }
        else
        {
            position.y = Random.Range(-spawArea.y, spawArea.y);
            position.x = spawArea.x * value;
        }

        return position;
    }

    //Kiểm tra xem tại vị trí có vật cản hay không
    public bool IsObstacle(Vector3 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position,new Vector3(0,0,1));
        if(hit.collider != null)
        {
            return hit.collider.gameObject.layer == 6;
        }
        else
        {
            return false;
        }
    }

    public void AddRepeatedSpaw(StageEvent stageEvent)
    {
        EnemiesSpawGroup repeatedSpawGroup=new EnemiesSpawGroup(stageEvent.enemySpawData,stageEvent.countEnemy,stageEvent.isBoss);
        repeatedSpawGroup.SetRepeatedSpaw(stageEvent.repeatedEverySeconds, stageEvent.countRepeated, stageEvent.StopTime);

        if (lst_ReSpawEnemy == null)
        {
            lst_ReSpawEnemy = new List<EnemiesSpawGroup>();
        }

        lst_ReSpawEnemy.Add(repeatedSpawGroup);
    }
}
