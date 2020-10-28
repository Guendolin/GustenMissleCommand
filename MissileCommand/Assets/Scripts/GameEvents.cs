using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance { get; private set; }

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
    ////public delegate int OnGameStartEventHandler(int x);
    ////public OnGameStartEventHandler Test = (x) => { return 0; };


    ////public Func<int,int> Test2 = (x) => { return 0; };

    public event Action onGameStartEvent;
    public void GameStartEvent()
    {
        if (onGameStartEvent != null)
        {
            onGameStartEvent();
        }
    }
}
