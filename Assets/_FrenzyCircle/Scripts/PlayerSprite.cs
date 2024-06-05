using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    public Sprite[] sprite;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprite[PlayerPrefs.GetInt("Player", 0)];
    }
}