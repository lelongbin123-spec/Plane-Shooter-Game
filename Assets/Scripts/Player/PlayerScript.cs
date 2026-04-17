using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject damageEffect;
    public GameObject explosion;
    public UIGameplay uiGameplay;
    public UIGameOver uiGameOver;
    public float speed = 10f;
    public float papding = 0.8f;
    float xMin, xMax, yMin, yMax;

    public float health = 20f;
    float barFillAmount = 1f;
    float damageAmount = 0;
    private int score = 0;
    void Awake()
    {
        //khong vuot ngoai khu vuc camera
        Camera cam = Camera.main;
        float distance = transform.position.z - cam.transform.position.z;
        Vector3 leftBottom = cam.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightTop = cam.ViewportToWorldPoint(new Vector3(1, 1, distance));
        xMin = leftBottom.x + papding;
        xMax = rightTop.x - papding;
        yMin = leftBottom.y + papding;
        yMax = rightTop.y - papding;
    }
    // Start is called before the first frame update
    void Start()
    {
        damageAmount = barFillAmount / health;
        uiGameOver.Close();
    }

    // Update is called once per frame
    void Update()
    {
        //di chuyen theo input
        /*float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float deltaY = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        float newXpos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        float newYpos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector3(newXpos, newYpos, transform.position.z);*/

        if (Input.GetMouseButton(0)) 
        { 
            Vector2 newPos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            transform.position = Vector2.Lerp(transform.position, newPos, speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        { 
            DamagePlayerHealthBar();
            Destroy(collision.gameObject);
            GameObject damageVfx = Instantiate(damageEffect, collision.transform.position, Quaternion.identity);
            Destroy(damageVfx, 0.1f);

            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlayPlayerHit();
            }

            if (health <= 0)
            {
                Destroy(gameObject);
                GameObject playerExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(playerExplosion, 2f);

                if (AudioManager.HasInstance)
                {
                    AudioManager.Instance.PlayPlayerDeath();
                }

                //xu ly khi player bi trung dan
            }
        }
        if (collision.CompareTag("Coin"))
        {
            // Handle coin collection
            Destroy(collision.gameObject);
            score++;
            uiGameplay.UpdateCoin(score);

            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlayCoinPickup();
            }
        }
    }

    void DamagePlayerHealthBar()
    {
        if (health > 0)
        {
            health -= 1;
            barFillAmount = barFillAmount - damageAmount;
            uiGameplay.UpdateHealthbar(barFillAmount);
        }

        if (health <= 0)
        {
            Time.timeScale = 0f;
            uiGameOver.Show(score);
        }
    }
}
