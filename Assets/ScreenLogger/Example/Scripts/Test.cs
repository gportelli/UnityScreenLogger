using UnityEngine;
using System.Collections;
using AClockworkBerry;

public class Test : MonoBehaviour
{
    float timeout = 5, lasttime = -1;
    int i = 0;

    void Start()
    {
        ScreenLogger.Instance.ShowLog = true;

        TestMessageTypes();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log(i++);
        }

        timeout -= Time.deltaTime;

        if ((int)lasttime != (int)timeout && timeout > 0)
            Debug.Log("-" + ((int)timeout + 1));

        lasttime = timeout;

        if (timeout <= 0)
        {
            Debug.Log("Loading new scene...");
            Application.LoadLevel("ExampleScene");
        }
    }

    void TestMessageTypes()
    {
        Debug.Log("Log message...");
        Debug.LogWarning("Warning message...");
        Debug.LogError("Error message...");
    }
}
