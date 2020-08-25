using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileLauncher : MonoBehaviour
{
    public void FireMissile(GameObject target, GameObject origin)
    {
        var enemyMissile = EnemyMissilePool.Instance.Get();

        enemyMissile.FireMissileInternal(target, origin);
        enemyMissile.gameObject.SetActive(true);
    }
}
