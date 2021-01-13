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

    public event Action onGameStartEvent;
    public event Action onLevelWonEvent;
    public event Action onGameResetEvent;
    public event Action onGameOverEvent;
    public void GameStartEvent()
    {
        if (onGameStartEvent != null)
        {
            onGameStartEvent();
        }
    }
    public void LevelWonEvent()
    {
        if (onLevelWonEvent != null)
        {
            onLevelWonEvent();
        }
    }
    public void GameResetEvent()
    {
        if (onGameResetEvent != null)
        {
            onGameResetEvent();
        }
    }
    public void GameOverEvent()
    {
        if (onGameOverEvent != null)
        {
            onGameOverEvent();
        }
    }
}

