using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenuStart : MonoBehaviour
{
    public Button startButton, exitButton;
    public LevelLoader levelLoader;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(Playgame);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void Playgame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayButtonClick();
        }

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
