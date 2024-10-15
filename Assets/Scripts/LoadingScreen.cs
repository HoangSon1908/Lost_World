using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image loadingImage;
    [SerializeField] private GameObject gamePlay;
    [SerializeField] private GameObject gamePlayUI;
    [SerializeField] private Button actionButton;
    [SerializeField] private TextMeshProUGUI currentYearText;

    private bool isGameStarted;
    private bool isGamePaused;
    public static LoadingScreen Instance;

    void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        ShowLoadingScreen();
        actionButton.onClick.AddListener(OnActionClick);
    }

    void OnActionClick()
    {
        if (!isGameStarted) 
        {
            StartGame();
        }
        else if (isGamePaused)
        {
            ContinueGame();
        }
    }

    void StartGame() 
    {
        isGameStarted = true;
        isGamePaused = false;

        loadingImage.DOFade(0, 0.5f).OnComplete(() => 
        {
            loadingImage.gameObject.SetActive(false);
            gamePlayUI.SetActive(true);
            gamePlay.SetActive(true);
        });

        Time.timeScale = 1f;
    }

    public void GameOver() 
    {
        isGameStarted = false;
        isGamePaused = true;

        loadingImage.gameObject.SetActive(true);
        gamePlayUI.SetActive(false);
        gamePlay.SetActive(false);
        loadingImage.DOFade(1, 3f).From(0);

        Time.timeScale = 0f;
    }

    void ContinueGame() 
    {
        isGamePaused = false;

        loadingImage.DOFade(0, 0.5f).OnComplete(() => 
        {
            loadingImage.gameObject.SetActive(false);
            gamePlayUI.SetActive(true);
            gamePlay.SetActive(true);
        });

        Time.timeScale = 1f;
    }

    public void ShowLoadingScreen()
    {
        loadingImage.DOFade(1, 3f).From(0);
        gamePlayUI.SetActive(false);
        currentYearText.text = $"Năm\n {GameManager.Instance.currentYears} trước công nguyên";
    }
}
