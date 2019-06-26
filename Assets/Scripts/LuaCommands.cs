using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LuaCommands : MonoBehaviour
{
    private static LuaCommands Instance;
    private ButtonHandler buttonHandler;

    [SerializeField]
    private TextMeshProUGUI tmpText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        buttonHandler = FindObjectOfType<ButtonHandler>();
    }

    public static void SetText(string text)
    {
        Instance.tmpText.text = text;
    }

    public static void ShowButtons(string buttonTextString1, string buttonTextString2)
    {
        Instance.buttonHandler.ShowButtons(buttonTextString1, buttonTextString2);
    }
}
