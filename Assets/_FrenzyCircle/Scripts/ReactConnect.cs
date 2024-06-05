using UnityEngine;
using System.Runtime.InteropServices;

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

    public void SetUserId(string user)
    {
        Debug.Log($"SetUserId - user: {user}");
    }

    public void SetUserName(string name)
    {
        Debug.Log($"GetUserName - name: {name}");
    }
#endif
}