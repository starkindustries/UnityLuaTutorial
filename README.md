# UnityLuaTutorial

Based on Ned Makes Games's Tutorial:

**Using Lua scripting for branching dialogue trees!**  
[https://www.youtube.com/watch?v=ngGJDI44z48](https://www.youtube.com/watch?v=ngGJDI44z48)

## Timestamps for the tutorial

- 6:24 Importing MoonSharp plugin
- 10:00 Scene setup
- 29:16 Lua HelloWorld
- 34:54 Load a Lua file from StreamingAssets
- 36:28 Set text
- 43:22 Coroutines and waiting for input
- 1:03:29 Get data from Unity into Lua
- 1:12:51 Branching dialogue
- 1:32:29 Saving and reading flags
- 1:39:34 Nest coroutines using a coroutine stack
- 1:49:35 Bells and whistles

## Super duper basic script:

```csharp
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
```

Add this script as a component to a gameobject and run.

HelloWorld!
