using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class BuyItem : MonoBehaviour
{
    public Image background;
    public GameObject price;
    public TextMeshProUGUI points;
    public TextMeshProUGUI availablePoint;

    private TextMeshProUGUI _priceText;
    private int _thisPrice;

    private void Awake()
    {
        _priceText = price.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        PlayerPrefs.SetInt("Item0", 1);
    }

    private void OnEnable()
    {
        if (_priceText != null)
        {
            Match match = Regex.Match(_priceText.text, @"\d+");
            _thisPrice = match.Success ? int.Parse(match.Value) : 0;
        }

        string itemName = "Item" + gameObject.name;
        string themeName = gameObject.name;

        if (PlayerPrefs.GetInt(itemName) == 1)
        {
            price.SetActive(false);
        }

        if (int.Parse(themeName) == PlayerPrefs.GetInt("Theme"))
        {
            background.color = new Color(1, 1, 1, 1);
        }

        // UpdatePointsText();
    }

    public void Buy()
    {
        GameObject.Find("GameManager").GetComponent<Menus>().UnSelectAllShopItems();
        background.color = new Color(1, 1, 1, 1);
        price.SetActive(false);
        GameObject.Find("ButtonSound").GetComponent<AudioSource>().Play();
        ThemeManager.SetThemes(int.Parse(gameObject.name));
        
        // string itemName = "Item" + gameObject.name;
        //
        // if (PlayerPrefs.GetInt(itemName) != 1)
        // {
        //     int totalPoints = PlayerPrefs.GetInt("totalPoints");
        //     int spentPoints = PlayerPrefs.GetInt("spentPoints");
        //
        //     if (totalPoints - spentPoints >= _thisPrice)
        //     {
        //         GameObject.Find("GameManager").GetComponent<Menus>().UnSelectAllShopItems();
        //         PlayerPrefs.SetInt("spentPoints", spentPoints + _thisPrice);
        //         PlayerPrefs.SetInt(itemName, 1);
        //         background.color = new Color(1, 1, 1, 1);
        //         price.SetActive(false);
        //         UpdatePointsText();
        //         GameObject.Find("ItemPurchaseSound").GetComponent<AudioSource>().Play();
        //         ThemeManager.SetThemes(int.Parse(gameObject.name));
        //     }
        //     else
        //     {
        //         GameObject.Find("DenySound").GetComponent<AudioSource>().Play();
        //     }
        // }
        // else
        // {
        //     GameObject.Find("GameManager").GetComponent<Menus>().UnSelectAllShopItems();
        //     background.color = new Color(1, 1, 1, 1);
        //     price.SetActive(false);
        //     GameObject.Find("ButtonSound").GetComponent<AudioSource>().Play();
        //     ThemeManager.SetThemes(int.Parse(gameObject.name));
        // }
    }

    private void UpdatePointsText()
    {
        int totalPoints = PlayerPrefs.GetInt("totalPoints");
        int spentPoints = PlayerPrefs.GetInt("spentPoints");
        points.SetText("SCORE: " + (totalPoints - spentPoints));
        availablePoint.SetText(points.text);
    }
}
