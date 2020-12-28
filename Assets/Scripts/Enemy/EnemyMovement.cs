using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    PlayerMovement playerMovement;
    Enemy enemy;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        enemy = FindObjectOfType<Enemy>();
    }
    private void Update()
    {
        EnemyRotate();
    }

    public void EnemyRotate()
    {
        if (enemy.healthEnemy > 0)
        {
            Vector3 enemyPos = transform.position;
            Vector3 playerPos = playerMovement.transform.position;
            Vector3 direction = enemyPos - playerPos;

            direction.z = 0;
            transform.up = direction;
        }
        else
        {
            return;
        }

    }
}
