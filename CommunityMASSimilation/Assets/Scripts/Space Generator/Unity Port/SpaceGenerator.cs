using Coordinates;
using DataModels;
using SpaceGeneration.DataModels;
using SpaceGeneration.Generation;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGeneration.UnityPort
{
    public class SpaceGenerator : SingletonBehaviour<SpaceGenerator>
    {
        public int radius = 10;

        public PercentNoiseMapParameters waterNoiseMapParameters;

        public bool generateRandomSeed = true;
        public int seed;

        public HexSO plainHex;
        public HexSO waterHex;

        public ResourcesContainer resources;

        public HexDataModelDictionary Map { get; set; }

        public SpaceGenerator()
        {
            instance = this;
        }

        private void Start()
        {
            if (generateRandomSeed)
                RandomSeed();

            Generate();
        }

        public void RandomSeed()
        {
            seed = Random.Range(int.MinValue, int.MaxValue);
        }

        public void Clear()
        {
            for (int i = transform.childCount - 1; i >= 0; --i)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }

            Map = null;
        }

        public void Generate()
        {
            Clear();

            Generator generator = new Generator(seed, radius, plainHex, waterHex,
                waterNoiseMapParameters.ToModel(), resources.ToModel());

            generator.Generate();
            Map = new HexDataModelDictionary();

            foreach (var hex in generator.HexMap.Map)
            {
                Map[hex.Key] = new HexDataModel()
                {
                    coordinate = hex.Key,
                    hexTypeInfo = hex.Value.HexModel,
                    hexObject = PrepareHexagon(hex.Key, hex.Value)
                };
            }

            SelectManager.instance.Select();
        }

        public GameObject PrepareHexagon(CubeCoordinate coordinate, Hex hex)
        {
            Vector2 position = WorldPosition(new Coordinate(coordinate));

            GameObject hexGameObject = CreateHexagonObject(position, hex.HexModel.image as Sprite);
            HexObject hexObject = hexGameObject.AddComponent<HexObject>();
            hexObject.coordinate = coordinate;

            CircleCollider2D circleCollider2D = hexGameObject.AddComponent<CircleCollider2D>();
            circleCollider2D.radius = 0.5f;

            return hexGameObject;
        }

        private GameObject CreateHexagonObject(Vector2 position, Sprite image)
        {
            GameObject gameObject = new GameObject();
            gameObject.transform.parent = transform;
            gameObject.transform.position = position;

            SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = image;

            return gameObject;
        }

        public static Vector2 WorldPosition(Coordinate coordinate)
        {
            return new Vector2(coordinate.Y % 2 == 0 ? coordinate.X : coordinate.X - 0.5f, coordinate.Y * 0.875f);
        }
    }
}