using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    public static ExplosionManager Instance { get; private set; }

    public List<EnemyMissile> enemyMissiles;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    //public bool CheckCollision(GameObject self, GameObject other, float bufferRange)
    //{
    //    if ((self.transform.position - other.transform.position).sqrMagnitude < bufferRange)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    public void CheckCollision(PlayerExplosion explosion)
    {
        enemyMissiles = EnemyMissilePool.Instance.ObjectList;

        for (int i = 0; i < enemyMissiles.Count; i++)
        {
            EnemyMissile missile = enemyMissiles[i];

            if ((explosion.transform.position - missile.transform.position).sqrMagnitude < (3.0f/*explosion.ExplosionRadius * explosion.ExplosionRadius*/))
            {
                missile.ExplodeAndReturnToPool();
                return;
            }
        }
    }
}


//bool CheckCollision()
//{
//    for (int i = 0; i < playerExplosions.Count; i++)
//    {
//        PlayerExplosion explosion = playerExplosions[i];

//        for (int j = 0; j < enemyMissiles.Count; j++)
//        {
//            EnemyMissile missile = enemyMissiles[j];

//            if ((missile.transform.position - explosion.transform.position).sqrMagnitude < (explosion.ExplosionRadius * explosion.ExplosionRadius))
//            {
//                missileToExplode = missile;
//                return true;
//            }
//        }
//    }
//    return false;
//}


