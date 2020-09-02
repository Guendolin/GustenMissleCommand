using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public PlayerMissileLauncher[] playerMissileLauncher = new PlayerMissileLauncher[10];

    public int currentLauncher;

    public bool HasMissiles() => currentLauncher >= 0;

    void Start()
    {
        currentLauncher = playerMissileLauncher.Length - 1;
    }
}
