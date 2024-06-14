
using UnityEngine;
using UnityEditor;
namespace TowerOfDefence.Level
{

    [CustomEditor(typeof(LevelGenarator))]
    public class LevelGenaratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            LevelGenarator levelGenarator = (LevelGenarator)target;
            if (GUILayout.Button("Clear Level"))
            {
                levelGenarator.ClearLevel();
            }
            if (GUILayout.Button("Create Level"))
            {
                levelGenarator.DoCreateLevel();
            }
        }
    }
}
