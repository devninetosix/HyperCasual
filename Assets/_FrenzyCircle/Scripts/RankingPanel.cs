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
    public RectTransform targetRectTr;

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

    public void SetTexts(string userName, int rank, int score)
    {
        nameTextPanel.SetText(userName);
        rankTextPanel.SetText(rank == 0 ? "-" : rank + "");
        scoreTextPanel.SetText(score == 0 ? "-" : score + "");
        pointTextPanel.SetText("-");
    }
}