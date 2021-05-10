using System;

namespace UI.Base
{
    public interface IUIPanel
    {
        void Enable(params object[] data);
        void Disable();
    }
}