using UnityEngine;
using UnityEngine.UI;

namespace UI.AgentInfoPanel.List.Rows
{
    public class TradePriceRow : MonoBehaviour
    {
        public Image image;
        public Text resName;
        public Text priceText;
        public Text countText;

        public void Enable(Resource resource, int price, int count)
        {
            resName.text = resource.resName;
            priceText.text = price.ToString();
            countText.text = count.ToString();
            image.sprite = resource.image;
        }
    }
}