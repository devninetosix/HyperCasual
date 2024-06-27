using UnityEngine;


namespace FrenzyCircle
{
    public class ObstacleStartScale : MonoBehaviour
    {
        public float startScale;

        private void Start()
        {
            startScale = transform.localScale.y;
        }
    }
}
