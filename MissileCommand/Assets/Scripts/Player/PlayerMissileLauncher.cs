using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject missileSprite;

    public void FireMissile(Vector2 target, Vector2 origin)
    {
        var playerMissile = PlayerMissilePool.Instance.Get();

        playerMissile.FireMissileInternal(target, origin);
        playerMissile.gameObject.SetActive(true);

        missileSprite.SetActive(false);
    }
}
