using UnityEngine;
using System.Collections;
using UnityEditor;

namespace AClockworkBerry
{
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
        }
    }
}
