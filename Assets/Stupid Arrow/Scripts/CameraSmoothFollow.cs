using UnityEngine;

public class CameraSmoothFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 _velocity = Vector3.zero;
    
    private void FixedUpdate()
    {
        if (target)
        {
            Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, -10));
            transform.position = Vector3.SmoothDamp(transform.position,
                new Vector3(targetPosition.x, targetPosition.y, -10), ref _velocity, smoothTime);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(0, 0, -10), ref _velocity, 1f);
        }
    }
}