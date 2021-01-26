using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingZombie : MonoBehaviour
{
    public float radius = 10;

    IAstarAI ai;
    void Start()
    {

        ai = GetComponent<IAstarAI>();
    }

    Vector3 PickRandomPoint()
    {
        var point = Random.insideUnitSphere * radius;

        point.y = 0;
        point += ai.position;
        return point;
    }

    void Update()
    {
        if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
        {
            ai.destination = PickRandomPoint();
            ai.SearchPath();
        }
    }
}
