using TMPro;
using UnityEngine;

public class TruncateText : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;
    private const int maxCharacters = 12;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        if (_textMesh.text.Length <= maxCharacters)
        {
            return;
        }

        string truncatedText = _textMesh.text[..maxCharacters] + "...";
        _textMesh.SetText(truncatedText);
    }
}