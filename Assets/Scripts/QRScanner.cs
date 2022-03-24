using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using ZXing;

public class QRScanner : MonoBehaviour
{
    [SerializeField]
    private ARCameraManager arCam;
    [SerializeField]
    private Text qrCodeText;

    private void Update ()
    {
        GetARCamBarcode();
    }

    private void GetARCamBarcode ()
    {
        if (arCam.TryAcquireLatestCpuImage(out XRCpuImage image))
        {
            using (image)
            {
                var conversionParams = new XRCpuImage.ConversionParams(image, TextureFormat.RGBA32);
                var dataSize = image.GetConvertedDataSize(conversionParams);
                var bytesPerPixel = 4;

                var pixels = new Color32[dataSize / bytesPerPixel];

                unsafe
                {
                    fixed (void* ptr = pixels)
                    {
                        image.Convert(conversionParams, new IntPtr(ptr), dataSize);
                    }
                }

                IBarcodeReader barcodeReader = new BarcodeReader();
                var result = barcodeReader.Decode(pixels, image.width, image.height);

                if (result != null)
                {
                    qrCodeText.text = result.Text;
                }
            }
        }
    }
}