using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPauseGameScript : MonoBehaviour
{
    public Transform pauseGamePanel;
    public Button pauseButton;
    public Button resumeButton;
    public Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        resumeButton.onClick.AddListener(ResumeGame);
        exitButton.onClick.AddListener(ExitGame);
    }
    public void PauseGame()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayButtonClick();
        }

        gameObject.SetActive(true);
        Time.timeScale = 0;
        pauseButton.gameObject.SetActive(false);
    }
    public void ResumeGame()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayButtonClick();
        }

        Time.timeScale = 1;
        gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }
    public void ExitGame()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayButtonClick();
        }

        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
