using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupScript : MonoBehaviour
{
    [SerializeField] private float fireIntervalMultiplier = 0.5f;
    [SerializeField] private float boostDuration = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerScript player = collision.GetComponent<PlayerScript>();
        if (player == null)
        {
            return;
        }

        Shooting shooting = player.GetComponent<Shooting>();
        if (shooting != null)
        {
            shooting.ApplyFireRateBoost(fireIntervalMultiplier, boostDuration);
        }

        Destroy(gameObject);
    }
}
