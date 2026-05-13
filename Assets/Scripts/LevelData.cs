using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData")]
public class LevelData : ScriptableObject
{
    public int levelNumber;
    public string sceneName;
    public int enemyCount;
    public float spawnDelay = 2f;
    public GameObject[] prefabToSpawn;
    public LevelData nextLevel;
}
