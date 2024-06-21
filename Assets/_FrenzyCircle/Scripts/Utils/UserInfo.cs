using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
#endif

[System.Serializable]
public class UserScore
{
    public string userId;
    public int score;
}

public class UserInfo : MonoBehaviour
{
    public static string Id { get; private set; }
    public static string NickName { get; private set; }
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

    public static void InitUserInfo(string userId, string userName, int todayHighScore = 0, int todayRank = 0)
    {
        Id = userId;
        NickName = userName;
        TodayHighScore = todayHighScore;
        TodayRank = todayRank;

        ES3.Save(Constant.BestScore, TodayHighScore);
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
    public static void DummyLogin()
    {
        InitUserInfo(
            Random.Range(10000, 100000).ToString(),
            Utils.RandomNameGenerator()
        );
    }

#if UNITY_EDITOR
    [Button(ButtonSizes.Large)]
    private void GoToGameScene()
    {
        DummyLogin();
        SceneManager.LoadScene(1);
    }
#endif

    public static void UpdateTodayBestScore(int score)
    {
        if (TodayHighScore > score)
        {
            return;
        }

        if (ES3.Load(Constant.BestScore, 0) < score)
        {
            ES3.Save(Constant.BestScore, score);
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