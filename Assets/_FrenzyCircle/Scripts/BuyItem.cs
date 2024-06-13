using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    public Image background;

    private void OnEnable()
    {
        string themeName = gameObject.name;

        if (int.Parse(themeName) == ES3.Load(Contant.Theme, 0))
        {
            background.color = new Color(1, 1, 1, 1);
        }
    }

    public void Buy()
    {
        GameObject.Find("GameManager").GetComponent<Menus>().UnSelectAllShopItems();
        background.color = new Color(1, 1, 1, 1);
        GameObject.Find("ButtonSound").GetComponent<AudioSource>().Play();
        ThemeManager.SetThemes(int.Parse(gameObject.name));
    }
}