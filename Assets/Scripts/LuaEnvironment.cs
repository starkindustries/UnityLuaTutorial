using UnityEngine;
using MoonSharp.Interpreter;

public class LuaEnvironment : MonoBehaviour
{
    private Script environment;
    void Start()
    {
        Script.DefaultOptions.DebugPrint = (s) => Debug.Log(s);
        environment = new Script();

        environment.DoString("print 'Hello world!'");
    }
}