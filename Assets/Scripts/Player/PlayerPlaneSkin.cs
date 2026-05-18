using UnityEngine;

[System.Serializable]
public class PlaneWeaponPosition
{
    public Vector3 leftSpawnPosition;
    public Vector3 rightSpawnPosition;
    public Vector3 leftFlashPosition;
    public Vector3 rightFlashPosition;
}

public class PlayerPlaneSkin : MonoBehaviour
{
    [SerializeField] private SpriteRenderer targetRenderer;
    [SerializeField] private Sprite[] planeSprites;
    [SerializeField] private Animator targetAnimator;
    [SerializeField] private RuntimeAnimatorController[] animatorControllers;
    [SerializeField] private bool forceSpriteAfterAnimator = true;

    [Header("Weapon Positions")]
    [SerializeField] private Shooting shooting;
    [SerializeField] private PlaneWeaponPosition[] weaponPositions;

    private Sprite selectedSprite;

    private void Awake()
    {
        if (targetRenderer == null)
        {
            targetRenderer = GetComponent<SpriteRenderer>();
        }

        if (targetAnimator == null)
        {
            targetAnimator = GetComponent<Animator>();
        }

        if (shooting == null)
        {
            shooting = GetComponent<Shooting>();
        }
    }

    private void Start()
    {
        ApplySelectedPlane();
    }

    private void LateUpdate()
    {
        if (forceSpriteAfterAnimator && targetRenderer != null && selectedSprite != null)
        {
            targetRenderer.sprite = selectedSprite;
        }
    }

    private void ApplySelectedPlane()
    {
        int selectedPlaneIndex = PlayerPlaneSelection.GetSelectedPlane();
        selectedSprite = PlayerPlaneSelection.SelectedPlaneSprite;

        if (selectedSprite == null && selectedPlaneIndex < planeSprites.Length)
        {
            selectedSprite = planeSprites[selectedPlaneIndex];
        }

        if (targetRenderer != null && selectedSprite != null)
        {
            targetRenderer.sprite = selectedSprite;
        }

        if (targetAnimator != null && selectedPlaneIndex < animatorControllers.Length && animatorControllers[selectedPlaneIndex] != null)
        {
            targetAnimator.runtimeAnimatorController = animatorControllers[selectedPlaneIndex];
        }

        ApplyWeaponPosition(selectedPlaneIndex);
    }

    private void ApplyWeaponPosition(int selectedPlaneIndex)
    {
        if (shooting == null || selectedPlaneIndex < 0 || selectedPlaneIndex >= weaponPositions.Length)
        {
            return;
        }

        PlaneWeaponPosition position = weaponPositions[selectedPlaneIndex];

        if (shooting.spawnPointLeft != null)
        {
            shooting.spawnPointLeft.localPosition = position.leftSpawnPosition;
        }

        if (shooting.spawnPointRight != null)
        {
            shooting.spawnPointRight.localPosition = position.rightSpawnPosition;
        }

        if (shooting.flashLeft != null)
        {
            shooting.flashLeft.transform.localPosition = position.leftFlashPosition;
        }

        if (shooting.flashRight != null)
        {
            shooting.flashRight.transform.localPosition = position.rightFlashPosition;
        }
    }
}
