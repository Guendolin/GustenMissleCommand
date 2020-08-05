using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Store the 34 Base tiles       -For base pos and game juice
    //Store the 3 city backgrounds  -For game juice, like shaking when city destroyed

    public GameObject[] BaseTiles = new GameObject[34];

    [SerializeField]
    private GameObject cityLayerBack;

    [SerializeField]   
    private GameObject cityLayerMid;

    [SerializeField]   
    private GameObject cityLayerFront;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
