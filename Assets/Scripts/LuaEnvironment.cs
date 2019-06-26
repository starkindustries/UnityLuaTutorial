using UnityEngine;
using MoonSharp.Interpreter;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;

public class LuaEnvironment : MonoBehaviour
{
    [SerializeField]
    private string fileName;
    private Script environment;
    private Stack<MoonSharp.Interpreter.Coroutine> coroutineStack;
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

        // Initialize the coroutine stack
        coroutineStack = new Stack<MoonSharp.Interpreter.Coroutine>();

        // Use SoftSandbox preset to keep your users safe!
        environment = new Script(CoreModules.Preset_SoftSandbox);
        environment.Globals["SetText"] = (Action<string>)LuaCommands.SetText;
        environment.Globals["ShowButtons"] = (Action<string, string>)LuaCommands.ShowButtons;
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
            coroutineStack.Push(environment.CreateCoroutine(returnValue).Coroutine);
        }
    }

    public void AdvanceScript()
    {
        // Check if the Coroutine stack is empty
        if(coroutineStack.Count > 0)
        {
            try
            {                
                MoonSharp.Interpreter.Coroutine activeCoroutine = coroutineStack.Peek();
                
                // Save the active coroutine's return value
                DynValue returnValue = activeCoroutine.Resume();

                // If the active coroutine is dead, pop it off the stack
                if (activeCoroutine.State == CoroutineState.Dead)
                {
                    coroutineStack.Pop();
                    Debug.Log("Dialogue complete");
                }

                // If the return value is a function, add it to the top of the coroutine stack
                if (returnValue.Type == DataType.Function)
                {
                    coroutineStack.Push(environment.CreateCoroutine(returnValue).Coroutine);
                }                                
            } 
            catch (ScriptRuntimeException e)
            {
                Debug.LogError(e.DecoratedMessage);
                coroutineStack.Clear();
            }
        }
        else
        {
            Debug.Log("No active dialogue");
        }        
    }
}