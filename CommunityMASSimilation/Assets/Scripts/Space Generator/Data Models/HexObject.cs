using Coordinates;
using SpaceGeneration.UnityPort;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class HexObject : MonoBehaviour
{
    public CubeCoordinate coordinate;

    private void OnMouseUpAsButton()
    {
        if (!EventSystem.current.IsPointerOverGameObject()/* && !PanelCentralManager.instance.IsSelected*/)
        {
            DataModels.HexDataModel hexDataModel = SpaceGenerator.instance.Map[coordinate];

            if (hexDataModel.Worker != null)
                UIPanelsController.instance.EnableAgentInfoPanel(hexDataModel.Worker);
        }
    }
}
