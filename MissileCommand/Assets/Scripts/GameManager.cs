using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject[] BaseTiles = new GameObject[34];

    [SerializeField]
    private GameObject cityLayerBack;

    [SerializeField]
    private GameObject cityLayerMid;

    [SerializeField]
    private GameObject cityLayerFront;

    //rework this with specific classes instead of game objects, could be empty classes
    public GameObject[] playerCities;

    private PlayerBase[] playerBases;

    public GameObject[] Enemytargets = new GameObject[8];

    public bool isWaveActive = false;
    public int GameLevel = 1;
    public int LevelMultiplier = 1;
    public int TotalScore = 0;


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
        playerBases = PlayerManager.Instance.playerBases;
    }


    public void GameStart()
    {
        if (UIManager.Instance)
        {
            UIManager.Instance.DisableMenu();
        }
        isWaveActive = true;
    }

    public void LevelWon()
    {
        isWaveActive = false;
        if (UIManager.Instance)
        {
            UIManager.Instance.LevelWonText.gameObject.SetActive(true);
        }

        LevelMultiplier = (1 + (GameLevel / 10));
        GameLevel++;
        if (UIManager.Instance)
        {
            UIManager.Instance.LevelText.text = " " + GameLevel;
        }

        TotalScore += CountScore();
        if (UIManager.Instance)
        {
            UIManager.Instance.ScoreText.text = " " + TotalScore;
        }

        for (int i = 0; i < playerBases.Length; i++)
        {
            playerBases[i].ResetMissileLaunchers();
        }

        EnemyManager.Instance.MissileCount = 10 * LevelMultiplier;
        EnemyManager.Instance.MissileSpeed = EnemyManager.Instance.MissileSpeed * LevelMultiplier;
        if (UIManager.Instance)
        {
            UIManager.Instance.EnableMenu();
        }
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
        
        if (UIManager.Instance)
        {
            UIManager.Instance.LevelText.text = " " + GameLevel;
        }

        if (UIManager.Instance)
        {
            UIManager.Instance.ScoreText.text = " " + TotalScore;
        }

        for (int i = 0; i < playerBases.Length; i++)
        {
            playerBases[i].ResetMissileLaunchers();
        }

        EnemyManager.Instance.MissileCount = 10 * LevelMultiplier;
        EnemyManager.Instance.MissileSpeed = EnemyManager.Instance.MissileSpeed * LevelMultiplier;
        
        if (UIManager.Instance)
        {
            UIManager.Instance.EnableMenu();
        }
    }
    public void Quit()
    {
        Debug.Log("Quit");

        //#if UNITY_EDITOR
        //        EditorApplication.Exit(0);
        //#else
        //        Application.Quit();
        //#endif
    }
}
