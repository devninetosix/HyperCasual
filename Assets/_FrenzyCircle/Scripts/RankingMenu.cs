using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankingMenu : MonoBehaviour
{
    public GameObject loadingIndicator;
    public RankingPanel userRankingPanel;
    public RankingPanel rankingPrefab;
    public Transform rankingParents;

    public Button[] periodButtons;

    public TextMeshProUGUI timeLeftText;

    public HttpManager.RankPeriod period;

    private void OnEnable()
    {
        ChangeRankingDate(0);
    }

    private void FixedUpdate()
    {
        timeLeftText.SetText($"TIME LEFT\n{Utils.GetTimeUntilMidnight()}");
    }

    public void ChangeRankingDate(int rankPeriod)
    {
        loadingIndicator.SetActive(true);
        period = (HttpManager.RankPeriod)rankPeriod;
        for (int i = 0; i < periodButtons.Length; i++)
        {
            periodButtons[i].image.enabled = rankPeriod == i;
            periodButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color =
                rankPeriod == i ? Color.white : Color.gray;
        }

        StartCoroutine(HttpManager.IEGetUserScore(UserRankingInfoUpdate_Callback, UserInfo.Id));
        StartCoroutine(HttpManager.IEGetAllRanking(GlobalRankingInfoUpdate_Callback, period));
    }

    private void UserRankingInfoUpdate_Callback()
    {
        switch (period)
        {
            case HttpManager.RankPeriod.Daily:
                var rankInfo = UserInfo.UserDayRanking;
                userRankingPanel.SetTexts(rankInfo.id + "", rankInfo.rank, rankInfo.score);
                break;
            case HttpManager.RankPeriod.Weekly:
                break;
            case HttpManager.RankPeriod.Monthly:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void GlobalRankingInfoUpdate_Callback()
    {
        Debug.Log("Call back!!!");
        loadingIndicator.SetActive(false);
    }
}