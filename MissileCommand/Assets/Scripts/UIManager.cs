using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public Text ScoreText;
    public Text LevelText;
    public Text LevelWonText;
    public Text GameOverText;
    public Button StartButton;
    public Button RestartButton;
    public Button QuitButton;

    void Start()
    {
        ScoreText.text = " " + GameManager.Instance.TotalScore;
        LevelText.text = " " + GameManager.Instance.GameLevel;
        GameEvents.Instance.onGameStartEvent += UIGameStart;
        GameEvents.Instance.onLevelWonEvent += UILevelWon;
        GameEvents.Instance.onGameResetEvent += UIGameReset;
    }

    void Update()
    {
        if (GameManager.Instance.isTheGameLost())
        {
            GameOverText.gameObject.SetActive(true);
            EnableMenu();
        }
    }

    public void EnableMenu()
    {
        StartButton.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(true);
        QuitButton.gameObject.SetActive(true);
    }
    public void DisableMenu()
    {
        StartButton.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);
        QuitButton.gameObject.SetActive(false);
        LevelWonText.gameObject.SetActive(false);
        GameOverText.gameObject.SetActive(false);
    }

    private void UIGameStart()
    {
        DisableMenu();
    }

    private void UILevelWon()
    {
        LevelWonText.gameObject.SetActive(true);
        LevelText.text = " " + GameManager.Instance.GameLevel;
        ScoreText.text = " " + GameManager.Instance.TotalScore;
        EnableMenu();
    }

    private void UIGameReset()
    {
        LevelText.text = " " + GameManager.Instance.GameLevel;
        ScoreText.text = " " + GameManager.Instance.TotalScore;
        EnableMenu();
    }

    private void OnDestroy()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.onGameStartEvent -= UIGameStart;
            GameEvents.Instance.onLevelWonEvent -= UILevelWon;
            GameEvents.Instance.onGameResetEvent -= UIGameReset;
        }
    }
}
