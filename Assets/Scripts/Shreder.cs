using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shreder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyScript enemy = collision.GetComponentInParent<EnemyScript>();
        if (enemy != null)
        {
            if (Spawner.Instance != null)
            {
                Spawner.Instance.DestroyEnemy();
            }

            Destroy(enemy.gameObject);
            return;
        }

        Destroy(collision.gameObject);
    }
}
