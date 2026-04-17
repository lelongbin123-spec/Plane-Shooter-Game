using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : MonoBehaviour
{
    public CoinCount coinCount;
    public PlayerHealthbar playerHealthbar;
    public UIPauseGameScript pauseGameScript;
    public Button pauseButton;

    private void Start()
    {
        Time.timeScale = 1f;
        pauseButton.onClick.AddListener(pauseGameScript.PauseGame);
        pauseButton.gameObject.SetActive(true);
    }

    public void UpdateHealthbar(float health)
    {
        playerHealthbar.SetAmount(health);
    }

    public void UpdateCoin(int score)
    {
        coinCount.UpdateCoin(score);
    }
}