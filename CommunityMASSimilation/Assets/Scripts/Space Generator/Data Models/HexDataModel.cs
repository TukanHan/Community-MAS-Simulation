using Agents;
using Coordinates;
using SpaceGeneration.DataModels;
using System;
using UnityEngine;

namespace DataModels
{
    [Serializable]
    public class HexDataModel
    {
        public CubeCoordinate coordinate;
        public HexSO hexTypeInfo;
        public GameObject hexObject;

        public Agent Worker { get; private set; }

        public void RegisterWorker(Agent worker)
        {
            Worker = worker;
        }

        public void UnregisterWorker()
        {
            Worker = null;
        }
    }

    [Serializable]
    public class HexDataModelDictionary : SerializableDictionary<CubeCoordinate, HexDataModel> { }
}