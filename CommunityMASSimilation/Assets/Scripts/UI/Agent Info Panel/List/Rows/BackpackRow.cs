using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.AgentInfoPanel.List.Rows
{
    public class BackpackRow : MonoBehaviour
    {
        public Text resName;
        public Text count;
        public Image image;

        public void Enable(ResourceCountPair pair)
        {
            resName.text = pair.resource.resName;
            count.text = $"{String.Format("{0:0.##}", pair.count)}";
            image.sprite = pair.resource.image;
        }
    }
}