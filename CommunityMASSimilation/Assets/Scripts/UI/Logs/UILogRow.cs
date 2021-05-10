using UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Logs
{
    public class UILogRow : MonoBehaviour, IUIPanel, IListElement
    {
        private RectTransform rectTransform;

        public void Enable(params object[] data)
        {
            rectTransform = GetComponent<RectTransform>();
            string text = data[0] as string;

            GetComponent<Text>().text = text;
        }

        public void Disable()
        {
            Destroy(gameObject);
        }

        public RectTransform GetRectTransform()
        {
            return rectTransform;
        }
    }
}