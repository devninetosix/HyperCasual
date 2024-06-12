using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    public static int Id;
    public static string Name;
    public static int TodayHighScore;
    public static int TodayRank;

    public static List<RankInfo> WorldRankings;

    public static RankInfo UserDayRanking;
    public static RankInfo UserWeekRanking;
    public static RankInfo UserMonthRanking;

    public static UserInfo Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        Instance = this;
    }

    public void SetUserInfo(int userId, string userName, int todayHighScore, int todayRank)
    {
        Id = userId;
        Name = userName;
        TodayHighScore = todayHighScore;
        TodayRank = todayRank;

        PlayerPrefs.SetInt("BestScore", TodayHighScore);
    }

    public void UpdateTodayBestScore(int score)
    {
        if (TodayHighScore > score)
        {
            return;
        }

        PlayerPrefs.SetInt("BestScore", score);
        StartCoroutine(HttpManager.IEHighScoreUpdate(Id, score));
    }
}