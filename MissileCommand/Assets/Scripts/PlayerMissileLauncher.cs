using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject missileSprite;

    

    void Update()
    {
        
    }

    public void FireMissile(Vector2 target, Vector2 origin)
    {
        var playerMissile = PlayerMissilePool.Instance.Get();

        playerMissile.FireMissileInternal(target, origin);
        playerMissile.gameObject.SetActive(true);

        //playerMissile.transform.rotation = transform.rotation;
        //playerMissile.transform.position = transform.position;


        //playerMissile.isBeingFired = true;
        //playerMissile.targetMarker.SetActive(true);
        //playerMissile.targetMarker.transform.position = target;
       
        //playerMissile.projectile.SetActive(true);
        //playerMissile.projectile.transform.position = origin;
    }
}
