using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReply: MonoBehaviour
{
    private bool canClick = true;

    void OnMouseDown(){
        if(canClick) {
            canClick = false;
            GameObject.Find("GameManager").GetComponent<Menus>().Reply();
            Invoke("AllowClicking", 1);
        }
    }

    private void AllowClicking() {
        canClick = true;
    }
}
