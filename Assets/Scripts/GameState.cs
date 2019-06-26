using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter; 

[MoonSharpUserData]
public class GameState
{
    private string playerName;
    private int buttonSelected;
    private HashSet<string> flags;

    // Note: keep the constructor hidden or else Lua will be able to 
    // construct multiple instances of GameState, which is not intended.
    [MoonSharpHidden]
    public GameState()
    {
        flags = new HashSet<string>();
    }

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

    public int ButtonSelected
    {
        get
        {
            return buttonSelected;
        }
        [MoonSharpHidden]
        set
        {
            buttonSelected = value;
        }
    }    

    public bool GetFlag(string flag)
    {
        return flags.Contains(flag);
    }

    public void SetFlag(string flag, bool set)
    {
        if (set)
        {
            flags.Add(flag);
        }
        else
        {
            flags.Remove(flag);
        }
    }
}
