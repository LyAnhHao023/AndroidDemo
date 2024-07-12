using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Transform target;

    EnenmyStats enemyStats;

    public bool canMove = true;

    public void SetData(Transform Target, EnenmyStats EnemyStats )
    {
        this.target = Target;
        this.enemyStats = EnemyStats;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * enemyStats.speed * Time.deltaTime;
        }
        
    }
}
