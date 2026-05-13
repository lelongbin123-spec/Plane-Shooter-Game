using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILevel : MonoBehaviour
{
    public Button nextLevelButton;
    public Button exitButton;
    public GameObject startText;
    public GameObject endText;
    public LevelLoader levelLoader;
    public Spawner spawner;

    private CanvasGroup canvasGroup;

    private void Start()
    {
        Time.timeScale = 1f;
        EnsureCanvasGroup();
        SetPanelVisible(false);
        endText.SetActive(false);
        startText.SetActive(false);

        nextLevelButton.onClick.AddListener(OnNextLevelClicked);
        exitButton.onClick.AddListener(OnExitClicked);

        if (levelLoader != null && levelLoader.CurrentLevel != null)
        {
            StartCoroutine(BeginLevel(levelLoader.CurrentLevel.levelNumber));
        }
    }

    public IEnumerator ShowLevelComplete()
    {
        yield return new WaitForSeconds(2f);
        endText.SetActive(true);
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0f;
        SetPanelVisible(true);
    }

    public IEnumerator ShowLevelStart(int levelNumber)
    {
        Time.timeScale = 1f;
        SetPanelVisible(false);
        endText.SetActive(false);

        startText.SetActive(true);

        TMPro.TextMeshProUGUI text = startText.GetComponent<TMPro.TextMeshProUGUI>();
        if (text != null)
        {
            text.text = "Level " + levelNumber;
        }

        yield return new WaitForSeconds(2f);

        startText.SetActive(false);
    }


    public void OnExitClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void OnNextLevelClicked()
    {
        if (!levelLoader.HasNextLevel())
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
            return;
        }

        Time.timeScale = 1f;
        endText.SetActive(false);
        SetPanelVisible(false);

        levelLoader.LoadNextLevelData();
        StartCoroutine(BeginLevel(levelLoader.CurrentLevel.levelNumber));
    }
    private IEnumerator BeginLevel(int levelNumber)
    {
        yield return StartCoroutine(ShowLevelStart(levelNumber));
        spawner.StartCurrentLevel(false);
    }

    private void EnsureCanvasGroup()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    private void SetPanelVisible(bool visible)
    {
        if (canvasGroup == null)
        {
            EnsureCanvasGroup();
        }

        canvasGroup.alpha = visible ? 1f : 0f;
        canvasGroup.interactable = visible;
        canvasGroup.blocksRaycasts = visible;
    }
}
