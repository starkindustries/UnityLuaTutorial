using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LuaCommands : MonoBehaviour
{
    private static LuaCommands Instance;

    [SerializeField]
    private TextMeshProUGUI tmpText;

    private void Awake()
    {
        Instance = this;
    }

    public static void SetText(string text)
    {
        Instance.tmpText.text = text;
    }
}
