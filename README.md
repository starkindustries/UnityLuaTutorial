# UnityLuaTutorial

https://www.youtube.com/watch?v=ngGJDI44z48

Super duper basic script:

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
