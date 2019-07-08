using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;
using System;

public class Factorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Factorial (3): " + MoonSharpFactorial());
        Debug.Log("Factorial2 (4): " + MoonSharpFactorial2());
        Debug.Log("CallbackTest (5): " + CallbackTest());
    }

    // https://www.moonsharp.org/getting_started.html
    // https://www.moonsharp.org/tutorial2.html
    private double MoonSharpFactorial()
    {
        string scriptCode = @"    
		-- defines a factorial function
		function fact (n)
			if (n == 0) then
				return 1
			else
				return n*fact(n - 1)
			end
		end

		return fact(mynumber)";

        Script script = new Script();
        script.Globals["mynumber"] = 3;
        DynValue res = script.DoString(scriptCode);
        return res.Number;
    }

    double MoonSharpFactorial2()
    {
        string scriptCode = @"    
		-- defines a factorial function
		function fact (n)
			if (n == 0) then
				return 1
			else
				return n*fact(n - 1)
			end
		end";

        Script script = new Script();

        script.DoString(scriptCode);

        DynValue res = script.Call(script.Globals["fact"], 4);

        return res.Number;
    }

    private static int Multiply(int a, int b)
    {
        return a * b;
    }

    private static double CallbackTest()
    {
        string scriptCode = @"    
        -- defines a factorial function
        function fact (n)
            if (n == 0) then
                return 1
            else
                return Multiply(n, fact(n - 1));
            end
        end";

        Script script = new Script();
        script.Globals["Multiply"] = (Func<int, int, int>)Multiply;
        script.DoString(scriptCode);
        DynValue res = script.Call(script.Globals["fact"], 5);
        return res.Number;
    }    
}
