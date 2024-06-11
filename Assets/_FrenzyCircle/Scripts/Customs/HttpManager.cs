using System.Text;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class HttpManager : MonoBehaviour
{
    private const string TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9." +
                                 "eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImNyY" +
                                 "2Fzb3VrcnVucHF5dXh0d3piIiwicm9sZSI6In" +
                                 "NlcnZpY2Vfcm9sZSIsImlhdCI6MTcxNjI2ODM" +
                                 "yNywiZXhwIjoyMDMxODQ0MzI3fQ.efA7_0nxu" +
                                 "y7yB5_IRSAqUPw9uefjYuEADU4yCpHyFwY";

    private const string BaseUrl = "https://crcasoukrunpqyuxtwzb.supabase.co/functions/v1";

    public enum RankPeriod
    {
        Daily,
        Weekly,
        Monthly,
    }

    /// <summary>
    /// [GET] Users, 유저 정보 조회
    /// </summary>
    /// <param name="id">Superbase 유저 ID</param>
    /// <returns></returns>
    public static IEnumerator IEGetUserInfo(int id)
    {
        string uri = $"{BaseUrl}/users/{id}";
        yield return IEGetRequest(uri, GetUserInfo_ResponseHandler);
    }

    private static void GetUserInfo_ResponseHandler(string json)
    {
        Utils.LogFormattedJson("[GetUserInfo]", json);
    }

    [Button(ButtonSizes.Large)]
    public void GetUserInfoTest(int id = 100)
    {
        StartCoroutine(IEGetUserInfo(id));
    }

    /// <summary>
    /// [GET] Users, 유저 게임 스코어 정보 조회
    /// </summary>
    /// <param name="gameId">게임 ID는 1</param>
    /// <param name="id">Superbase 유저 ID</param>
    /// <returns></returns>
    public static IEnumerator IEGetUserScore(int id)
    {
        string uri = $"{BaseUrl}/users/{id}/games/1/scores";
        yield return IEGetRequest(uri, GetUserScore_ResponseHandler);
    }

    private static void GetUserScore_ResponseHandler(string json)
    {
        Utils.LogFormattedJson("[GetUserScore]", json);
    }

    [Button(ButtonSizes.Large)]
    public void GetUserScoreTest(int id = 100)
    {
        StartCoroutine(IEGetUserScore(id));
    }


    /// <summary>
    /// [POST] Auth, 로그인
    /// </summary>
    /// <param name="gameId">게임 아이디는 1</param>
    /// <param name="id">Superbase 유저 ID</param>
    /// <param name="nickname">텔레그램 유저 이름</param>
    /// <returns></returns>
    public static IEnumerator IELogin(int id, string nickname)
    {
        string uri = $"{BaseUrl}/auth/login";
        string jsonData = $"{{ \"gameId\": {1}, \"id\": {id}, \"nickname\": \"{nickname}\" }}";
        yield return IEPostRequest(uri, jsonData, Login_ResponseHandler);
    }

    private static void Login_ResponseHandler(string json)
    {
        ApiResponse<UserData> response = JsonUtility.FromJson<ApiResponse<UserData>>(json);
        UserInfo.Instance.SetUserInfo(
            response.data.id,
            response.data.nickname,
            response.data.todayHighestScore,
            response.data.todayRank
        );

        Utils.LogFormattedJson("[Login]", json);
    }

    [Button(ButtonSizes.Large)]
    public void LoginTest(int id = 100, string nickname = "aespablo")
    {
        StartCoroutine(IELogin(id, nickname));
    }

    /// <summary>
    /// [POST] Games, 유저 최고 스코어 등록
    /// </summary>
    /// <param name="gameId">게임 아이디는 1</param>
    /// <param name="id">Superbase 유저 ID</param>
    /// <param name="score">새로 등록할 점수</param>
    /// <returns></returns>
    public static IEnumerator IEHighScoreUpdate(int id, int score)
    {
        string uri = $"{BaseUrl}/games/1/scores";
        string jsonData = $"{{ \"userId\": {id}, \"score\": \"{score}\" }}";
        yield return IEPostRequest(uri, jsonData, HighScoreUpdate_ResponseHandler);
    }

    private static void HighScoreUpdate_ResponseHandler(string json)
    {
        Utils.LogFormattedJson("[HighScoreUpdate]", json);
    }

    [Button(ButtonSizes.Large)]
    public void HighScoreTest(int id = 100, int score = 1000)
    {
        StartCoroutine(IEHighScoreUpdate(id, score));
    }


    /// <summary>
    /// [GET] Games, 전체 랭킹 조회하기
    /// </summary>
    /// <param name="rankPeriod">일간, 주간, 월간</param>
    /// <param name="offset">시작점</param>
    /// <param name="limit">으로부터 개수</param>
    /// <returns></returns>
    public static IEnumerator IEGetAllRanking(RankPeriod rankPeriod, int offset = 0, int limit = 100)
    {
        string period = string.Empty;

        switch (rankPeriod)
        {
            case RankPeriod.Daily:
            default:
                period = "daily";
                break;
            case RankPeriod.Weekly:
                period = "weekly";
                break;
            case RankPeriod.Monthly:
                period = "monthly";
                break;
        }

        string uri = $"{BaseUrl}/games/1/rankings?period={period}&offset={offset}&limit={limit}";
        yield return IEGetRequest(uri, GetAllRanking_ResponseHandler);
    }

    private static void GetAllRanking_ResponseHandler(string json)
    {
        Utils.LogFormattedJson("[GetAllRanking]", json);
    }

    [Button(ButtonSizes.Large)]
    public void GetAllScoresTest(RankPeriod period = RankPeriod.Daily, int offset = 0, int limit = 100)
    {
        StartCoroutine(IEGetAllRanking(period, offset, limit));
    }

    // HTTP /////////////////////////////////////////////////////////////////////

    private static IEnumerator IEGetRequest(string uri, UnityAction<string> callback)
    {
        using UnityWebRequest req = UnityWebRequest.Get(uri);
        req.SetRequestHeader("Authorization", "Bearer " + TOKEN);
        yield return req.SendWebRequest();

        if (req.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error: {req.error}\n" +
                           $"Response: {req.downloadHandler.text}");
        }
        else
        {
            callback?.Invoke(req.downloadHandler.text);
        }
    }

    private static IEnumerator IEPostRequest(string uri, string jsonData, UnityAction<string> callback)
    {
        using UnityWebRequest req = new UnityWebRequest(uri, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        req.uploadHandler = new UploadHandlerRaw(bodyRaw);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");
        req.SetRequestHeader("Authorization", "Bearer " + TOKEN);

        yield return req.SendWebRequest();

        if (req.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error: {req.error}\n" +
                           $"Response: {req.downloadHandler.text}");
        }
        else
        {
            callback?.Invoke(req.downloadHandler.text);
        }
    }
}