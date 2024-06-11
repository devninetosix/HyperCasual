using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class ThemeManager : MonoBehaviour
{
    public static ThemeManager Instance { get; private set; }

    public Sprite[] rotCircles;
    public Sprite[] middleCircles;
    public Sprite[] arrows;

    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }

    public static void SetThemes(int number)
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

    [Button(ButtonSizes.Large)]
    public void InitPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("totalPoints", 100000);

        SceneManager.LoadScene(1);
    }
}