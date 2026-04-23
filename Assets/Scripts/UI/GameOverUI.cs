using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject uiParent;
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI gameOverTitleText;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button backToMenuButton;

    void Start()
    {
        Hide();
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        if (nextLevelButton != null)
        {
            nextLevelButton.onClick.AddListener(OnNextLevelClicked);
        }
        if (backToMenuButton != null)
        {
            backToMenuButton.onClick.AddListener(OnBackToMenuClicked);
        }
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOverState())
        {
            Show();
        }
    }

    private void Show()
    {
        int totalScore = OrderMananger.Instance.GetTotalScore();
        int targetScore = LevelManager.GetTargetScore();
        bool passed = LevelManager.IsLevelPassed(totalScore);

        numberText.text = OrderMananger.Instance.GetSuccessDeliveryCount().ToString();

        if (scoreText != null)
        {
            scoreText.text = totalScore + "\u91d1\u5e01";
        }

        if (passed)
        {
            if (gameOverTitleText != null)
            {
                gameOverTitleText.text = "\u7b2c" + LevelManager.GetCurrentLevel() + "\u5173\u901a\u5173\uff01";
            }
            if (resultText != null)
            {
                resultText.gameObject.SetActive(false);
            }
            if (nextLevelButton != null) nextLevelButton.gameObject.SetActive(true);
            if (backToMenuButton != null) backToMenuButton.gameObject.SetActive(false);
        }
        else
        {
            if (gameOverTitleText != null)
            {
                gameOverTitleText.text = "\u53a8\u623f\u5012\u95ed";
            }
            if (resultText != null)
            {
                resultText.gameObject.SetActive(false);
            }
            if (nextLevelButton != null) nextLevelButton.gameObject.SetActive(false);
            if (backToMenuButton != null) backToMenuButton.gameObject.SetActive(true);
        }

        uiParent.SetActive(true);
    }

    private void Hide()
    {
        uiParent.SetActive(false);
    }

    private void OnNextLevelClicked()
    {
        LevelManager.GoToNextLevel();
        Loader.Load(Loader.Scene.GameScene);
    }

    private void OnBackToMenuClicked()
    {
        LevelManager.ResetToFirstLevel();
        Loader.Load(Loader.Scene.GameMenuScene);
    }
}