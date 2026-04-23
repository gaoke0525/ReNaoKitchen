using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private GameObject uiParent;
    [SerializeField] private TextMeshProUGUI upKeyText;
    [SerializeField] private TextMeshProUGUI downKeyText;
    [SerializeField] private TextMeshProUGUI leftKeyText;
    [SerializeField] private TextMeshProUGUI rightKeyText;
    [SerializeField] private TextMeshProUGUI interactKeyText;
    [SerializeField] private TextMeshProUGUI operateKeyText;
    [SerializeField] private TextMeshProUGUI pauseKeyText;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        if (LevelManager.GetCurrentLevel() == 1)
        {
            StartCoroutine(DelayedShow());
        }
        else
        {
            Hide();
        }
    }

    private IEnumerator DelayedShow()
    {
        while (GameInput.Instance == null)
        {
            yield return null;
        }
        Show();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsWaitingToStartState() && LevelManager.GetCurrentLevel() == 1)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        if (GameInput.Instance == null) return;
        UpdateVisual();
        uiParent.SetActive(true);
    }
    private void Hide()
    {
        uiParent.SetActive(false);
    }
    private void UpdateVisual()
    {
        upKeyText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Up);
        downKeyText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Down);
        leftKeyText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Left);
        rightKeyText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Right);
        interactKeyText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Interact);
        operateKeyText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Operate);
        pauseKeyText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Pause);
    }
}
