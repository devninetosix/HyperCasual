using UnityEngine;
using UnityEngine.UI;

public class MenuTransition : MonoBehaviour
{
    private float _timer;
    private bool _up = true;
    private float _alpha;
    public Image img;
    private GameObject _gameManager;
    private Menus _menus;
    private MenuTransition _menuTransition;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager");
        _menus = _gameManager.GetComponent<Menus>();
        _menuTransition = GetComponent<MenuTransition>();
    }

    private void OnEnable()
    {
        img.enabled = true;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (!(_timer >= 0.01f))
        {
            return;
        }

        _timer = 0;
        if (_up)
        {
            img.raycastTarget = true;
            _alpha += 0.02f;
            if (_alpha >= 1f)
            {
                _up = false;
                img.raycastTarget = false;

                switch (Vars.CurrentMenu)
                {
                    case 0:
                        _menus.ShowMainMenu();
                        break;
                    case 1:
                        _menus.ShowGamePlayMenu();
                        break;
                    case 2:
                        _menus.GameReply();
                        break;
                }
            }
        }
        else
        {
            _alpha -= 0.02f;
            if (_alpha <= 0)
            {
                _up = true;
                img.enabled = false;
                _menuTransition.enabled = false;
            }
        }

        img.color = new Color(0, 0, 0, _alpha);
    }
}