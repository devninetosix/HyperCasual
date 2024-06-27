using TMPro;
using UnityEngine;


namespace FrenzyCircle
{
    public class TruncateText : MonoBehaviour
    {
        private const int MaxCharacters = 12;

        private TextMeshProUGUI _textMesh;

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
            _textMesh.SetText(" ");
        }

        private void LateUpdate()
        {
            if (_textMesh.text == string.Empty)
            {
                return;
            }

            if (_textMesh.text.Length < MaxCharacters)
            {
                return;
            }

            string truncatedText = _textMesh.text[..MaxCharacters] + "...";
            _textMesh.SetText(truncatedText);
        }
    }
}
