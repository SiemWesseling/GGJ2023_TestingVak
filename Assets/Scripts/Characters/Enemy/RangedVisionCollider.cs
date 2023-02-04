using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedVisionCollider : MonoBehaviour
{
    private EnemyRangedAttack enemyRangedAttack;
    
    private void Start()
    {
        if (enemyRangedAttack == null)
        {
            enemyRangedAttack = transform.parent.GetComponent<EnemyRangedAttack>();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            enemyRangedAttack.onPlayerSpotted.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            enemyRangedAttack.onPlayerLost.Invoke();
        }
    }
}
