using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
    public int lines;
    int i = 1;

    // Use this for initialization
    void Start()
    {
        TestMessageTypes();
        //Debug.Log(Screen.height);
        //TestSize(lines);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log(i++);
        }
    }

    void TestSize(int lines)
    {
        for (; i <= lines; i++)
        {
            Debug.Log(i);
        }
    }

    void TestMessageTypes()
    {
        Debug.Log("Log message...");
        Debug.LogWarning("Warning message...");
        Debug.LogError("Error message...");

        int a = 0, b = 5;
        int c = b / a;
    }


}
