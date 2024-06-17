using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif

[System.Serializable]
public class UserScore
{
    public int userId;
    public int score;
}

public class UserInfo : MonoBehaviour
{
    public static int Id { get; private set; }
    public static string Name { get; private set; }
    public static int TodayHighScore { get; private set; }
    public static int TodayRank { get; private set; }

    public static List<RankInfo> GlobalRankings { get; private set; }

    public static RankInfo UserDayRanking { get; private set; }
    public static RankInfo UserWeekRanking { get; private set; }
    public static RankInfo UserMonthRanking { get; private set; }

    public static UserInfo Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    public static void InitUserInfo(int userId, string userName, int todayHighScore = 0, int todayRank = 0)
    {
        Id = userId;
        Name = userName;
        TodayHighScore = todayHighScore;
        TodayRank = todayRank;

        ES3.Save(Contant.BestScore, TodayHighScore);
    }

    public static void UpdateUserRanking(int todayHighScore = -1, int todayRank = -1)
    {
        TodayHighScore = todayHighScore == -1 ? TodayHighScore : todayHighScore;
        TodayRank = todayRank == -1 ? TodayRank : todayRank;
    }

    public static void SetUserRanking(RankInfo dayRanking, RankInfo weekRanking, RankInfo monthRanking)
    {
        UserDayRanking = dayRanking;
        UserWeekRanking = weekRanking;
        UserMonthRanking = monthRanking;
    }

    public static void SetGlobalRanking(List<RankInfo> globalRanking)
    {
        GlobalRankings = globalRanking;
    }

    // 비회원 로그인!!
#if UNITY_EDITOR
    [Button(ButtonSizes.Large)]
#endif
    public static void DummyLogin()
    {
        InitUserInfo(
            Random.Range(10000, 100000),
            Utils.RandomNameGenerator()
        );
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

        UserScore userScore = new UserScore()
        {
            userId = Id,
            score = score
        };

        string json = JsonUtility.ToJson(userScore);
        ReactConnect.SetHighScore(json);
    }
}