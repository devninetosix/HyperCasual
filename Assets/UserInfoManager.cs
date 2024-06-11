using UnityEngine;

public class UserInfoManager : MonoBehaviour
{
    private void Start()
    {
        Debug.Log($"Id: {UserInfo.Id}");
        Debug.Log($"Name: {UserInfo.Name}");
        Debug.Log($"TodayRank: {UserInfo.TodayRank}");
        Debug.Log($"TodayHighScore: {UserInfo.TodayHighScore}");
    }
}