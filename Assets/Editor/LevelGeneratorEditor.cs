
using UnityEngine;
using UnityEditor;
namespace TowerOfDefence.Level
{

    [CustomEditor(typeof(LevelGenerator))]
    public class LevelGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            LevelGenerator LevelGenerator = (LevelGenerator)target;
            if (GUILayout.Button("Clear Level"))
            {
                LevelGenerator.ClearLevel();
            }
            if (GUILayout.Button("Create Level"))
            {
                LevelGenerator.DoCreateLevel();
            }
        }
    }
}
