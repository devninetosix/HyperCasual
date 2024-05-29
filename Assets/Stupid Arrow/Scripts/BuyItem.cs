using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    public Image background;
    public GameObject price;
    public TextMeshProUGUI points;

    private void Start()
    {
        PlayerPrefs.SetInt("Item0", 1);
    }

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("Item" + gameObject.name) == 1)
        {
            price.SetActive(false);
        }

        if (int.Parse(gameObject.name) == PlayerPrefs.GetInt("Player"))
        {
            background.color = new Color(1, 1, 1, 1);
        }

        points.text = "POINTS: " + (PlayerPrefs.GetInt("totalPoints") - PlayerPrefs.GetInt("spentPoints"));
    }

    public void Buy()
    {
        if (PlayerPrefs.GetInt("Item" + gameObject.name) != 1)
        {
            if (PlayerPrefs.GetInt("totalPoints") - PlayerPrefs.GetInt("spentPoints") >= 100)
            {
                GameObject.Find("GameManager").GetComponent<Menus>().UnSelectAllShopItems();
                PlayerPrefs.SetInt("spentPoints", PlayerPrefs.GetInt("spentPoints") + 100);
                PlayerPrefs.SetInt("Item" + gameObject.name, 1);
                background.color = new Color(1, 1, 1, 1);
                price.SetActive(false);
                points.text = "POINTS: " + (PlayerPrefs.GetInt("totalPoints") - PlayerPrefs.GetInt("spentPoints"));
                GameObject.Find("ItemPurchaseSound").GetComponent<AudioSource>().Play();
                PlayerPrefs.SetInt("Player", int.Parse(gameObject.name));
            }
            else
            {
                GameObject.Find("DenySound").GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            GameObject.Find("GameManager").GetComponent<Menus>().UnSelectAllShopItems();
            background.color = new Color(1, 1, 1, 1);
            price.SetActive(false);
            GameObject.Find("ButtonSound").GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("Player", int.Parse(gameObject.name));
        }
    }
}