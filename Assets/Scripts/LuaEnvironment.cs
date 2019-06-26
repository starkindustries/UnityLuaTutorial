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
    private GameState luaGameState;


    public GameState LuaGameState
    {
        get { return luaGameState; }
    }

    private void Awake()
    {
        luaGameState = new GameState();
    }

    private IEnumerator Start()
    {
        Script.DefaultOptions.DebugPrint = (s) => Debug.Log(s);
        UserData.RegisterAssembly();

        environment = new Script();
        environment.Globals["SetText"] = (Action<string>)LuaCommands.SetText;
        environment.Globals["State"] = UserData.Create(luaGameState);

        // wait 1 frame then return the file
        yield return 1;

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