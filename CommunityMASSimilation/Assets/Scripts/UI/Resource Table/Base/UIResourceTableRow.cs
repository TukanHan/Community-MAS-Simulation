using System.Collections.Generic;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ResourceTable
{
    public abstract class UIResourceTableRow : MonoBehaviour, IUIPanel, IListElement
    {
        public GameObject dataCellPrefab;
        public Text labelText;

        protected Dictionary<Resource, Text> dataCells = new Dictionary<Resource, Text>();
        protected RectTransform rectTransform;

        public virtual void Enable(params object[] data)
        {
            rectTransform = GetComponent<RectTransform>();

            AgentQueueController.instance.NextRoundStarted += UpdateDataCallback;

            BuildRow(data[0] as List<Resource>);
            UpdateData();
        }

        protected virtual float BuildRow(List<Resource> resOrder)
        {
            float width = 0.85f / resOrder.Count;
            float min = labelText.rectTransform.anchorMax.x, max = min + width;

            foreach (var res in resOrder)
            {
                GameObject dataCell = Instantiate(dataCellPrefab, transform);

                dataCell.GetComponent<RectTransform>().anchorMin = new Vector2(min, 0.1f);
                dataCell.GetComponent<RectTransform>().anchorMax = new Vector2(max, 0.9f);
                dataCell.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                dataCell.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

                min += width;
                max += width;

                dataCells[res] = dataCell.GetComponent<Text>();
            }

            return min;
        }

        private void UpdateDataCallback(object sender, int round)
        {
            UpdateData();
        }

        protected abstract void UpdateData();

        public void Disable()
        {
            AgentQueueController.instance.NextRoundStarted -= UpdateDataCallback;
            Destroy(gameObject);
        }

        public RectTransform GetRectTransform()
        {
            return rectTransform;
        }
    }
}