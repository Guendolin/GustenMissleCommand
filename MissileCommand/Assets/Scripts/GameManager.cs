using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public EnemyManager enemyManager;
    public PlayerManager playerManager;
    public ExplosionManager explosionManager;
    public AudioManager audioManager;

    public GameObject[] BaseTiles = new GameObject[34];

    [SerializeField]
    private GameObject cityLayerBack;

    [SerializeField]
    private GameObject cityLayerMid;

    [SerializeField]
    private GameObject cityLayerFront;

    public PlayerCity[] playerCities;

    private PlayerBase[] playerBases;

    //rework this with specific a class instead of game object, could be empty class
    public GameObject[] Enemytargets = new GameObject[8];

    public bool isWaveActive = false;
    public int GameLevel = 1;
    public int LevelMultiplier = 1;
    public int TotalScore = 0;

    string fullTrackName = null;
    int musicTrackNumber = -1;


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
    void Start()
    {
        playerBases = playerManager.playerBases;
    }

    public void GameStart()
    {
        GameEvents.Instance.GameStartEvent();
        isWaveActive = true;
        PlayNextSong();
    }

    //think about renaming this
    public void LevelWon()
    {
        isWaveActive = false;
        LevelMultiplier = (1 + (GameLevel / 10));
        GameLevel++;
        TotalScore += CountScore();

        for (int i = 0; i < playerBases.Length; i++)
        {
            playerBases[i].ResetMissileLaunchers();
        }
        GameEvents.Instance.LevelWonEvent();
        StopCurrentSong();
        Instance.enemyManager.MissileCount = 10 * LevelMultiplier;
        Instance.enemyManager.MissileSpeed = Instance.enemyManager.MissileSpeed * LevelMultiplier;
    }

    public bool isTheGameLost()
    {
         int safeCities = playerCities.Length;

        for (int i = 0; i < playerCities.Length; i++)
        {
            if (!playerCities[i].gameObject.activeInHierarchy)
            {
                safeCities--;
            }
        }
        if (safeCities < 1)
        {
            isWaveActive = false;
            return true;
        }
        return false;

    }

    int CountScore()
    {
        int baseCount = 0;
        int missileCount = 0;
        int baseScore = 50;
        int missileScore = 5;

        for (int i = 0; i < Enemytargets.Length; i++)
        {
            baseCount++;
        }

        for (int i = 0; i < playerBases.Length; i++)
        {
            for (int j = 0; j < playerBases[i].playerMissileLauncher.Length; j++)
            {
                if (playerBases[i].playerMissileLauncher[j].gameObject.activeInHierarchy)
                {
                    missileCount++;
                }
            }
        }

        return (baseCount * baseScore + missileCount * missileScore) * LevelMultiplier;
    }

    public void ResetGame()
    {
        GameLevel = 1;
        LevelMultiplier = 1;
        TotalScore = 0;
        isWaveActive = false;
        LevelMultiplier = (1 + (GameLevel / 10));

        for (int i = 0; i < playerBases.Length; i++)
        {
            playerBases[i].ResetMissileLaunchers();
            playerBases[i].ResetBase();
        }

        for (int i = 0; i < playerCities.Length; i++)
        {
            playerCities[i].ResetCity();
        }

        GameEvents.Instance.GameResetEvent();

        Instance.enemyManager.MissileCount = 10 * LevelMultiplier;
        Instance.enemyManager.MissileSpeed = Instance.enemyManager.MissileSpeed * LevelMultiplier;
    }

    public void PlayNextSong()
    {
        StopCurrentSong();
        if (musicTrackNumber <= 2)
        {
            musicTrackNumber++;
        }
        else
        {
            musicTrackNumber = 0;
        }

        fullTrackName = "MusicGame" + musicTrackNumber;
        audioManager.Play(fullTrackName);
    }
    public void StopCurrentSong()
    {
        if (fullTrackName != null)
        {
            audioManager.Stop(fullTrackName);
        }
    }
    public void Quit()
    {
        //        Debug.Log("Quit");

        //#if UNITY_EDITOR
        //        EditorApplication.Exit(0);
        //#else
        //                Application.Quit();
        //#endif
    }
}
