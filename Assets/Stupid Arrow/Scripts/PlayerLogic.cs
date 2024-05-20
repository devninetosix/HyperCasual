using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerLogic : MonoBehaviour {

    public float FixeScale = 1;
    public GameObject parent;
    public Camera cam;
    private AudioSource lineChangeSound;
    private Text score;

    void Awake (){
        lineChangeSound = GameObject.Find("LineChangeSound").GetComponent<AudioSource> ();
        score = GameObject.Find("Canvas").transform.Find("GameMenu").transform.Find("TopMenu").transform.Find("ScoreText").GetComponent<Text>();
    }
     
    void FixedUpdate () {
        transform.localScale = new Vector2 (FixeScale/parent.transform.localScale.x,FixeScale/parent.transform.localScale.y); 
    }  

    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            if(EventSystem.current.IsPointerOverGameObject(-1) || EventSystem.current.IsPointerOverGameObject(0) || EventSystem.current.IsPointerOverGameObject(1)) {
                return;
            }
            Vars.obstacle++;

            GameObject circle = GameObject.Find("circle" + Vars.obstacle);
            float circleScale = circle.transform.localScale.x;

            Vars.numberOfCircles++;
            GameObject newCircle = Instantiate(Resources.Load("circle", typeof(GameObject))) as GameObject;
           
            newCircle.transform.parent = GameObject.Find("Gameplay").transform;
            GameObject previousCircle = GameObject.Find("circle" + (Vars.numberOfCircles - 1));

            GameObject circleToJump = GameObject.Find("circle" + (Vars.obstacle - 1));
            float circleToJumpScale = circleToJump.transform.localScale.x; 
            circle.transform.localScale = new Vector2(0.2f, 0.2f);
            circleToJump.transform.localScale = new Vector2(0.2f, 0.2f);
            transform.parent = circle.transform;
            parent = circle;
            circle.transform.localScale = new Vector2(circleScale, circleScale);
            circleToJump.transform.localScale = new Vector2(circleToJumpScale, circleToJumpScale);
            
            newCircle.name = "circle" + Vars.numberOfCircles;
            SpriteRenderer newCircleSpriteRenderer = newCircle.GetComponent<SpriteRenderer>();
            newCircleSpriteRenderer.sortingOrder = -Vars.numberOfCircles;
            newCircle.transform.localScale = new Vector3(previousCircle.transform.localScale.x + 0.05f, previousCircle.transform.localScale.y + 0.05f, 1);
            if(Vars.numberOfCircles %2 == 0) {
                newCircle.GetComponent<ObstacleRotation> ().rotateForward = false;
                newCircle.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            Vector2 playerPos = cam.WorldToScreenPoint(transform.position);
            StartCoroutine (CheckPixelColor((int)playerPos.x, (int)playerPos.y));
        }
       
    }

      IEnumerator CheckPixelColor(int x, int y) {//It will check the color of the pixel at the top of current arrow position
            Rect viewRect = cam.pixelRect;
            Texture2D tex = new Texture2D(1, 1, TextureFormat.RGB24, false );
            yield return new WaitForEndOfFrame();
            tex.ReadPixels(new Rect(x, y, x, y), 0, 0, false);
            tex.Apply(false);

            RenderTexture currentRT = RenderTexture.active;
        
            RenderTexture renderTexture = new RenderTexture(1, 1, 32);
            Graphics.Blit(tex, renderTexture);
        
            RenderTexture.active = renderTexture;
        
            Color pixel = tex.GetPixel(0, 0);
            
            if(pixel.g == 0  && pixel.b == 0) {
                DestroyPlayer();
            }else {
                 Vars.score++;
                if(Vars.score > PlayerPrefs.GetInt("BestScore")) {
                    PlayerPrefs.SetInt("BestScore", Vars.score);
                }
                score.text = "POINTS: " + Vars.score;
                PlayerPrefs.SetInt("totalPoints", PlayerPrefs.GetInt("totalPoints") + 1);
                lineChangeSound.Play();
            }
            
            RenderTexture.active = currentRT;
    }

    void OnTriggerEnter2D(Collider2D other) {
         DestroyPlayer();
    }

    private void DestroyPlayer() {
        GameObject.Find("ExplosionSound").GetComponent<AudioSource> ().Play();
        transform.parent = null;
        transform.Find("PlayerSprite").GetComponent<PlayerDestroy> ().enabled = true;
        if(GameObject.Find("TopMenu") != null) GameObject.Find("TopMenu").SetActive(false);
        GameObject rpyButton = GameObject.Find("ReplyButton");
        rpyButton.transform.localScale = new Vector2(4, 4);
        rpyButton.GetComponent<CircleCollider2D> ().enabled = true;
        rpyButton.GetComponent<SpriteRenderer> ().enabled = true;
        GameObject.Find("GameOverMenu").transform.localScale = new Vector2(1, 1);
        GameObject.Find("GameOverScore").GetComponent<Text> ().text = "POINTS: " + Vars.score;
        GameObject.Find("GameOverBestScore").GetComponent<Text> ().text = "BEST: " + PlayerPrefs.GetInt("BestScore");
        Destroy(this.gameObject, 0.5f);
        GetComponent<PlayerLogic> ().enabled = false;
    }
}
