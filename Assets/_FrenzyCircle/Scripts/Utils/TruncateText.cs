using TMPro;
using UnityEngine;

public class TruncateText : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    private const int maxCharacters = 12;

    private void FixedUpdate()
    {
        if (textMesh.text.Length <= maxCharacters)
        {
            return;
        }

        string truncatedText = textMesh.text[..maxCharacters] + "...";
        textMesh.SetText(truncatedText);
    }
}