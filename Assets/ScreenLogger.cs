using UnityEngine;
using System.Collections.Generic;

public class ScreenLogger : MonoBehaviour
{
    public enum LogAnchor
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    public bool IsPersistent = true;
    public bool ShowInEditor = true;

    [Tooltip("Height of the log area as a percentage of the screen height")]
    [Range(0.3f, 1.0f)]
    public float Height = 0.5f;

    [Tooltip("Width of the log area as a percentage of the screen width")]
    [Range(0.3f, 1.0f)]
    public float Width = 0.5f;

    public int Margin = 20;

    public LogAnchor AnchorPosition = LogAnchor.BottomLeft;

    [Range(10, 40)]
    public int FontSize = 14;

    [Range(0f, 0.8f)]
    public float BackgroundOpacity = 0.5f;

    public bool StackTraceLog = false;
    public bool StackTraceWarning = false;
    public bool StackTraceError = true;
    public bool StackTraceException = true;
    public bool StackTraceAssert = true;

    public Color NormalColor = Color.white;
    public Color WarningColor = Color.yellow;
    public Color ErrorColor = new Color(1, 0.5f, 0.5f);

    static Queue<LogMessage> queue = new Queue<LogMessage>();

    GUIStyle styleContainer, styleText;
    int padding = 5;

    public void Awake()
    {
        Texture2D back = new Texture2D(1, 1);
        back.SetPixel(0, 0, new Color(0, 0, 0, BackgroundOpacity));
        back.Apply();

        styleContainer = new GUIStyle();
        styleContainer.normal.background = back;
        styleContainer.wordWrap = true;
        styleContainer.padding = new RectOffset(padding, padding, padding, padding);

        styleText = new GUIStyle();
        styleText.fontSize = FontSize;

        if (IsPersistent)
            DontDestroyOnLoad(this);
    }

    void OnEnable()
    {
        if (!ShowInEditor && Application.isEditor) return;

        queue = new Queue<LogMessage>();

        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        if (!ShowInEditor && Application.isEditor) return;

        Application.logMessageReceived -= HandleLog;
    }

    void Update()
    {
        if (!ShowInEditor && Application.isEditor) return;

        while (queue.Count > ((Screen.height - 2 * Margin) * Height - 2 * padding) / styleText.lineHeight)
            queue.Dequeue();
    }

    void OnGUI()
    {
        if (!ShowInEditor && Application.isEditor) return;

        float w = (Screen.width - 2 * Margin) * Width;
        float h = (Screen.height - 2 * Margin) * Height;
        float x = 1, y = 1;

        switch(AnchorPosition) {
            case LogAnchor.BottomLeft:
                x = Margin;
                y = Margin + (Screen.height - 2 * Margin) * (1 - Height);
                break;

            case LogAnchor.BottomRight:
                x = Margin + (Screen.width - 2 * Margin) * (1 - Width);
                y = Margin + (Screen.height - 2 * Margin) * (1 - Height);
                break;

            case LogAnchor.TopLeft:
                x = Margin;
                y = Margin;
                break;

            case LogAnchor.TopRight:
                x = Margin + (Screen.width - 2 * Margin) * (1 - Width);
                y = Margin;
                break;
        }
        
        GUILayout.BeginArea(new Rect(x, y, w, h), styleContainer);

        foreach (LogMessage m in queue)
        {
            switch (m.Type)
            {
                case LogType.Warning:
                    styleText.normal.textColor = WarningColor;
                    break;

                case LogType.Log:
                    styleText.normal.textColor = NormalColor;
                    break;

                case LogType.Assert:
                case LogType.Exception:
                case LogType.Error:
                    styleText.normal.textColor = ErrorColor;
                    break;

                default:
                    styleText.normal.textColor = NormalColor;
                    break;
            }

            GUILayout.Label(m.Message, styleText);
        }

        GUILayout.EndArea();
    }

    void HandleLog(string message, string stackTrace, LogType type)
    {
        queue.Enqueue(new LogMessage(message, type));

        if (type == LogType.Assert && !StackTraceAssert) return;
        if (type == LogType.Error && !StackTraceError) return;
        if (type == LogType.Exception && !StackTraceException) return;
        if (type == LogType.Log && !StackTraceLog) return;
        if (type == LogType.Warning && !StackTraceWarning) return;

        string[] trace = stackTrace.Split(new char[] { '\n' });

        foreach (string t in trace)
            if (t.Length != 0) queue.Enqueue(new LogMessage("  " + t, type));
    }
}

class LogMessage
{
    public string Message;
    public LogType Type;

    public LogMessage(string msg, LogType type)
    {
        Message = msg;
        Type = type;
    }
}