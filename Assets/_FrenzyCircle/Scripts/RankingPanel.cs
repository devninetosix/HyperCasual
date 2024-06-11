using TMPro;
using UnityEngine;

public class RankingPanel : MonoBehaviour
{
    public TextMeshProUGUI nameTextPanel;
    public TextMeshProUGUI rankTextPanel;
    public TextMeshProUGUI scoreTextPanel;

    public void SetTexts(string name, int rank, int score)
    {
        nameTextPanel.SetText(name);
        rankTextPanel.SetText(rank + "");
        scoreTextPanel.SetText(score + "");
    }
}