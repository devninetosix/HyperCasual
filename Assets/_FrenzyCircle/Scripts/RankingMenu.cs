using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RankingMenu : MonoBehaviour
{
    public RankingPanel userRankingPanel;
    public RankingPanel rankingPrefab;
    public Transform rankingParents;
    public CanvasGroup cgNoData;

    public Button[] periodButtons;
    public TextMeshProUGUI timeLeftText;
    public HttpManager.RankPeriod period;
    public CanvasGroup loadingData;
    public CanvasGroup noData;

    private readonly List<RankingPanel> _rankingInstances = new();

    private void OnEnable()
    {
        ChangeRankingDate(0);
    }

    private void OnDisable()
    {
        while (rankingParents.childCount != 0)
        {
            Destroy(rankingParents.GetChild(0));
        }

        _rankingInstances.Clear();
    }

    private void FixedUpdate()
    {
        timeLeftText.SetText($"TIME LEFT\n{Utils.GetTimeUntilMidnight()}");
    }

    public void ChangeRankingDate(int rankPeriod)
    {
        cgNoData.alpha = 1f;
        loadingData.alpha = 1f;
        noData.alpha = 0f;
        
        period = (HttpManager.RankPeriod)rankPeriod;
        for (int i = 0; i < periodButtons.Length; i++)
        {
            periodButtons[i].image.enabled = rankPeriod == i;
            periodButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color =
                rankPeriod == i ? Color.white : Color.gray;
        }

        StartCoroutine(IEChain());
        return;

        IEnumerator IEChain()
        {
            yield return StartCoroutine(HttpManager.IEGetUserScore(UserRankingInfoUpdate_Callback, UserInfo.Id));
            StartCoroutine(HttpManager.IEGetAllRanking(GlobalRankingInfoUpdate_Callback, period));
        }
    }

    private void UserRankingInfoUpdate_Callback()
    {
        RankInfo rankInfo;

        switch (period)
        {
            case HttpManager.RankPeriod.Daily:
                rankInfo = UserInfo.UserDayRanking;
                userRankingPanel.SetTexts(UserInfo.Name, rankInfo.rank, rankInfo.score);
                break;
            case HttpManager.RankPeriod.Weekly:
                rankInfo = UserInfo.UserWeekRanking;
                userRankingPanel.SetTexts(UserInfo.Name, rankInfo.rank, rankInfo.score);
                break;
            case HttpManager.RankPeriod.Monthly:
                rankInfo = UserInfo.UserMonthRanking;
                userRankingPanel.SetTexts(UserInfo.Name, rankInfo.rank, rankInfo.score);
                break;
            default:
                return;
        }
    }

    private void GlobalRankingInfoUpdate_Callback()
    {
        List<RankInfo> globalRankings = UserInfo.WorldRankings;

        globalRankings.Sort((a, b) => a.rank.CompareTo(b.rank));

        foreach (var oldRank in _rankingInstances)
        {
            oldRank.gameObject.SetActive(false);
        }

        _rankingInstances.Clear();

        foreach (var newRank in globalRankings)
        {
            RankingPanel instance = Instantiate(rankingPrefab, rankingParents);

            instance.SetTexts(newRank.nickname, newRank.rank, newRank.score);

            _rankingInstances.Add(instance);
        }

        loadingData.alpha = 0f;
        cgNoData.alpha = globalRankings.Count == 0 ? 1f : 0f;
        noData.alpha = globalRankings.Count == 0 ? 1f : 0f;
    }
}