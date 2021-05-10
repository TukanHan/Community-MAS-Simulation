using UnityEngine;

namespace UI.Base
{
    public interface IListElement
    {
        RectTransform GetRectTransform();

        void Disable();
    }
}