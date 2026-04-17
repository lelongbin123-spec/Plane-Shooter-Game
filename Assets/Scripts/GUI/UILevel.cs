using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILevel : MonoBehaviour
{
    public Button nextLevelButton;
    public Button exitButton;
    public GameObject endText;
    public LevelLoader levelLoader;

    // Start is called before the first frame update
    private void Start()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        endText.SetActive(false);
        nextLevelButton.onClick.AddListener(OnNextLevelClicked);
        exitButton.onClick.AddListener(OnExitClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator ShowLevelComplete()
    {
        yield return new WaitForSeconds(2f);
        endText.SetActive(true);
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }
    public void OnExitClicked()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayButtonClick();
        }

        SceneManager.LoadScene("Menu");
    }
    public void OnNextLevelClicked()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayButtonClick();
        }
        // Load the next level (you can replace "NextLevelSceneName" with the actual name of your next level scene)
        //SceneManager.LoadScene("NextLevelSceneName");
        levelLoader.NextLevel();
    }
}
