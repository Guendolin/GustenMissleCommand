using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileLauncher : MonoBehaviour
{
    public void FireMissile(Vector2 target, Vector2 origin)
    {
        var enemyMissile = EnemyMissilePool.Instance.Get();

        enemyMissile.FireMissileInternal(target, origin);
        enemyMissile.gameObject.SetActive(true);
    }
}
