using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter; 

[MoonSharpUserData]
public class GameState
{
    private string playerName;

    public string PlayerName
    {
        get
        {
            return playerName;
        }
        [MoonSharpHidden]
        set
        {
            playerName = value;
        }
    }
}
