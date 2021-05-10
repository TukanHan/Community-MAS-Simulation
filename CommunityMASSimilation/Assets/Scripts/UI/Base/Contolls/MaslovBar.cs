using Agents;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Base
{
    public class MaslovBar : MonoBehaviour
    {
        public Color emptyColor;

        public Image bar1;
        public Image background1;
        public Color fillColor1;
        public Color fullColor1;

        public Image bar2;
        public Image background2;
        public Color fillColor2;
        public Color fullColor2;

        public Image bar3;
        public Image background3;
        public Color fillColor3;
        public Color fullColor3;

        public void SetPercent(Energy energy)
        {
            bar1.fillAmount = energy.FoodNeeds;
            bar2.fillAmount = energy.BasicNeeds;
            bar3.fillAmount = energy.LuxuryNeeds;


            if (energy.AreFoodNeedsMeeting)
                background1.color = fillColor1;
            else
                background1.color = emptyColor;

            
            if (energy.AreFoodNeedsMeeting)
            {
                bar2.color = fullColor2;

                if (energy.AreBasicNeedsMeeting)
                    background2.color = fillColor2;
                else
                    background2.color = emptyColor;
            }
            else
            {
                background2.color = emptyColor;
                bar2.color = Color.Lerp(fullColor2, emptyColor, 0.7f);
            }

            background3.color = emptyColor;
            if(energy.AreBasicNeedsMeeting)
            {
                bar3.color = fullColor3;
            }
            else
            {
                bar3.color = Color.Lerp(fullColor3, emptyColor, 0.7f);
            }
        }
    }
}