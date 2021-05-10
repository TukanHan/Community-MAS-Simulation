using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ResourceTable
{
    public class UIResourcesTableHeader : MonoBehaviour
    {
        public GameObject resHeaderPrefab;

        public Text nameText;

        public void Set(List<Resource> resOrder)
        {
            BuildRow(resOrder);
        }

        private void BuildRow(List<Resource> resOrder)
        {
            float width = 0.85f / resOrder.Count;
            float min = nameText.rectTransform.anchorMax.x, max = min + width;

            foreach (var res in resOrder)
            {
                GameObject dataCell = Instantiate(resHeaderPrefab, transform);
                dataCell.GetComponent<RectTransform>().anchorMin = new Vector2(min, 0f);
                dataCell.GetComponent<RectTransform>().anchorMax = new Vector2(max, 1f);
                dataCell.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                dataCell.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

                min += width;
                max += width;

                dataCell.GetComponent<Image>().sprite = res.image;
            }
        }
    }
}