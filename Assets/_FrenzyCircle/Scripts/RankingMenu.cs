using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RankingMenu : MonoBehaviour
{
    public GameObject loadingIndicator;
    public RankingPanel userRankingPanel;
    public RankingPanel rankingPrefab;
    public Transform rankingParents;
    public GameObject noDataImage;

    public Button[] periodButtons;
    public TextMeshProUGUI timeLeftText;
    public HttpManager.RankPeriod period;

    private readonly List<RankingPanel> _rankingInstances = new();
    private readonly Queue<RankingPanel> _rankingPool = new();

    private void OnEnable()
    {
        ChangeRankingDate(0);
    }

    private void OnDisable()
    {
        foreach (var instance in _rankingInstances)
        {
            instance.gameObject.SetActive(false);
            _rankingPool.Enqueue(instance);
        }

        _rankingInstances.Clear();
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
        RankInfo rankInfo;

        switch (period)
        {
            case HttpManager.RankPeriod.Daily:
                rankInfo = UserInfo.UserDayRanking;
                userRankingPanel.SetTexts(Utils.RandomNameGenerator(), rankInfo.rank, rankInfo.score);
                break;
            case HttpManager.RankPeriod.Weekly:
                rankInfo = UserInfo.UserWeekRanking;
                userRankingPanel.SetTexts(Utils.RandomNameGenerator(), rankInfo.rank, rankInfo.score);
                break;
            case HttpManager.RankPeriod.Monthly:
                rankInfo = UserInfo.UserMonthRanking;
                userRankingPanel.SetTexts(Utils.RandomNameGenerator(), rankInfo.rank, rankInfo.score);
                break;
            default:
                return;
        }
    }

    private void GlobalRankingInfoUpdate_Callback()
    {
        List<RankInfo> globalRankings = UserInfo.WorldRankings;

        foreach (var oldRank in _rankingInstances)
        {
            oldRank.gameObject.SetActive(false);
            _rankingPool.Enqueue(oldRank);
        }

        _rankingInstances.Clear();

        foreach (var newRank in globalRankings)
        {
            RankingPanel instance;
            if (_rankingPool.Count > 0)
            {
                instance = _rankingPool.Dequeue();
                instance.gameObject.SetActive(true);
            }
            else
            {
                instance = Instantiate(rankingPrefab, rankingParents);
            }

            // todo: 이거 실제 유저 이름으로 바꿔야함
            instance.SetTexts(Utils.RandomNameGenerator(), newRank.rank, newRank.score);

            _rankingInstances.Add(instance);
        }

        noDataImage.SetActive(globalRankings.Count == 0);
        loadingIndicator.SetActive(false);
    }
}