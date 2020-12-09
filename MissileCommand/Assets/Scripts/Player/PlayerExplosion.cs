using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosion : MonoBehaviour
{
    public float ExplosionRadius;

    [SerializeField]
    private float minExpolsionRadius = 0.5f;

    [SerializeField]
    private float maxExpolsionRadius = 1.0f;

    [SerializeField]
    private float explosionTime = 0.5f;

    private float timer;

    private void OnEnable()
    {
        GameManager.Instance.audioManager.PlayWithRandomPitch("Explosion0", 0.8f, 1.2f);
        ExplosionRadius = minExpolsionRadius;
        GameManager.Instance.explosionManager.CheckCollisionLoop(this);
    }

    void Update()
    {
        ExplosionRadius = Mathf.Lerp(minExpolsionRadius, maxExpolsionRadius, timer);
        
        timer += Time.deltaTime / explosionTime;
        if (timer >= 1f)
        {
            timer = 0f;
            ExplosionRadius = minExpolsionRadius; 
            PlayerExplosionPool.Instance.ReturnToPool(this);
        }
    }
}

