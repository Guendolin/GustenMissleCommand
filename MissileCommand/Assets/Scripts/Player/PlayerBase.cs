using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public PlayerMissileLauncher[] playerMissileLauncher = new PlayerMissileLauncher[10];

    public int currentLauncher;

    public bool HasMissiles() => currentLauncher >= 0 && gameObject.activeInHierarchy;

    void Start()
    {
        ResetMissileLaunchers();
    }

    public void LaunchSmoke(Vector2 launcherPosition)
    {
        var playerLaunchSmoke = PlayerLaunchSmokePool.Instance.Get();

        playerLaunchSmoke.transform.position = launcherPosition;
        playerLaunchSmoke.gameObject.SetActive(true);
    }

    public void ResetMissileLaunchers()
    {
        currentLauncher = playerMissileLauncher.Length - 1;
        for (int i = 0; i < playerMissileLauncher.Length; i++)
        {
            playerMissileLauncher[i].gameObject.SetActive(true);
        }
    }

    public void ResetBase()
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
    }
}
