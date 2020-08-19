using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosion : MonoBehaviour
{
    public float ExplosionRadius = 0f;

    [SerializeField]
    private float minExpolsionRadius = 0.05f;

    [SerializeField]
    private float maxExpolsionRadius = 1.0f/*0.3f*/;

    [SerializeField]
    private float explosionTime = 1f;

    private float timer;

    private void OnEnable()
    {
        ExplosionManager.Instance.CheckCollision(this);
    }

    void Update()
    {
        ExplosionRadius = Mathf.Lerp(minExpolsionRadius, maxExpolsionRadius, timer);

        timer += Time.deltaTime / explosionTime;
        if (timer >= 1f)
        {
            timer = 0f;
            ExplosionRadius = 0f;
            PlayerExplosionPool.Instance.ReturnToPool(this);
        }
    }
}

