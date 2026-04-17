using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public LevelLoader levelLoader;
    public Transform PanelOver;

    public Button replayButton, exitButton;
    private Tween panelTween;

    private void Start()
    {
        replayButton.onClick.AddListener(OnReplayClicked);
        exitButton.onClick.AddListener(OnExitClicked);
    }

    public void Close()
    {
        KillPanelTween();
        gameObject.SetActive(false);
    }

    public void Show(int score)
    {
        Time.timeScale = 0;
        scoreText.text = $"Your Score: {score}";
        gameObject.SetActive(true);

        if (PanelOver == null)
        {
            return;
        }

        KillPanelTween();
        PanelOver.localScale = Vector3.zero;
        panelTween = PanelOver.DOScale(Vector3.one, 0.25f).SetUpdate(true);

    }

    private void OnDisable()
    {
        KillPanelTween();
    }

    private void OnDestroy()
    {
        KillPanelTween();
    }

    private void KillPanelTween()
    {
        if (panelTween != null && panelTween.IsActive())
        {
            panelTween.Kill();
        }

        panelTween = null;
    }

    private void OnExitClicked()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayButtonClick();
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    private void OnReplayClicked()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayButtonClick();
        }

        Time.timeScale = 1f;
        levelLoader.Reload();
    }
}
