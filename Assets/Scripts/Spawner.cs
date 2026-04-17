using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject []prefabToSpawn;
    public GameObject popup;
    public UILevel level;
    public float spawnDelay = 2f;
    public int enemyCount = 10;

    [Range(0f, 1f)]
    public float popupChance = 0.2f; // 20% tỉ lệ xuất hiện

    private bool lastSpawnedEnemy = false;
    private bool popupSpawned = false; // đảm bảo chỉ 1 lần

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyWithDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (lastSpawnedEnemy && FindAnyObjectByType<EnemyScript>() == null)
        {
            StartCoroutine(level.ShowLevelComplete());
        }
    }

    IEnumerator SpawnEnemyWithDelay()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            yield return new WaitForSeconds(spawnDelay); // Adjust the delay as needed
            SpawnEnemy();

            // 👇 check spawn popup
            TrySpawnPopup();
        }
        lastSpawnedEnemy = true;
    }
    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, prefabToSpawn.Length);
        int randomX = Random.Range(-2, 2); // Adjust the range as needed
        Instantiate(prefabToSpawn[randomIndex], new Vector3(randomX, transform.position.y, transform.position.z), Quaternion.identity);
    }
    void TrySpawnPopup()
    {
        if (popup == null)
        {
            Debug.LogWarning("Popup prefab is missing on Spawner.", this);
            return;
        }

        if (!popupSpawned && Random.value < popupChance)
        {
            popupSpawned = true;

            Instantiate(popup,
                new Vector3(0, transform.position.y, transform.position.z),
                Quaternion.identity);
        }
    }
}
