using System;
using UnityEngine;
using UnityEngine.UI;

public class WSPDemandRow : MonoBehaviour
{
    public Image image;
    public Text resName;
    public Text demandSizeText;

    public void Enable(Resource resource, float demandSize)
    {
        resName.text = resource.resName;
        image.sprite = resource.image;

        demandSizeText.text = $"{String.Format("{0:0.##}", demandSize)}";
    }
}
