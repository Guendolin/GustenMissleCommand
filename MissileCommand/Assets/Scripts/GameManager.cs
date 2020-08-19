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

    public GameObject[] targetCities = new GameObject[6];

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

    }

    void Update()
    {

    }
}
