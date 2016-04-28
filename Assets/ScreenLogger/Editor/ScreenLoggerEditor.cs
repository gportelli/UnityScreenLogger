using UnityEngine;
using System.Collections;
using UnityEditor;

namespace AClockworkBerry
{
    [CustomEditor(typeof(ScreenLogger))]
    public class ScreenLoggerEditor : Editor
    {
        [MenuItem("GameObject/Create Other/Screen Logger")]
        static void AddScreenLogger()
        {
            if (GameObject.FindObjectOfType<ScreenLogger>() == null)
            {
                GameObject gameObject = new GameObject();
                gameObject.name = "ScreenLogger";
                gameObject.AddComponent<ScreenLogger>();
            }
            else
            {
                Debug.LogError("ScreenLogger already added to the scene.");
            }
        }

        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();

            ScreenLogger.Instance.InspectorGUIUpdated();
        }
    }
}
