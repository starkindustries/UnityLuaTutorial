using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using MoonSharp.Interpreter;

public class LuaEditor : MonoBehaviour
{
    public TextMeshProUGUI tmpTextField;
    public TextMeshProUGUI tmpConsole;

    private void Start()
    {
        // Script.DefaultOptions.DebugPrint = (s) => Debug.Log(s);
        Script.DefaultOptions.DebugPrint = (s) => ConsoleLog(s);
    }

    private void HelloWorld()
    {
        Debug.Log("Hello world");
    }    

    private void ConsoleLog(string s)
    {
        tmpConsole.text += s + "\n";
    }

    public void DidPressRunButton()
    {
        Debug.Log(tmpTextField.text);
        string scriptCode = tmpTextField.text;
        if (scriptCode[scriptCode.Length-1] == (char)8203)
        {
            Debug.Log("Removing zero-width space found at end of script.");
            scriptCode = scriptCode.Remove(scriptCode.Length - 1, 1);
        }        

        /*  
        scriptCode = @"
        -- print hello world
        print('Hello WORLD')
        ";
        */
        
        Script script = new Script();

        Debug.Log("Double quote: " + (int)'"');
        // Zero-width space
        // https://stackoverflow.com/questions/2973698/whats-html-character-code-8203        
        Debug.Log("zero-width space: \"" + (char)8203 + "\"");

        foreach(char c in scriptCode.Trim())
        {
            Debug.Log("\"" + c + "\": " + (int)c);
        }

        // script.Globals["Hello"] = (Func<>)HelloWorld;
        script.DoString(scriptCode);
        // DynValue res = script.Call(script.Globals["fact"], 5);

    }
}
