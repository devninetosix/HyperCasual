using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class HttpManager : MonoBehaviour
{
    public static HttpManager Instance { get; private set; }

    private const string TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9." +
                                 "eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImNyY" +
                                 "2Fzb3VrcnVucHF5dXh0d3piIiwicm9sZSI6In" +
                                 "NlcnZpY2Vfcm9sZSIsImlhdCI6MTcxNjI2ODM" +
                                 "yNywiZXhwIjoyMDMxODQ0MzI3fQ.efA7_0nxu" +
                                 "y7yB5_IRSAqUPw9uefjYuEADU4yCpHyFwY";

    private const string BaseUrl = "https://crcasoukrunpqyuxtwzb.supabase.co/functions/v1";
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        Instance = this;
    }

    public void LoginTest()
    {
        StartCoroutine(Login(1, 9876, "testpa"));
    }

    public void HighScoreTest()
    {
        StartCoroutine(HighScoreUpdate(1, 9876, 9199));
    }

    public void GetUserInfoTest()
    {
        StartCoroutine(GetUserInfo(9876));
    }

    public void GetUserScoreTest()
    {
        StartCoroutine(GetUserScore(1, 9876));
    }

    public IEnumerator Login(int gameId, int id, string nickname)
    {
        string uri = $"{BaseUrl}/auth/login";
        string jsonData = $"{{ \"game_id\": {gameId}, \"id\": {id}, \"nickname\": \"{nickname}\" }}";
        yield return PostRequest(uri, jsonData, Login_ResponseHandler);
    }
    
    private void Login_ResponseHandler(string json)
    {
        Debug.Log($"[Login_ResponseHandler] successful: {json}");
    }

    public IEnumerator HighScoreUpdate(int gameId, int id, int score)
    {
        string uri = $"{BaseUrl}/scores";
        string jsonData = $"{{ \"game_id\": {gameId}, \"id\": {id}, \"score\": \"{score}\" }}";
        yield return PostRequest(uri, jsonData, HighScoreUpdate_ResponseHandler);
    }

    private void HighScoreUpdate_ResponseHandler(string json)
    {
        Debug.Log($"[HighScoreUpdate_ResponseHandler] successful: {json}");
    }

    public IEnumerator GetUserInfo(int id)
    {
        string uri = $"{BaseUrl}/users/{id}";
        yield return GetRequest(uri, GetUserInfo_ResponseHandler);
    }

    private void GetUserInfo_ResponseHandler(string json)
    {
        Debug.Log($"[GetUserInfo_ResponseHandler] successful: {json}");
    }

    public IEnumerator GetUserScore(int gameId, int id)
    {
        string uri = $"{BaseUrl}/users/{id}/scores?gameId={gameId}";
        yield return GetRequest(uri, GetUserScore_ResponseHandler);
    }

    private void GetUserScore_ResponseHandler(string json)
    {
        Debug.Log($"[GetUserScore_ResponseHandler] successful: {json}");
    }
    
    private IEnumerator GetRequest(string uri, UnityAction<string> callback)
    {
        using UnityWebRequest req = UnityWebRequest.Get(uri);
        req.SetRequestHeader("Authorization", "Bearer " + TOKEN);
        yield return req.SendWebRequest();

        if (req.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error: {req.error}");
            Debug.LogError($"Response Code: {req.responseCode}");
            Debug.LogError($"Response: {req.downloadHandler.text}");
        }
        else
        {
            callback?.Invoke(req.downloadHandler.text);
        }
    }

    private IEnumerator PostRequest(string uri, string jsonData, UnityAction<string> callback)
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
            Debug.LogError($"Response Code: {req.responseCode}");
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
