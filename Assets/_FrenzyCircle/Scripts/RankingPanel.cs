using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class RankingPanel : MonoBehaviour
{
    public TextMeshProUGUI nameTextPanel;
    public TextMeshProUGUI rankTextPanel;
    public TextMeshProUGUI scoreTextPanel;
    public TextMeshProUGUI pointTextPanel;

    public bool targetChangedSize;
    public bool ignoreSetFontSize;
    public RectTransform targetRectTr;

    public DynamicScrollContentSize dynamicScrollSize;

    [Button]
    private void OnEnable()
    {
        RectTransform target = transform.parent.parent.GetComponent<RectTransform>();

        if (targetChangedSize)
        {
            target = targetRectTr;
        }

        GetComponent<LayoutElement>().minHeight = target.rect.height / 10f;
    }

    private void Start()
    {
        if (!ignoreSetFontSize)
        {
            if (dynamicScrollSize == null)
            {
                dynamicScrollSize = transform.parent.GetComponent<DynamicScrollContentSize>();
            }

            SetFontSizes();
        }
    }

    public void SetTexts(string userName, int rank, int score)
    {
        if (userName.Length >= 12)
        {
            userName = userName[..12] + "...";
        }

        nameTextPanel.SetText(userName);
        rankTextPanel.SetText(rank == 0 ? "-" : rank + "");
        scoreTextPanel.SetText(score == 0 ? "-" : score + "");
        pointTextPanel.SetText("-");
    }

    private void SetFontSizes()
    {
        nameTextPanel.fontSize = dynamicScrollSize.basePanel.GetNameFontSize();
        rankTextPanel.fontSize = dynamicScrollSize.basePanel.GetRankTextSize();
        scoreTextPanel.fontSize = dynamicScrollSize.basePanel.GetScoreTextSize();
        pointTextPanel.fontSize = dynamicScrollSize.basePanel.GetPointTextSize();
    }

    public float GetNameFontSize()
    {
        return nameTextPanel.fontSize;
    }

    public float GetRankTextSize()
    {
        return rankTextPanel.fontSize;
    }

    public float GetScoreTextSize()
    {
        return scoreTextPanel.fontSize;
    }

    public float GetPointTextSize()
    {
        return pointTextPanel.fontSize;
    }
}