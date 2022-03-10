using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : EnemyBaseController
{
    private void Start()
    {
        currentHealth = maxHealth;
        waitCounter = waitAtPoint;

        foreach(var point in patrolPoints)
        {
            point.parent = null;
        }
    }

    private void Update()
    {
        Patrolling();
    }
}
