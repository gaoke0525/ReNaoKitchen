using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;

    private int currentScore = 0;
    private bool isFlashing = false;
    private float flashTimer = 0f;
    private bool flashVisible = true;

    private void Start()
    {
        UpdateScoreText(0);
        OrderMananger.Instance.OnScoreChanged += OrderManager_OnScoreChanged;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        if (levelText != null)
        {
            levelText.text = "\u7b2c" + LevelManager.GetCurrentLevel() + "\u5173";
        }
    }

    private void Update()
    {
        int target = LevelManager.GetTargetScore();
        float timeLeft = GameManager.Instance.GetGamePlayingTimer();

        if (GameManager.Instance.IsGamePlayingState() && currentScore < target && timeLeft <= 10f && timeLeft > 0f)
        {
            if (!isFlashing)
            {
                isFlashing = true;
            }
            flashTimer += Time.deltaTime;
            if (flashTimer >= 1.0f)
            {
                flashTimer = 0f;
                flashVisible = !flashVisible;
                UpdateScoreText(currentScore);
            }
        }
        else if (isFlashing)
        {
            isFlashing = false;
            flashVisible = true;
            UpdateScoreText(currentScore);
        }
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOverState())
        {
            gameObject.SetActive(false);
        }
    }

    private void OrderManager_OnScoreChanged(object sender, OrderMananger.OnScoreChangedEventArgs e)
    {
        currentScore = e.totalScore;
        UpdateScoreText(e.totalScore);
    }

    private void UpdateScoreText(int score)
    {
        int target = LevelManager.GetTargetScore();
        string scoreColor;

        if (score >= target)
        {
            scoreColor = "#00FF00";
        }
        else if (isFlashing && !flashVisible)
        {
            scoreColor = "#FF000044";
        }
        else if (isFlashing)
        {
            scoreColor = "#FF0000";
        }
        else
        {
            scoreColor = "#FFFFFF";
        }

        scoreText.text = "<color=#FFE804>\u8425\u4e1a\u989d:</color><color=" + scoreColor + ">" + score + "\u91d1\u5e01</color> <color=#FFE804>\u5e97\u94fa\u79df\u91d1:</color>" + target + "\u91d1\u5e01";
    }

    private void OnDestroy()
    {
        if (OrderMananger.Instance != null)
        {
            OrderMananger.Instance.OnScoreChanged -= OrderManager_OnScoreChanged;
        }
    }
}