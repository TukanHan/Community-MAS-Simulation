using UnityEngine;
using UnityEngine.UI;

namespace UI.Base
{
    public class ProgressBar : MonoBehaviour
    {
        public Image bar;

        public void SetPercent(float percent)
        {
            bar.fillAmount = percent;
        }
    }
}