using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerLogic : MonoBehaviour
{
    public float fixedScale = 1;
    public GameObject parent;
    public Camera cam;
    private AudioSource _lineChangeSound;
    private TextMeshProUGUI _score;

    public GameObject circlePrefab;

    private void Awake()
    {
        _lineChangeSound = GameObject.Find("LineChangeSound").GetComponent<AudioSource>();
        _score = GameObject.Find("Canvas")
            .transform.Find("GameMenu")
            .transform.Find("TopMenu")
            .transform.Find("ScoreText")
            .GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        transform.localScale = new Vector2(
            fixedScale / parent.transform.localScale.x,
            fixedScale / parent.transform.localScale.y
        );
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject(-1) ||
            EventSystem.current.IsPointerOverGameObject(0) ||
            EventSystem.current.IsPointerOverGameObject(1))
        {
            return;
        }

        GameLogic();
    }

    private void GameLogic()
    {
        Vars.Obstacle++;

        GameObject circle = GameObject.Find("circle" + Vars.Obstacle);
        float circleScale = circle.transform.localScale.x;

        Vars.NumberOfCircles++;
        GameObject newCircle = Instantiate(circlePrefab, GameObject.Find("Gameplay").transform, true);
        GameObject previousCircle = GameObject.Find("circle" + (Vars.NumberOfCircles - 1));
        GameObject circleToJump = GameObject.Find("circle" + (Vars.Obstacle - 1));

        float circleToJumpScale = circleToJump.transform.localScale.x;
        circle.transform.localScale = new Vector2(0.2f, 0.2f);
        circleToJump.transform.localScale = new Vector2(0.2f, 0.2f);
        transform.parent = circle.transform;
        parent = circle;
        circle.transform.localScale = new Vector2(circleScale, circleScale);
        circleToJump.transform.localScale = new Vector2(circleToJumpScale, circleToJumpScale);

        if (newCircle)
        {
            newCircle.name = "circle" + Vars.NumberOfCircles;
            SpriteRenderer newCircleSpriteRenderer = newCircle.GetComponent<SpriteRenderer>();
            newCircleSpriteRenderer.sortingOrder = -Vars.NumberOfCircles;
            newCircle.transform.localScale = new Vector3(
                previousCircle.transform.localScale.x + 0.05f,
                previousCircle.transform.localScale.y + 0.05f,
                1
            );

            if (Vars.NumberOfCircles % 2 == 0)
            {
                newCircle.GetComponent<ObstacleRotation>().rotateForward = false;
                newCircle.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        Vector2 playerPos = cam.WorldToScreenPoint(transform.position);
        StartCoroutine(CheckPixelColor((int)playerPos.x, (int)playerPos.y));
    }

    private IEnumerator CheckPixelColor(int x, int y)
    {
        Texture2D tex = new Texture2D(1, 1, TextureFormat.RGB24, false);
        yield return new WaitForEndOfFrame();
        tex.ReadPixels(new Rect(x, y, x, y), 0, 0, false);
        tex.Apply(false);

        RenderTexture currentRT = RenderTexture.active;
        RenderTexture renderTexture = new RenderTexture(1, 1, 32);
        Graphics.Blit(tex, renderTexture);

        RenderTexture.active = renderTexture;

        Color pixel = tex.GetPixel(0, 0);

        if (pixel is { r: 0, g: 0, b: 0 })
        {
            DestroyPlayer();
        }
        else
        {
            Vars.Score++;
            if (Vars.Score > PlayerPrefs.GetInt("BestScore"))
            {
                PlayerPrefs.SetInt("BestScore", Vars.Score);
            }

            _score.SetText("POINTS: " + Vars.Score);
            PlayerPrefs.SetInt("totalPoints", PlayerPrefs.GetInt("totalPoints") + 1);
            _lineChangeSound.Play();
        }

        RenderTexture.active = currentRT;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DestroyPlayer();
    }

    private void DestroyPlayer()
    {
        GameObject.Find("ExplosionSound").GetComponent<AudioSource>().Play();
        transform.parent = null;
        transform.Find("PlayerSprite").GetComponent<PlayerDestroy>().enabled = true;
        if (GameObject.Find("TopMenu"))
        {
            GameObject.Find("TopMenu").SetActive(false);
        }

        GameObject rpyButton = GameObject.Find("ReplyButton");
        rpyButton.transform.localScale = new Vector2(0.8f, 0.8f);
        rpyButton.GetComponent<CircleCollider2D>().enabled = true;
        rpyButton.GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("GameOverMenu").transform.localScale = new Vector2(1, 1);
        GameObject.Find("GameOverScore").GetComponent<TextMeshProUGUI>().SetText("POINTS: " + Vars.Score);
        GameObject.Find("GameOverBestScore").GetComponent<TextMeshProUGUI>()
            .SetText("BEST: " + PlayerPrefs.GetInt("BestScore"));
        Destroy(this.gameObject, 0.5f);
        GetComponent<PlayerLogic>().enabled = false;
    }
}