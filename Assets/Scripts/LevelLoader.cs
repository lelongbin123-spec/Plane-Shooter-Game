using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private LevelData currentLevel;

    public LevelData CurrentLevel => currentLevel;

    public void SetLevel(LevelData levelData)
    {
        currentLevel = levelData;
    }

    public bool HasNextLevel()
    {
        return currentLevel != null && currentLevel.nextLevel != null;
    }

    public LevelData GetNextLevel()
    {
        if (currentLevel == null)
            return null;

        return currentLevel.nextLevel;
    }

    public void LoadNextLevelData()
    {
        if (currentLevel == null || currentLevel.nextLevel == null)
        {
            Debug.Log("No next level data.");
            return;
        }

        currentLevel = currentLevel.nextLevel;
    }
}
