using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    //Ask Simon about these arrays, they are separate in some functions and share other functions, should this be rearranged?
    public GameObject[] playerCities;

    private PlayerBase[] playerBases;

    public GameObject[] Enemytargets = new GameObject[8];

    public int GameLevel = 1;

    public int levelMultiplier = 1;


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

    void Update()
    {
        //Ask Simon, use this as is or switch it around so that the game manager keeps the list and the explosion manager accesses it?

        //Win scenario
        if (EnemyManager.Instance.MissileCount == 1 && ExplosionManager.Instance.enemyMissiles.Count == 0)
        {
            //Display a level win text
            Debug.Log("Level Won");

            //Up the levelmultiplier
            levelMultiplier = (1 + (GameLevel / 10));

            //Score counting
            Debug.Log("Score: " + CountScore());

            //Reset the player missiles for non destroyed bases
            for (int i = 0; i < playerBases.Length; i++)
            {
                playerBases[i].ResetMissileLaunchers();
            }

            //Set a new amount of enemy missiles and increase speed slightly
            EnemyManager.Instance.MissileCount = 10 * levelMultiplier;
            EnemyManager.Instance.MissileSpeed = EnemyManager.Instance.MissileSpeed * levelMultiplier;

        }
        if (isTheGameLost())
        {
            Debug.Log("Game Over! :(");
        }

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
}
