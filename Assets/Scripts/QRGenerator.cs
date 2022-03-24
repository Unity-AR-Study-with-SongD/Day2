using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public class QRGenerator : MonoBehaviour
{
    [SerializeField]
    private RawImage qrCodeImg;
    public string text { get; set; }

    public void CreateQRCode ()
    {
        qrCodeImg.texture = GenerateBarcode();
    }

    private Texture2D GenerateBarcode ()
    {
        int width = 256;
        int height = 256;

        Texture2D result = new Texture2D(256, 256);

        BarcodeWriter writer = new BarcodeWriter();
        writer.Format = BarcodeFormat.QR_CODE;
        writer.Options = new QrCodeEncodingOptions();
        writer.Options.Width = width;
        writer.Options.Height = height;
        Color32[] colors = writer.Write(text);

        result.SetPixels32(colors);
        result.Apply();

        return result;
    }
}