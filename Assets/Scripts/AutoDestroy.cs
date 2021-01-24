using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class AutoDestroy : MonoBehaviour
{
    public float destroyDelay =1f;

    // Start is called before the first frame update
    void Start()
    {
        LeanPool.Despawn(gameObject, destroyDelay);
    }

   
}
