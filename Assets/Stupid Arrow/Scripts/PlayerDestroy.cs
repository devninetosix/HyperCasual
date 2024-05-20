using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestroy : MonoBehaviour
{
    private float timer = 0;
    private float scale = 1;

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 0.01f) {
            timer = 0;
            scale -= 0.05f;
            transform.localScale = new Vector2(scale, scale);
            if(scale <= 0) {
                Destroy(this.gameObject);
            }
        }    
    }
}
