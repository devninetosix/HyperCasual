using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoothFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    
    void FixedUpdate() {
        if(target != null) {
            Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, -10));
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(targetPosition.x, targetPosition.y, -10), ref velocity, smoothTime);
        }else {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(0, 0, -10), ref velocity, 1f);
        }
    }

}
