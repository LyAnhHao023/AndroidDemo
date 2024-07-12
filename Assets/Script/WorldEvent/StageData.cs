using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StageEvent
{
    public float Time;
    public EnemyData enemySpawData;
    public bool isBoss;
    public int countEnemy;

    public bool isRepeatedEvent;
    public float repeatedEverySeconds;
    public int countRepeated;

    public float StopTime = 0;
}

[CreateAssetMenu]
public class StageData : ScriptableObject
{
    public List<StageEvent> stageEvents;
}
