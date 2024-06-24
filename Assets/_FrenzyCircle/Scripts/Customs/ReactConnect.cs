using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class ConnectInfo
{
    public string id;
    public string nickname;
    public string todayHighestScore;
    public string todayRank;
}

public class ReactConnect : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void GameInit(string message);

    [DllImport("__Internal")]
    public static extern void SetHighScore(string message);

    private void Start()
    {
        ES3.Save(Constant.BestScore, 0);
        Utils.Log("[Start] Unity Event, Frenzy Circle Start");
        GameInit("start");
    }

    // 미들웨어 (React)에서 실행해주는 함수
    public void SetUserInfo(string json)
    {
        try
        {
            ConnectInfo response = JsonUtility.FromJson<ConnectInfo>(json);
            UserInfo.InitUserInfo(
                response.id,
                response.nickname,
                int.Parse(response.todayHighestScore),
                int.Parse(response.todayRank)
            );
            Utils.LogFormattedJson("[SetUserInfo]", json);
            Invoke(nameof(LoadScene), 0.25f);
        }
        catch (Exception ex)
        {
            // [POST] 호출 실패했을 경우, 로직 타는 부분
            Utils.Log(ex.Message, true);
            UserInfo.DummyLogin();
            Invoke(nameof(LoadScene), 0.25f);
        }
    }

    private static void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}