using System;
using System.Collections;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ConnectInfo
{
    public string id;
    public string name;
}

public class ReactConnect : MonoBehaviour
{
#if !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void GameInit(string message);

    private void Start()
    {
        print("CallTestScript - Start");
        GameInit("start");
    }

    public void SetUserInfo(string json)
    {
        try
        {
            ConnectInfo response = JsonUtility.FromJson<ConnectInfo>(json);
            UserInfo.Id = int.Parse(response.id);
            UserInfo.Name = response.name;
            StartCoroutine(IELoginLogic(UserInfo.Id, UserInfo.Name));

            Utils.LogFormattedJson("[SetUserInfo]", json);
        }
        catch (Exception ex)
        {
            StartCoroutine(IEDummyLogin());
        }
    }

    private IEnumerator IELoginLogic(int id, string userName)
    {
        yield return StartCoroutine(HttpManager.IELogin(id, userName));
        SceneManager.LoadScene(1);
    }

    private IEnumerator IEDummyLogin()
    {
        int dummyId = UnityEngine.Random.Range(10000, 100000);
        yield return StartCoroutine(HttpManager.IELogin(dummyId, Utils.RandomNameGenerator()));
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

#endif
}