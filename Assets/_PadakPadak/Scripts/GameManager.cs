using TMPro;
using UnityEngine;


namespace PadakPadak
{
    [DefaultExecutionOrder(-1)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private Player player;
        [SerializeField] private Spawner spawner;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private GameObject playButton;
        [SerializeField] private GameObject gameOver;

        public int Score { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }

            Instance = this;
            Application.targetFrameRate = 60;
            DontDestroyOnLoad(gameObject);
            Pause();
        }

        public void Play()
        {
            Score = 0;
            scoreText.text = Score.ToString();
            playButton.SetActive(false);
            gameOver.SetActive(false);

            ResetPipes();

            Time.timeScale = 1f;
            player.enabled = true;
        }

        private void ResetPipes()
        {
            foreach (Transform child in spawner.transform)
            {
                if (child.gameObject.activeInHierarchy)
                {
                    ObjectPool.Instance.ReleasePooledObject(child.GetComponent<Pipes>());
                }
            }
        }

        public void GameOver()
        {
            playButton.SetActive(true);
            gameOver.SetActive(true);

            Pause();
        }

        public void Pause()
        {
            Time.timeScale = 0f;
            player.enabled = false;
        }

        public void IncreaseScore()
        {
            Score++;
            scoreText.text = Score.ToString();
        }
    }
}