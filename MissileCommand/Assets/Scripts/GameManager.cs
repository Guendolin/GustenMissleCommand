using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //Store the 34 Base tiles       -For base pos and game juice
    //Store the 3 city backgrounds  -For game juice, like shaking when city destroyed

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

    public int levelMultiplier = 1;

    public int TotalScore = 0;
    public Text ScoreText;
    public Text LevelText;
    public Button StartButton;
    public Button CreditsButton;
    public Button QuitButton;

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
        ScoreText.text = "SCORE: " + TotalScore;
        LevelText.text = "LEVEL: " + GameLevel;

        playerBases = PlayerManager.Instance.playerBases;
    }

    void Update()
    {
        //TODO (rework with events) check when towns explode
        if (isTheGameLost())
        {
            Debug.Log("Game Over! :(");
            EnableMenu();
        }
    }

    public void GameStart()
    {
        DisableMenu();
        isWaveActive = true;
    }

    public void LevelWon()
    {
        isWaveActive = false;
        Debug.Log("Level Won");

        levelMultiplier = (1 + (GameLevel / 10));
        GameLevel++;
        LevelText.text = "LEVEL: " + GameLevel;

        TotalScore += CountScore();
        ScoreText.text = "SCORE: " + TotalScore;

        for (int i = 0; i < playerBases.Length; i++)
        {
            playerBases[i].ResetMissileLaunchers();
        }

        EnemyManager.Instance.MissileCount = 10 * levelMultiplier;
        EnemyManager.Instance.MissileSpeed = EnemyManager.Instance.MissileSpeed * levelMultiplier;
        EnableMenu();
    }

    bool isTheGameLost()
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

        return (baseCount * baseScore + missileCount * missileScore) * levelMultiplier;
    }

    public void EnableMenu()
    {
        StartButton.gameObject.SetActive(true);
        CreditsButton.gameObject.SetActive(true);
        QuitButton.gameObject.SetActive(true);
    }
    public void DisableMenu()
    {
        StartButton.gameObject.SetActive(false);
        CreditsButton.gameObject.SetActive(false);
        QuitButton.gameObject.SetActive(false);
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
