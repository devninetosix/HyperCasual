using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuTransition : MonoBehaviour {
    private float timer = 0;
    private bool up = true;
    private float alpha = 0;
    public Image img;

    void OnEnable() {
        img.enabled = true;
    }

    void Update() {
        timer += Time.deltaTime;
        if(timer >= 0.01f) {
            timer = 0;
            if(up) {
                img.raycastTarget = true;
                alpha += 0.02f;
                if(alpha >= 1f) {
                    up = false;
                    img.raycastTarget = false;
                    if(Vars.currentMenu == 0) {
                        GameObject.Find("GameManager").GetComponent<Menus> ().ShowMainMenu();
                    }else if(Vars.currentMenu == 1) {
                        GameObject.Find("GameManager").GetComponent<Menus> ().ShowGamePlayMenu();
                    }else if(Vars.currentMenu == 2) {
                        GameObject.Find("GameManager").GetComponent<Menus> ().GameReply();
                    }
                }
            }else {
                alpha -= 0.02f;
                if(alpha <= 0) {
                    up = true;
                    img.enabled = false;
                    GetComponent<MenuTransition> ().enabled = false; 
                }
            }
            img.color = new Color(0, 0, 0, alpha);
        }
    }
}
