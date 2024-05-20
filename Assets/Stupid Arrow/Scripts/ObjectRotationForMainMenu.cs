using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotationForMainMenu : MonoBehaviour
{
    private float rot;
    private float startScale;
    public bool rotateForward = true;
    private SpriteRenderer sp;
    private float speed = 0;

    void Awake() {
        startScale = transform.localScale.y;
        rot = Random.Range(50, 100);
        sp = GetComponent<SpriteRenderer>();
    }

    void OnEnable() {
        speed = 0;
        transform.localScale = new Vector2(startScale, startScale);
    }

    void FixedUpdate() {
        
        if(rotateForward) {
            transform.Rotate(0, 0, rot * Time.deltaTime);
        }else {
            transform.Rotate(0, 0, -rot * Time.deltaTime);
        }

        if(Vars.startGame) {
            speed += 0.001f;
        }
 
        transform.localScale  = new Vector2(transform.localScale.x - (0.0005f + speed), transform.localScale.y - (0.0005f + speed));

        if(transform.localScale.x < 0f) {
            if(Vars.startGame) Destroy(this.gameObject);
            transform.localScale  = new Vector2(0.75f, 0.75f);
            Vars.mainMenuCircles++;
            sp.sortingOrder = -Vars.mainMenuCircles;

        }

        Color c = sp.color;
        c.g = 1.2f - transform.localScale.x;
        c.b = 1.2f - transform.localScale.x;
        sp.color = c;
    }

}
