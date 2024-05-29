using UnityEngine;

public class QrGenerator : MonoBehaviour {

    // Resolution
    private const int PixelsPerModule = 20;

    /// <summary>
    /// Encode text in to a QR Code
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static Texture2D EncodeString(string text)
    {
        QrCodeGenerator qrGenerator = new QrCodeGenerator();
        QrCodeGenerator.QrCode qrCode = qrGenerator.CreateQrCode(text, QrCodeGenerator.EccLevel.L);

        Texture2D qrTexture = qrCode.GetGraphic(PixelsPerModule);

        return qrTexture;
    }

    /// <summary>
    /// Encode text in to a QR Code and define the colors
    /// </summary>
    /// <param name="text"></param>
    /// <param name="darkColor"></param>
    /// <param name="lightColor"></param>
    /// <returns></returns>
    public static Texture2D EncodeString(string text, Color darkColor, Color lightColor)
    {
        QrCodeGenerator qrGenerator = new QrCodeGenerator();
        QrCodeGenerator.QrCode qrCode = qrGenerator.CreateQrCode(text, QrCodeGenerator.EccLevel.L);

        Texture2D qrTexture = qrCode.GetGraphic(PixelsPerModule, darkColor, lightColor);

        return qrTexture;
    }

    /// <summary>
    /// Encode text in to a QR Code and define the colors and the Errer Correction Level
    /// </summary>
    /// <param name="text"></param>
    /// <param name="darkColor"></param>
    /// <param name="lightColor"></param>
    /// <param name="errorCorrectionLevel"></param>
    /// <returns></returns>
    public static Texture2D EncodeString(string text, Color darkColor, Color lightColor, QrCodeGenerator.EccLevel errorCorrectionLevel)
    {
        QrCodeGenerator qrGenerator = new QrCodeGenerator();
        QrCodeGenerator.QrCode qrCode = qrGenerator.CreateQrCode(text, errorCorrectionLevel);

        Texture2D qrTexture = qrCode.GetGraphic(PixelsPerModule, darkColor, lightColor);

        return qrTexture;
    }
}
