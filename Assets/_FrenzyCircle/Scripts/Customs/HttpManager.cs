using System;
using System.Text;
using System.Collections;
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
    
    public void LoginTest()
    {
        StartCoroutine(IELogin(9876, "testpa"));
    }

    public void HighScoreTest()
    {
        StartCoroutine(IEHighScoreUpdate(9876, 9199));
    }

    public void GetUserInfoTest()
    {
        StartCoroutine(IEGetUserInfo(9876));
    }

    public void GetUserScoreTest()
    {
        StartCoroutine(IEGetUserScore(9876));
    }

    public void GetAllScoresTest(RankPeriod period)
    {
        StartCoroutine(IEGetAllRanking(period));
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
        string jsonData = $"{{ \"game_id\": {1}, \"id\": {id}, \"nickname\": \"{nickname}\" }}";
        yield return IEPostRequest(uri, jsonData, Login_ResponseHandler);
    }
    
    private static void Login_ResponseHandler(string json)
    {
        Debug.Log($"[Login] successful: {json}");
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
        Debug.Log($"[HighScoreUpdate] successful: {json}");
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
        Debug.Log($"[GetUserInfo] successful: {json}");
    }

    /// <summary>
    /// [GET] Users, 게임 스코어 정보 조회
    /// </summary>
    /// <param name="gameId">게임 ID는 1</param>
    /// <param name="id">Superbase 유저 ID</param>
    /// <returns></returns>
    public static IEnumerator IEGetUserScore(int id)
    {
        // https://crcasoukrunpqyuxtwzb.supabase.co/functions/v1/users/1231/games/1/scores
        string uri = $"{BaseUrl}/users/{id}/games/1/scores";
        yield return IEGetRequest(uri, GetUserScore_ResponseHandler);
    }

    private static void GetUserScore_ResponseHandler(string json)
    {
        Debug.Log($"[GetUserScore] successful: {json}");
    }
    
    public IEnumerator IEGetAllRanking(RankPeriod rankPeriod, int offset = 0, int limit = 100)
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
        
        // https://crcasoukrunpqyuxtwzb.supabase.co/functions/v1/games/1/rankings?period=monthly&offset=0&limit=100
        string uri = $"{BaseUrl}/games/1/rankings?period={period}&offset={offset}&limit={limit}";
        yield return IEGetRequest(uri, GetAllRanking_ResponseHandler);
    }

    private static void GetAllRanking_ResponseHandler(string json)
    {
        Debug.Log($"[GetAllRanking] successful: {json}");
    }
    
    private static IEnumerator IEGetRequest(string uri, UnityAction<string> callback)
    {
        using UnityWebRequest req = UnityWebRequest.Get(uri);
        req.SetRequestHeader("Authorization", "Bearer " + TOKEN);
        yield return req.SendWebRequest();

        if (req.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error: {req.error}");
            Debug.LogError($"Response: {req.downloadHandler.text}");
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
            Debug.LogError($"Error: {req.error}");
            Debug.LogError($"Response: {req.downloadHandler.text}");
        }
        else
        {
            callback?.Invoke(req.downloadHandler.text);
        }
    }

    private PlayerInfo JsonToPlayerInfo(string jsonText)
    {
        return JsonUtility.FromJson<PlayerInfo>(jsonText);
    }
}
