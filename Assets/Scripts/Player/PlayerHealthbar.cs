using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbar : MonoBehaviour
{
    public Image bar;
    [SerializeField] private float fillDuration = 0.2f;
    [SerializeField] private Ease fillEase = Ease.OutQuad;

    private Tween fillTween;

    private void Start()
    {
        if (bar != null)
        {
            bar.fillAmount = Mathf.Clamp01(bar.fillAmount);
        }
    }

    private void OnDisable()
    {
        KillFillTween();
    }

    private void OnDestroy()
    {
        KillFillTween();
    }

    public void SetAmount(float amount)
    {
        if (bar == null)
        {
            return;
        }

        float targetAmount = Mathf.Clamp01(amount);

        KillFillTween();
        fillTween = bar.DOFillAmount(targetAmount, fillDuration)
            .SetEase(fillEase)
            .SetUpdate(true);
    }

    private void KillFillTween()
    {
        if (fillTween != null && fillTween.IsActive())
        {
            fillTween.Kill();
        }

        fillTween = null;
    }
}
