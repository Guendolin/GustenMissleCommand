using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //PlayerMissile playerMissile;
    public PlayerMissileLauncher playerMissileLauncher;

    [SerializeField]
    private Camera camera;

    //Set amount of bases
    //Set amount of missile launchers
    //Set pos and rot of missile launchers
    //Score keeping?


    void Start()
    {
        ////TODO remove get component and do the usual serialize stuff
        //playerMissileLauncher = GetComponentInChildren<PlayerMissileLauncher>();
        //camera = Camera.main;
    }
    void Update()
    {
        OnMouseDown();
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition;

            mousePosition = Input.mousePosition;
            mousePosition = camera.ScreenToWorldPoint(mousePosition);

            playerMissileLauncher.FireMissile(mousePosition, playerMissileLauncher.transform.position);
        }
    }
}
