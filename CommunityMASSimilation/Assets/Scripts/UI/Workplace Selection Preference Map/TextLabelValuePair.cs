using UnityEngine;
using UnityEngine.UI;

namespace UI.Base
{
    public class TextLabelValuePair : MonoBehaviour
    {
        public Text label;
        public Text value;

        public void SetValue(string text)
        {
            value.text = text;
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
