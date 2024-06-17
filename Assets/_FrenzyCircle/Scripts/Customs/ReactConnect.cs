using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class ConnectInfo
{
    public string id;
    public string name;
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
        ES3.Save(Contant.BestScore, 0);
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
                int.Parse(response.id),
                response.name,
                int.Parse(response.todayHighestScore),
                int.Parse(response.todayRank)
            );
            Utils.LogFormattedJson("[SetUserInfo]", json);
            StartCoroutine(IELoadScene());
        }
        catch (Exception ex)
        {
            // [POST] 호출 실패했을 경우, 로직 타는 부분
            Utils.Log(ex.Message, true);
            UserInfo.DummyLogin();
            StartCoroutine(IELoadScene());
        }
    }

    private static IEnumerator IELoadScene()
    {
        yield return new WaitForSeconds(.1f);
        SceneManager.LoadScene(1);
    }
}