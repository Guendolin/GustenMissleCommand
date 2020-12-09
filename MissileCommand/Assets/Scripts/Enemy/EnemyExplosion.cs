using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour
{
    //Make this expand as well to chain
    [SerializeField]
    private float explosionTime = 1f;

    private float timer;
    private void OnEnable()
    {
        GameManager.Instance.audioManager.PlayWithRandomPitch("Explosion1", 0.8f, 1.2f);
    }

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
