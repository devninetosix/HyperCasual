using DG.Tweening;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            return;
        }

        Instance = this;
    }

    public AudioSource audioSource;
    public AudioClip[] bgmClips;

    public void BGMOn()
    {
        audioSource.volume = 0;
        audioSource.Stop();
        audioSource.clip = bgmClips[0];
        audioSource.Play();
        audioSource.DOFade(1, .3f)
            .SetEase(Ease.InQuad);
    }

    public void ShopBgm()
    {
        audioSource.volume = 1;

        audioSource.DOFade(0, .3f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                audioSource.Stop();
                audioSource.clip = bgmClips[2];
                audioSource.Play();
                audioSource.DOFade(1, .3f)
                    .SetEase(Ease.InQuad);
            });
    }

    public void DefeatBgm()
    {
        audioSource.volume = 1;

        audioSource.DOFade(0, .3f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                audioSource.Stop();
                audioSource.clip = bgmClips[1];
                audioSource.Play();
                audioSource.DOFade(1, .3f)
                    .SetEase(Ease.InQuad);
            });
    }
}