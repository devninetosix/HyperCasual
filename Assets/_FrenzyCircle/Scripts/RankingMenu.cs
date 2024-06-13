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
    public Transform trashBox;
    public CanvasGroup cgNoData;

    public Button[] periodButtons;
    public TextMeshProUGUI timeLeftText;
    public HttpManager.RankPeriod period;
    public CanvasGroup loadingData;
    public CanvasGroup noData;

    private readonly List<RankingPanel> _rankingInstances = new();

    private void OnEnable()
    {
        period = HttpManager.RankPeriod.Monthly;
        ChangeRankingDate(0);
    }

    private void OnDisable()
    {
        foreach (var instance in _rankingInstances)
        {
            instance.gameObject.SetActive(false);
            instance.transform.SetParent(trashBox);
        }

        _rankingInstances.Clear();
    }

    private void FixedUpdate()
    {
        timeLeftText.SetText($"TIME LEFT\n{Utils.GetTimeUntilMidnight()}");
    }

    public void ChangeRankingDate(int rankPeriod)
    {
        if (period == (HttpManager.RankPeriod)rankPeriod)
        {
            return;
        }

        period = (HttpManager.RankPeriod)rankPeriod;

        cgNoData.alpha = 1f;
        loadingData.alpha = 1f;
        noData.alpha = 0f;

        for (int i = 0; i < periodButtons.Length; i++)
        {
            periodButtons[i].image.enabled = rankPeriod == i;
            periodButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color =
                rankPeriod == i ? Color.white : Color.gray;
        }

        userRankingPanel.SetTexts(UserInfo.Name);
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

        globalRankings.RemoveAll(rankInfo => rankInfo.score == 0);
        globalRankings.Sort((a, b) => a.rank.CompareTo(b.rank));

        foreach (var oldRank in _rankingInstances)
        {
            oldRank.gameObject.SetActive(false);
            oldRank.transform.SetParent(trashBox);
        }

        _rankingInstances.Clear();

        for (var i = 0; i < globalRankings.Count; i++)
        {
            var newRank = globalRankings[i];
            RankingPanel instance = Instantiate(rankingPrefab, rankingParents);

            instance.SetTexts(newRank.nickname, i + 1, newRank.score);

            _rankingInstances.Add(instance);

            if (i >= 100)
            {
                break;
            }
        }

        loadingData.alpha = 0f;
        cgNoData.alpha = globalRankings.Count == 0 ? 1f : 0f;
        noData.alpha = globalRankings.Count == 0 ? 1f : 0f;
    }
}