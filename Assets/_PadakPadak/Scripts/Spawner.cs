using UnityEngine;


namespace PadakPadak
{
    public class Spawner : MonoBehaviour
    {
        public float spawnRate = 1f;
        public float minHeight = -1f;
        public float maxHeight = 2f;
        public float verticalGap = 3f;

        private void OnEnable()
        {
            InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
        }

        private void OnDisable()
        {
            CancelInvoke(nameof(Spawn));
        }

        private void Spawn()
        {
            Pipes pipes = ObjectPool.Instance.GetPooledObject();
            if (pipes != null)
            {
                pipes.transform.position = transform.position + Vector3.up * Random.Range(minHeight, maxHeight);
                pipes.gap = verticalGap;
            }
        }
    }
}