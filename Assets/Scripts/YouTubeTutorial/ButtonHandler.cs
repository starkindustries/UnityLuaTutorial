using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonHandler : MonoBehaviour
{
    private LuaEnvironment lua;

    [SerializeField]
    private GameObject buttonParent;
    [SerializeField]
    private TextMeshProUGUI buttonText1;
    [SerializeField]
    private TextMeshProUGUI buttonText2;

    private void Start()
    {
        lua = FindObjectOfType<LuaEnvironment>();
        buttonParent.SetActive(false);
    }

    public void DidPressButton(int index)
    {
        Debug.Log("Pressed button #:" + index);
        lua.LuaGameState.ButtonSelected = index + 1;
        buttonParent.SetActive(false);
        lua.AdvanceScript();
    }

    public void ShowButtons(string buttonTextString1, string buttonTextString2)
    {
        buttonText1.text = buttonTextString1;
        buttonText2.text = buttonTextString2;
        buttonParent.gameObject.SetActive(true);
    }

    public bool ButtonsAreActive()
    {
        return buttonParent.gameObject.activeSelf;
    }
}
