using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotation : MonoBehaviour {
    
    public float rot;
    public float startScale;
    public bool rotateForward = true;
    private float timer = 0;
    private SpriteRenderer sp;

    void Start() {
        startScale = transform.localScale.y;
        rot = Random.Range(50, 100);
        sp = GetComponent<SpriteRenderer>();
        Color c = sp.color;
        c.g = 1.2f - transform.localScale.x;
        c.b = 1.2f - transform.localScale.x;
        sp.color = c;
    }

    void FixedUpdate() {
        
        if(rotateForward) {
            transform.Rotate(0, 0, rot * Time.deltaTime);
        }else {
            transform.Rotate(0, 0, -rot * Time.deltaTime);
        }

        timer += Time.deltaTime;
        if(timer > 5f) {
            timer = 0;
            Vars.obstacleScaleSpeed += 0.00001f;
        }
        
        transform.localScale  = new Vector2(transform.localScale.x - (0.0005f + Vars.obstacleScaleSpeed), transform.localScale.y - (0.0005f + Vars.obstacleScaleSpeed));

        Color c = sp.color;
        c.g = 1.2f - transform.localScale.x;
        c.b = 1.2f - transform.localScale.x;
        sp.color = c;

        if(transform.localScale.x < 0f) {
            Destroy(this.gameObject);
        }
    } 
}
