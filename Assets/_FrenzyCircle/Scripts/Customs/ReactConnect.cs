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
}

public class ReactConnect : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void GameInit(string message);

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
            UserInfo.Name = response.name;
            UserInfo.Id = int.Parse(response.id);
            StartCoroutine(IELoginLogic(UserInfo.Id, UserInfo.Name));
            Utils.LogFormattedJson("[SetUserInfo]", json);
        }
        catch (Exception ex)
        {
            // [POST] 호출 실패했을 경우, 로직 타는 부분
            Utils.Log(ex.Message, true);
            StartCoroutine(IEDummyLogin());
        }
    }

    // 비회원 로그인!!
    private IEnumerator IEDummyLogin()
    {
        yield return StartCoroutine(IELoginLogic(UserInfo.Id, UserInfo.Name));
    }

    // 전반적인 로그인 로직
    // 로그인 후 바로 다음씬으로 넘어간다.
    private IEnumerator IELoginLogic(int id, string userName)
    {
        yield return StartCoroutine(HttpManager.IELogin(id, userName));
        SceneManager.LoadScene(1);
    }
}