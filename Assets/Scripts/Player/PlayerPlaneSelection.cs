using UnityEngine;

public static class PlayerPlaneSelection
{
    public const string PlayerPrefsKey = "SelectedPlaneIndex";

    public static Sprite SelectedPlaneSprite { get; private set; }

    public static int GetSelectedPlane(int defaultPlaneIndex = 0)
    {
        return PlayerPrefs.GetInt(PlayerPrefsKey, defaultPlaneIndex);
    }

    public static void SetSelectedPlane(int planeIndex)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey, Mathf.Max(0, planeIndex));
        PlayerPrefs.Save();
    }

    public static void SetSelectedPlane(int planeIndex, Sprite planeSprite)
    {
        SelectedPlaneSprite = planeSprite;
        SetSelectedPlane(planeIndex);
    }
}
