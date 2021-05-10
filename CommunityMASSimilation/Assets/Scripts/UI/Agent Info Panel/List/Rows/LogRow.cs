using UnityEngine;
using UnityEngine.UI;

namespace UI.AgentInfoPanel.List.Rows
{
    public class LogRow : MonoBehaviour
    {
        public Text text;

        public void Enable(string str)
        {
            text.text = str;
        }
    }
}