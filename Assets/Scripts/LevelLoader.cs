using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    int currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(currentLevel+1);
    }
    public void Reload()
    {
        SceneManager.LoadScene(currentLevel);
    }
}
