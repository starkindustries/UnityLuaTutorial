using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceInput : MonoBehaviour
{
    private LuaEnvironment lua;
    private ButtonHandler buttonHandler;

    private void Start()
    {
        lua = FindObjectOfType<LuaEnvironment>();
        buttonHandler = FindObjectOfType<ButtonHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonHandler.ButtonsAreActive())
        {
            return;
        }

        if (Input.GetButtonDown("Submit"))
        {
            lua.AdvanceScript();
        }
    }
}
