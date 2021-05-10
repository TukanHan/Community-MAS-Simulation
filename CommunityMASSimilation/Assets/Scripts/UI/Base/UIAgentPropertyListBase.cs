using Agents;
using UnityEngine;

namespace UI.Base
{
    public abstract class UIAgentPropertyListBase : MonoBehaviour
    {
        public RectTransform content;
        public GameObject headerPrefab;
        public GameObject rowPrefab;
        
        protected float rowHeight;

        public void Awake()
        {
            rowHeight = rowPrefab.GetComponent<RectTransform>().sizeDelta.y;
        }

        public void ResetPosition()
        {
            content.anchoredPosition = Vector2.zero;
        }

        public abstract void UpdateList(Agent agent);

        protected void CreateHeader()
        {
            GameObject item = Instantiate(headerPrefab, content);
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
    }
}