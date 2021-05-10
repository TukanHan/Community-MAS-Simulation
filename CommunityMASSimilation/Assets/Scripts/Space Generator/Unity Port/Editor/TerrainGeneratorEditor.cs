using UnityEngine;
using UnityEditor;
using SpaceGeneration.DataModels;

namespace SpaceGeneration.UnityPort
{
    [CustomEditor(typeof(SpaceGenerator))]
    public class SpaceGeneratorEditor : Editor
    {
        private SerializedProperty waterNoiseMapParameters;
        private SerializedProperty mountainNoiseMapParameters;
        private SerializedProperty resources;

        private SpaceGenerator spaceGenerator;

        void OnEnable()
        {
            waterNoiseMapParameters = serializedObject.FindProperty("waterNoiseMapParameters");
            mountainNoiseMapParameters = serializedObject.FindProperty("mountainNoiseMapParameters");
            resources = serializedObject.FindProperty("resources").FindPropertyRelative("resources");

            spaceGenerator = (SpaceGenerator)target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawShapeSection();
            EditorGUILayout.Space();

            DrawWaterBiomSection();
            EditorGUILayout.Space();

            DrawResourcesSection(spaceGenerator.resources);
            EditorGUILayout.Space();

            DrawGenerationSection();
            EditorGUILayout.Space();

            DrawButtonSection();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawShapeSection()
        {
            EditorGUILayout.LabelField("Shape", EditorStyles.boldLabel);
            spaceGenerator.radius = EditorGUILayout.IntField("Radius", spaceGenerator.radius, GUILayout.MinHeight(10));
            spaceGenerator.plainHex = (HexSO)EditorGUILayout.ObjectField("Plain biom", spaceGenerator.plainHex, typeof(HexSO), true);
        }

        private void DrawWaterBiomSection()
        {
            EditorGUILayout.LabelField("Water", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(waterNoiseMapParameters, new GUIContent("Water noise map parameters"), true);
            
            spaceGenerator.waterHex = (HexSO)EditorGUILayout.ObjectField("Water biom",spaceGenerator.waterHex, typeof(HexSO), true);
        }

        private void DrawResourcesSection(ResourcesContainer resourcesContainer)
        {
            EditorGUILayout.LabelField("Resource distribution", EditorStyles.boldLabel);

            for (int i = 0; i < resourcesContainer.resources.Count; ++i)
            {
                EditorGUILayout.PropertyField(resources.GetArrayElementAtIndex(i), new GUIContent($"Resource {i}"), true);
            }

            if (GUILayout.Button("Add resource", GUILayout.Width(150)))
                resourcesContainer.AddResource();
        }

        private void DrawGenerationSection()
        {
            EditorGUILayout.LabelField(new GUIContent("Generation"), EditorStyles.boldLabel);

            spaceGenerator.generateRandomSeed = EditorGUILayout.Toggle(
                new GUIContent("Generate random seed", "Is the map to be generated based on random seed?"),
                spaceGenerator.generateRandomSeed);

            if (!spaceGenerator.generateRandomSeed)
            {
                EditorGUILayout.BeginHorizontal();

                spaceGenerator.seed = EditorGUILayout.IntField(
                    new GUIContent("Seed", "Random factor based on which the map is generated."),
                    spaceGenerator.seed);

                if (GUILayout.Button("Random Seed"))
                    spaceGenerator.RandomSeed();

                EditorGUILayout.EndHorizontal();
            }
        }

        private void DrawButtonSection()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Generate"))
            {
                if (spaceGenerator.generateRandomSeed)
                    spaceGenerator.RandomSeed();
            
                spaceGenerator.Generate();
            }

            if (GUILayout.Button("Clear"))
            {
                spaceGenerator.Clear();
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}