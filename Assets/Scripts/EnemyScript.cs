using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class EnemyScript : MonoBehaviour
{
    public Transform []gunPoint;
    public GameObject enemyBullet;
    public GameObject EnemyFlash;
    public GameObject enemyExplosionPrefabs;
    public GameObject damagePrefab;
    public HealthBar healthBar;
    public GameObject coinPrefab;
    public float bulletSpawnTime = 0.5f;
    public float health = 10f;
    float barSize = 1f;
    float damage = 0;

    public float moveSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        EnemyFlash.SetActive(false);
        StartCoroutine(EnemyShoot());
        damage = barSize / health;
    }

    void Update()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            DamageHealthBar();
            Destroy(collision.gameObject);
            GameObject damageVfx = Instantiate(damagePrefab, collision.transform.position, Quaternion.identity);
            Destroy(damageVfx, 0.1f);
            if (health <= 0)
            {
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
                GameObject enemyExplosion = Instantiate(enemyExplosionPrefabs, transform.position, Quaternion.identity);
                Destroy(enemyExplosion, 0.4f);

                if (AudioManager.HasInstance)
                {
                    AudioManager.Instance.PlayExplosion();
                }
            }
        }
    }
    void DamageHealthBar()
    {
        if (health > 0)
        {
            health -= 1;
            barSize = barSize - damage;
            healthBar.SetSize(barSize);
        }
    }
    void EnemyFire()
    {
        //Instantiate(enemyBullet, gunLeft.transform.position, Quaternion.identity);
        //Instantiate(enemyBullet, gunRight.transform.position, Quaternion.identity);

        for (int i = 0; i < gunPoint.Length; i++) 
        {
            Instantiate(enemyBullet, gunPoint[i].position, Quaternion.identity);
        }

        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayEnemyShoot();
        }
    }
    IEnumerator EnemyShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(bulletSpawnTime);
            EnemyFire();
            EnemyFlash.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            EnemyFlash.SetActive(false);
        }
    }
}
