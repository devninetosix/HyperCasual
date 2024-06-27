using System;


namespace FrenzyCircle
{
    [Serializable]
    public class ApiResponse<T>
    {
        public int resultCode;
        public string message;
        public T data;
    }

    [Serializable]
    public class UserData
    {
        public string id;
        public string nickname;
        public int todayHighestScore;
        public int todayRank;
    }

    [Serializable]
    public class UserRank
    {
        public RankInfo dayRanking;
        public RankInfo weekRanking;
        public RankInfo monthRanking;
    }

    [Serializable]
    public class RankInfo
    {
        public string id;
        public string userId;
        public string nickname;
        public int score;
        public int rank;
        public string createdAt;
    }
}