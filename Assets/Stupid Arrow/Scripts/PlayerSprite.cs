using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    public Sprite[] sprite;
    
    void Start() {
        GetComponent<SpriteRenderer> ().sprite = sprite[PlayerPrefs.GetInt("Player", 0)];
    }
}
