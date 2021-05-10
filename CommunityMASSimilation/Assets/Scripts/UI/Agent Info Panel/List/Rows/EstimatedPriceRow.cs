using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.AgentInfoPanel.List.Rows
{
    public class EstimatedPriceRow : MonoBehaviour
    {
        public Text resName;
        public Text priceText;
        public Image image;

        public void Enable(Resource resource, float price)
        {
            resName.text = resource.resName;
            priceText.text = $"{String.Format("{0:0.##}", price)}";
            image.sprite = resource.image;
        }
    }
}
