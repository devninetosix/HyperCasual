using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {

    private bool canClick = true;

    void OnMouseDown(){
        if(canClick) {
            canClick = false;
            GameObject.Find("GameManager").GetComponent<Menus>().StartTheGame();
            Invoke("AllowClicking", 1);
        }
    }

    private void AllowClicking() {
        canClick = true;
    }
}
