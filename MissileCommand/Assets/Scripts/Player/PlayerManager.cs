﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField]
    private Camera camera;

    public PlayerMissileLauncher[] playerMissileLauncher = new PlayerMissileLauncher[10];

    private int currentLauncher;

    //Score keeping?


    void Start()
    {
        currentLauncher = playerMissileLauncher.Length - 1;
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

            if (currentLauncher >= 0)
            {
                FireMissile(mousePosition, playerMissileLauncher[currentLauncher].transform.position);
                playerMissileLauncher[currentLauncher].gameObject.SetActive(false);
                currentLauncher--;
            }
        }
    }
    public void FireMissile(Vector2 target, Vector2 origin)
    {
        var playerMissile = PlayerMissilePool.Instance.Get();

        playerMissile.FireMissileInternal(target, origin);
        playerMissile.gameObject.SetActive(true);
    }
}
