using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour
{
    //Make this expand as well to chain

    [SerializeField]
    private float explosionTime = 1f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= explosionTime)
        {
            timer = 0f;
            EnemyExplosionPool.Instance.ReturnToPool(this);
        }
    }
}
