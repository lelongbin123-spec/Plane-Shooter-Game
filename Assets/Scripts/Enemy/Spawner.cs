using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;
    public GameObject []prefabToSpawn;
    public PlayerScript player;
    public GameObject popup;
    public UILevel level;
    [SerializeField] private LevelLoader levelLoader;


    public float spawnDelay = 2f;
    public int enemyCount = 10;

    [Range(0f, 1f)]
    public float popupChance = 0.2f; // 20% tỉ lệ xuất hiện

    private bool lastSpawnedEnemy = false;
    private bool popupSpawned = false; // đảm bảo chỉ 1 lần
    private bool levelCompleted = false;

    private int currentEnemyCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        lastSpawnedEnemy = false;
        popupSpawned = false;
        levelCompleted = false;
        currentEnemyCount = 0;
    }

    public void DestroyEnemy()
    {
        currentEnemyCount = Mathf.Max(0, currentEnemyCount - 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!levelCompleted && lastSpawnedEnemy && currentEnemyCount == 0)
        {
            levelCompleted = true;
            StartCoroutine(level.ShowLevelComplete());
        }
    }

    public void StartCurrentLevel(bool resetScore = true)
    {
        StopAllCoroutines();

        foreach (var enemy in FindObjectsOfType<EnemyScript>())
            Destroy(enemy.gameObject);

        foreach (var bullet in GameObject.FindGameObjectsWithTag("EnemyBullet"))
            Destroy(bullet);

        foreach (var coin in GameObject.FindGameObjectsWithTag("Coin"))
            Destroy(coin);

        foreach (var popupObj in FindObjectsOfType<PopupScript>())
            Destroy(popupObj.gameObject);

        player.Resurrection(resetScore);
        currentEnemyCount = 0;
        lastSpawnedEnemy = false;
        popupSpawned = false;
        levelCompleted = false;

        StartCoroutine(SpawnEnemyWithDelay());
    }


    IEnumerator SpawnEnemyWithDelay()
    {
        LevelData levelData = levelLoader.CurrentLevel;
        if (levelData == null)
            yield break;

        for (int i = 0; i < levelData.enemyCount; i++)
        {
            yield return new WaitForSeconds(levelData.spawnDelay);
            SpawnEnemy(levelData);
            TrySpawnPopup();
        }

        lastSpawnedEnemy = true;
    }
    private void SpawnEnemy(LevelData levelData)
    {
        if (levelData.prefabToSpawn == null || levelData.prefabToSpawn.Length == 0)
            return;

        int randomIndex = Random.Range(0, levelData.prefabToSpawn.Length);
        int randomX = Random.Range(-2, 2);

        Instantiate(
            levelData.prefabToSpawn[randomIndex],
            new Vector3(randomX, transform.position.y, transform.position.z),
            Quaternion.identity
        );
        currentEnemyCount++;
    }
    private void TrySpawnPopup()
    {
        if (popup == null)
            return;

        if (!popupSpawned && Random.value < popupChance)

        {
            popupSpawned = true;
            Instantiate(popup, new Vector3(0, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }

}
