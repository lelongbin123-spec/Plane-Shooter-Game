using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject flash;
    public Transform spawnPointLeft;
    public Transform spawnPointRight;
    public float bulletSpawnTime = 0.5f;

    [SerializeField] private float minBulletSpawnTime = 0.1f;

    private float defaultBulletSpawnTime;
    private Coroutine fireRateBoostCoroutine;
    // Start is called before the first frame update
    void OnEnable()
    {
        defaultBulletSpawnTime = bulletSpawnTime;
        flash.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void Fire()
    {
        Instantiate(bulletPrefab, spawnPointLeft.transform.position, Quaternion.identity);
        Instantiate(bulletPrefab, spawnPointRight.transform.position, Quaternion.identity);

        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayPlayerShoot();
        }
    }

    public void ApplyFireRateBoost(float fireIntervalMultiplier, float duration)
    {
        bulletSpawnTime = Mathf.Max(minBulletSpawnTime, defaultBulletSpawnTime * fireIntervalMultiplier);

        if (fireRateBoostCoroutine != null)
        {
            StopCoroutine(fireRateBoostCoroutine);
        }

        fireRateBoostCoroutine = StartCoroutine(ResetFireRateAfterDelay(duration));
    }

    IEnumerator ResetFireRateAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        bulletSpawnTime = defaultBulletSpawnTime;
        fireRateBoostCoroutine = null;
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(bulletSpawnTime);
            Fire();
            flash.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            flash.SetActive(false);
        }
    }
}
