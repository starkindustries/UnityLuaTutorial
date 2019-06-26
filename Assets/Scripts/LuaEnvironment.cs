using UnityEngine;
using MoonSharp.Interpreter;
using System.IO;
using System;

public class LuaEnvironment : MonoBehaviour
{
    [SerializeField]
    private string fileName;
    private Script environment;

    void Start()
    {
        Script.DefaultOptions.DebugPrint = (s) => Debug.Log(s);

        environment = new Script();
        environment.Globals["SetText"] = (Action<string>)LuaCommands.SetText;

        // environment.DoString("print 'Hello world!'");

        LoadFile(fileName);
    }

    private void LoadFile(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        
        // 'using' keyword indicates that unity will dispose this stream on exiting this code block
        using (BufferedStream stream = new BufferedStream(new FileStream(filePath, FileMode.Open, FileAccess.Read)))
        {
            environment.DoStream(stream);
        }
    }
}