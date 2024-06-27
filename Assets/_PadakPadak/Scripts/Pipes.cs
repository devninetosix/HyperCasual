using UnityEngine;


namespace PadakPadak
{
    public class Pipes : MonoBehaviour
    {
        public Transform top;
        public Transform bottom;
        public float speed = 5f;
        public float gap = 3f;

        private float _leftEdge;

        private void OnEnable()
        {
            ResetPipes();
            _leftEdge = Camera.main!.ScreenToWorldPoint(Vector3.zero).x - 1f;
            top.position += Vector3.up * gap / 2;
            bottom.position += Vector3.down * gap / 2;
        }

        private void Update()
        {
            transform.position += speed * Time.deltaTime * Vector3.left;

            if (transform.position.x < _leftEdge)
            {
                ObjectPool.Instance.ReleasePooledObject(this);
            }
        }

        private void ResetPipes()
        {
            top.localPosition = new Vector3(0f, 2.25f, 0f);
            bottom.localPosition = new Vector3(0f, -2.25f, 0f);
        }
    }
}
