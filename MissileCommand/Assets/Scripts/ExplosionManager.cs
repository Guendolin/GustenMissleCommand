using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    public List<EnemyMissile> enemyMissiles;

    public bool CheckCollision(GameObject self, GameObject other, float bufferRange)
    {

        if ((self.transform.position - other.transform.position).sqrMagnitude < bufferRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CheckCollisionLoop(PlayerExplosion explosion)
    {
        enemyMissiles = EnemyMissilePool.Instance.ObjectList;

        for (int i = 0; i < enemyMissiles.Count; i++)
        {
            EnemyMissile missile = enemyMissiles[i];
            
            if (CheckCollision(explosion.gameObject, missile.gameObject, 1.0f))
            {
                missile.ExplodeAndReturnToPool();
            }
        }
    }
}
