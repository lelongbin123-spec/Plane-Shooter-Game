using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenuStart : MonoBehaviour
{
    public Button startButton, exitButton;
    public LevelLoader levelLoader;

    [Header("Choose Plane")]
    public GameObject mainMenuPanel;
    public GameObject choosePlanePanel;
    public Button[] planeButtons;
    public Button playSelectedButton;
    public Button backButton;
    public Image selectedPlanePreview;
    public Sprite[] planeSprites;
    public string gameplaySceneName = "Level1";
    public int defaultPlaneIndex = 0;

    private int selectedPlaneIndex;

    // Start is called before the first frame update
    void Start()
    {
        selectedPlaneIndex = PlayerPlaneSelection.GetSelectedPlane(defaultPlaneIndex);

        startButton.onClick.AddListener(OpenChoosePlane);
        exitButton.onClick.AddListener(ExitGame);

        if (playSelectedButton != null)
        {
            playSelectedButton.onClick.AddListener(Playgame);
        }

        if (backButton != null)
        {
            backButton.onClick.AddListener(ShowMainMenu);
        }

        for (int i = 0; i < planeButtons.Length; i++)
        {
            int planeIndex = i;
            if (planeButtons[i] != null)
            {
                planeButtons[i].onClick.AddListener(() => SelectPlane(planeIndex));
            }
        }

        ShowMainMenu();
        SelectPlane(selectedPlaneIndex);
    }

    private void OpenChoosePlane()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayButtonClick();
        }

        if (choosePlanePanel == null)
        {
            Playgame();
            return;
        }

        SetMainMenuVisible(false);

        choosePlanePanel.SetActive(true);
        SelectPlane(selectedPlaneIndex);
    }

    private void ShowMainMenu()
    {
        SetMainMenuVisible(true);

        if (choosePlanePanel != null)
        {
            choosePlanePanel.SetActive(false);
        }
    }

    private void SetMainMenuVisible(bool visible)
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(visible);
            return;
        }

        if (startButton != null)
        {
            startButton.gameObject.SetActive(visible);
        }

        if (exitButton != null)
        {
            exitButton.gameObject.SetActive(visible);
        }
    }

    private void SelectPlane(int planeIndex)
    {
        selectedPlaneIndex = Mathf.Max(0, planeIndex);
        PlayerPlaneSelection.SetSelectedPlane(selectedPlaneIndex, GetSelectedPlaneSprite());

        if (selectedPlanePreview != null && selectedPlaneIndex < planeSprites.Length)
        {
            selectedPlanePreview.sprite = planeSprites[selectedPlaneIndex];
            selectedPlanePreview.enabled = selectedPlanePreview.sprite != null;
        }

        for (int i = 0; i < planeButtons.Length; i++)
        {
            if (planeButtons[i] != null)
            {
                planeButtons[i].interactable = i != selectedPlaneIndex;
            }
        }
    }

    private Sprite GetSelectedPlaneSprite()
    {
        if (selectedPlaneIndex >= 0 && selectedPlaneIndex < planeSprites.Length)
        {
            return planeSprites[selectedPlaneIndex];
        }

        return null;
    }

    private void Playgame()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayButtonClick();
        }

        PlayerPlaneSelection.SetSelectedPlane(selectedPlaneIndex, GetSelectedPlaneSprite());
        SceneManager.LoadScene(gameplaySceneName);
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


