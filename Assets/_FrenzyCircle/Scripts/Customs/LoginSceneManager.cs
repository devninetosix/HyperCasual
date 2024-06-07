using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginSceneManager : MonoBehaviour
{
    [SerializeField] private int userId = 100;
    [SerializeField] private string userName = "aespablo";
    
    // 에디터에서만 돌아가는 녀석
#if UNITY_EDITOR
    private IEnumerator Start()
    {
        yield return StartCoroutine(HttpManager.IELogin(userId, userName));
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
#endif
}
