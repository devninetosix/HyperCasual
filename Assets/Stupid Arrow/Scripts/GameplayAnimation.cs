using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayAnimation : MonoBehaviour
{
    private float speed = 0;
    public PlayerLogic pl;
    public CircleCollider2D playersCollider;

    void FixedUpdate() {
        speed += 0.001f;
        transform.localScale  = new Vector2(transform.localScale.x + (0.0005f + speed), transform.localScale.y + (0.0005f + speed));
        if(transform.localScale.x >= 1) {
            transform.localScale = new Vector2(1, 1);
            pl.enabled = true;
            playersCollider.enabled = true;
            GetComponent<GameplayAnimation>().enabled = false;
        }
    }
}
