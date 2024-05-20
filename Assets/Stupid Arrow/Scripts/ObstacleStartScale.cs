using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleStartScale : MonoBehaviour {
    public float startScale;

    void Start() {
        startScale = transform.localScale.y;
    }
}
