using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ConnectInfo
{
    public string id;
    public string name;
}

public class ReactConnect : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void GameInit(string message);

    private void Awake()
    {
        UserInfo.Name = Utils.RandomNameGenerator();
        UserInfo.Id = UnityEngine.Random.Range(1000000, 10000000);
    }

    private void Start()
    {
        ES3.Save(Contant.BestScore, 0);
        Utils.Log("[Start] Unity Event, Frenzy Circle Start");
        GameInit("start");
    }

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
            StartCoroutine(IEDummyLogin());
        }
    }

    private IEnumerator IEDummyLogin()
    {
        yield return StartCoroutine(IELoginLogic(UserInfo.Id, UserInfo.Name));
    }

    private IEnumerator IELoginLogic(int id, string userName)
    {
        yield return StartCoroutine(HttpManager.IELogin(id, userName));
        SceneManager.LoadScene(1);
    }
}