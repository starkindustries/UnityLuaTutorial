using UnityEngine;
using MoonSharp.Interpreter;
using System.IO;
using System;
using System.Collections;

public class LuaEnvironment : MonoBehaviour
{
    [SerializeField]
    private string fileName;
    private Script environment;
    private MoonSharp.Interpreter.Coroutine activeCoroutine;

    void Start()
    {
        Script.DefaultOptions.DebugPrint = (s) => Debug.Log(s);

        environment = new Script();
        environment.Globals["SetText"] = (Action<string>)LuaCommands.SetText;

        // environment.DoString("print 'Hello world!'");

        LoadFile(fileName);
        AdvanceScript();
    }

    private void LoadFile(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        DynValue returnValue = DynValue.Nil;

        try
        {
            // 'using' keyword indicates that unity will dispose this stream on exiting this code block
            using (BufferedStream stream = new BufferedStream(new FileStream(filePath, FileMode.Open, FileAccess.Read)))
            {
                returnValue = environment.DoStream(stream);
            }
        } catch (SyntaxErrorException e)
        {
            Debug.LogError(e.DecoratedMessage);
        }        

        if(returnValue.Type == DataType.Function)
        {
            activeCoroutine = environment.CreateCoroutine(returnValue).Coroutine;            
        }
        else
        {
            activeCoroutine = null;
        }
    }

    public void AdvanceScript()
    {
        if(activeCoroutine != null)
        {            
            if (activeCoroutine.State == CoroutineState.Dead)
            {
                activeCoroutine = null;
                Debug.Log("Dialogue complete");
                return;
            }
            activeCoroutine.Resume();
            return;
        }
        Debug.Log("No active dialogue");
    }
}