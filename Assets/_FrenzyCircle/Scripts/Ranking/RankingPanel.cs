using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        }
    }

    private void Update()
    {
        if (ignoreSetFontSize)
        {
            return;
        }

        if (nameTextPanel.fontSize - dynamicScrollSize.basePanel.GetNameFontSize() < 0.01f)
        {
            SetFontSizes();
        }
    }

    public void SetTexts(string userName, int rank, int score)
    {
        if (!string.IsNullOrEmpty(userName) && userName.Length >= 12)
        {
            userName = userName[..12] + "...";
        }
        else if (userName == null)
        {
            userName = "-";
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
        TurnOnOff(false);
        TurnOnOff(true);
    }

    private void TurnOnOff(bool turned)
    {
        nameTextPanel.enabled = turned;
        rankTextPanel.enabled = turned;
        scoreTextPanel.enabled = turned;
        pointTextPanel.enabled = turned;
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