using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.AgentInfoPanel.List.Rows
{
    public class ConsumptionQualityRow : MonoBehaviour
    {
        public Text resType;
        public Text resName;
        public Image image;
        public Text percentText;

        public void Enable(Resource resource, float percent)
        {
            resType.text = resource.tag.GetText();
            resName.text = resource.resName;
            image.sprite = resource.image;
            percentText.text = $"{String.Format("{0:0.}%", Mathf.Ceil(percent *100))}";
        }
    }
}