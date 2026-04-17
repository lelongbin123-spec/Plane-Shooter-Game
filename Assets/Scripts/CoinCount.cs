using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CoinCount : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public TextMeshProUGUI coinsText;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    public void UpdateCoin(int score)
    {
        textMeshPro.text = score.ToString();
        coinsText.text = "Coins: " + score.ToString();
    }
}
