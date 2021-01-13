using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    [SerializeField]
    private GameObject targetMarker;

    public PlayerBase playerBase;
    public PlayerBase[] playerBases;

    public int MissileFiringMode = 0;
    bool isPlayingSound = false;


    void Update()
    {
        //do this with enums instead?
        FiringMode();
        if (MissileFiringMode == 0)
        {
            FireAndForgetMissile();
        }
        if (MissileFiringMode == 1)
        {
            HoldAndReleaseMissile();
        }
        if (MissileFiringMode == 2)
        {
            FireAndReleaseMissile();
        }
    }

    void FireAndForgetMissile()
    {
        if (GameManager.Instance.isWaveActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePositionScreen = Input.mousePosition;
                Vector3 mousePositionWorld = camera.ScreenToWorldPoint(mousePositionScreen);

                int currentBase = -1;

                float closetDistance = float.MaxValue;

                for (int i = 0; i < playerBases.Length; i++)
                {
                    playerBase = playerBases[i];
                    float distanceToPosition = Mathf.Abs(playerBase.transform.position.x - mousePositionWorld.x);

                    if (playerBase.HasMissiles() && distanceToPosition < closetDistance)
                    {
                        currentBase = i;
                        closetDistance = distanceToPosition;
                    }
                }
                if (currentBase >= 0 && playerBases[currentBase].HasMissiles())
                {
                    FireMissile(mousePositionWorld, playerBases[currentBase].playerMissileLauncher[playerBases[currentBase].currentLauncher].transform.position);
                    playerBases[currentBase].playerMissileLauncher[playerBases[currentBase].currentLauncher].gameObject.SetActive(false);
                    playerBases[currentBase].currentLauncher--;
                }
            }
        }

    }
    void HoldAndReleaseMissile()
    {
        if (GameManager.Instance.isWaveActive)
        {
            Vector3 mousePositionScreen = Input.mousePosition;
            Vector3 mousePositionWorld = camera.ScreenToWorldPoint(mousePositionScreen);

            if (Input.GetMouseButton(0))
            {
                targetMarker.SetActive(true);
                targetMarker.transform.position = (Vector2)mousePositionWorld;

                if (!isPlayingSound)
                {
                    GameManager.Instance.audioManager.PlayWithRandomPitch("Radar", 1f, 1.5f);
                    isPlayingSound = true;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isPlayingSound = false;
                targetMarker.SetActive(false);
                GameManager.Instance.audioManager.Stop("Radar");


                int currentBase = -1;

                float closetDistance = float.MaxValue;

                for (int i = 0; i < playerBases.Length; i++)
                {
                    playerBase = playerBases[i];
                    float distanceToPosition = Mathf.Abs(playerBase.transform.position.x - mousePositionWorld.x);

                    if (playerBase.HasMissiles() && distanceToPosition < closetDistance)
                    {
                        currentBase = i;
                        closetDistance = distanceToPosition;
                    }
                }
                if (currentBase >= 0 && playerBases[currentBase].HasMissiles())
                {
                    FireMissile(mousePositionWorld, playerBases[currentBase].playerMissileLauncher[playerBases[currentBase].currentLauncher].transform.position);
                    playerBases[currentBase].playerMissileLauncher[playerBases[currentBase].currentLauncher].gameObject.SetActive(false);
                    playerBases[currentBase].currentLauncher--;
                }
            }
        }
    }
    void FireAndReleaseMissile()
    {
        if (GameManager.Instance.isWaveActive)
        {
            Vector3 mousePositionScreen = Input.mousePosition;
            Vector3 mousePositionWorld = camera.ScreenToWorldPoint(mousePositionScreen);

            if (Input.GetMouseButton(0))
            {
                targetMarker.SetActive(true);
                targetMarker.transform.position = (Vector2)mousePositionWorld;

                if (!isPlayingSound)
                {
                    GameManager.Instance.audioManager.PlayWithRandomPitch("Radar", 1f, 1.5f);
                    isPlayingSound = true;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isPlayingSound = false;
                targetMarker.SetActive(false);
                GameManager.Instance.audioManager.Stop("Radar");


                int currentBase = -1;

                float closetDistance = float.MaxValue;

                for (int i = 0; i < playerBases.Length; i++)
                {
                    playerBase = playerBases[i];
                    float distanceToPosition = Mathf.Abs(playerBase.transform.position.x - mousePositionWorld.x);

                    if (playerBase.HasMissiles() && distanceToPosition < closetDistance)
                    {
                        currentBase = i;
                        closetDistance = distanceToPosition;
                    }
                }
                if (currentBase >= 0 && playerBases[currentBase].HasMissiles())
                {
                    FireMissile(mousePositionWorld, playerBases[currentBase].playerMissileLauncher[playerBases[currentBase].currentLauncher].transform.position);
                    playerBases[currentBase].playerMissileLauncher[playerBases[currentBase].currentLauncher].gameObject.SetActive(false);
                    playerBases[currentBase].LaunchSmoke(playerBases[currentBase].playerMissileLauncher[playerBases[currentBase].currentLauncher].transform.position);
                    playerBases[currentBase].currentLauncher--;
                }
            }
        }
    }

    void FiringMode()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (MissileFiringMode <= 1)
            {
                MissileFiringMode++;
            }
            else
            {
                MissileFiringMode = 0;
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