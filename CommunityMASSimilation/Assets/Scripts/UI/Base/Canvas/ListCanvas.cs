using System.Collections.Generic;
using UnityEngine;

namespace UI.Base
{
    public class ListCanvas<T>
    {
        private Dictionary<T, IListElement> elements = new Dictionary<T, IListElement>();

        private RectTransform contentPanel;

        public ListCanvas(RectTransform contentPanel)
        {
            this.contentPanel = contentPanel;
            contentPanel.ClearChildren();
            contentPanel.sizeDelta = new Vector2(contentPanel.sizeDelta.x, 0);
        }

        public void AddRow(T key, IListElement element)
        {
            SetElementPosition(element, new Vector2(0, -contentPanel.sizeDelta.y));
            ChangeContentSize(element.GetRectTransform().sizeDelta.y);

            elements.Add(key, element);
        }

        public void RemoveRow(T key)
        {
            IListElement element = elements[key];
            ChangeContentSize(-element.GetRectTransform().sizeDelta.y);
            elements.Remove(key);
            element.Disable();

            ReorganizeList();
        }

        public void Remove()
        {
            foreach(var key in elements.Keys)
            {
                elements[key].Disable();
            }

            elements.Clear();
            contentPanel.sizeDelta = new Vector2(contentPanel.sizeDelta.x, 0);
        }

        private void SetElementPosition(IListElement element, Vector2 position)
        {
            element.GetRectTransform().anchoredPosition = position;
        }

        private void ChangeContentSize(float change)
        {
            contentPanel.sizeDelta = new Vector2(contentPanel.sizeDelta.x, contentPanel.sizeDelta.y + change);
        }

        private void ReorganizeList()
        {
            float x = 0;
            foreach (var element in elements.Values)
            {
                SetElementPosition(element, new Vector2(0, -x));
                x += element.GetRectTransform().sizeDelta.y;
            }
        }
    }
}


