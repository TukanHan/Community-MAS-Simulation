using Professions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.AgentInfoPanel.List.Rows
{
    public class WorkExperienceRow : MonoBehaviour
    {
        public Text workName;
        public Text experience;

        public void Enable(KeyValuePair<WorkTask, float> pair)
        {
            workName.text = pair.Key.productionName;
            experience.text = $"{String.Format("{0:0.}%", Mathf.Ceil(pair.Value * 100))}";
        }
    }
}