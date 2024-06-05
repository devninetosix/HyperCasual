using UnityEngine;
using UnityEditor.Animations;

public class ThemeManager : MonoBehaviour
{
    public static ThemeManager Instance { get; private set; }

    public Sprite[] rotCircles;
    public Sprite[] middleCircles;
    public Sprite[] arrows;
    public AnimatorController[] animControllers;

    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }

    public void SetThemes(int number)
    {
        PlayerPrefs.SetInt("Theme", number);
    }

    public Sprite GetRotCircleSprite()
    {
        return rotCircles[PlayerPrefs.GetInt("Theme", 0)];
    }

    public Sprite GetMiddleCircleSprite()
    {
        return middleCircles[PlayerPrefs.GetInt("Theme", 0)];
    }

    public Sprite GetArrowSprite()
    {
        return arrows[PlayerPrefs.GetInt("Theme", 0)];
    }

    public AnimatorController GetAnimatorController()
    {
        return animControllers[PlayerPrefs.GetInt("Theme", 0)];
    }
}