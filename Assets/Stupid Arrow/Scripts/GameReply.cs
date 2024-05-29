using UnityEngine;

public class GameReply : MonoBehaviour
{
    private bool _canClick = true;

    private void OnMouseDown()
    {
        if (!_canClick)
        {
            return;
        }

        _canClick = false;
        GameObject.Find("GameManager").GetComponent<Menus>().Reply();
        Invoke("AllowClicking", 1);
    }

    private void AllowClicking()
    {
        _canClick = true;
    }
}