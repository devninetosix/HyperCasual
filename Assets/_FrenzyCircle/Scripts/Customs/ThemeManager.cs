using System.Collections;
using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class ThemeManager : MonoBehaviour
{
    public static ThemeManager Instance { get; private set; }

    public Sprite[] rotCircles;
    public Sprite[] middleCircles;
    public Sprite[] arrows;

    public CanvasGroup cgImgFader;

    private static bool isLoaded = false;

    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }

    private IEnumerator Start()
    {
        if (isLoaded)
        {
            cgImgFader.alpha = 0f;
            yield break;
        }
        cgImgFader.alpha = 1f;
        yield return new WaitForSeconds(.1f);

        yield return cgImgFader.DOFade(0f, 1).SetEase(Ease.InQuart);
        isLoaded = true;
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