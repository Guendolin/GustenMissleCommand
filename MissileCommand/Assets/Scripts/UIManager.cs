using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    //public int TotalScore = 0;
    public Text ScoreText;
    public Text LevelText;
    public Text LevelWonText;
    public Text GameOverText;
    public Button StartButton;
    public Button RestartButton;
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
        ScoreText.text = " " + GameManager.Instance.TotalScore;
        LevelText.text = " " + GameManager.Instance.GameLevel;
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
}
