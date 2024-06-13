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

    public void InitUserInfo(int todayHighScore = 0, int todayRank = 0)
    {
        TodayHighScore = todayHighScore;
        TodayRank = todayRank;

        ES3.Save(Contant.BestScore, TodayHighScore);
    }

    public void UpdateTodayBestScore(int score)
    {
        if (TodayHighScore > score)
        {
            return;
        }

        if (ES3.Load(Contant.BestScore, 0) < score)
        {
            ES3.Save(Contant.BestScore, score);
        }

        TodayHighScore = score;
        StartCoroutine(HttpManager.IEHighScoreUpdate(Id, score));
    }
}