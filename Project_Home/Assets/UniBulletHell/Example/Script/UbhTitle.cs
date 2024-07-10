using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UbhTitle : UbhMonoBehaviour
{
    const string TITLE_PC = "Press X";
    const string TITLE_MOBILE = "Tap To Start";
    [SerializeField]
    Text _StartGUIText;

    void Start ()
    {
        _StartGUIText.text = UbhUtil.IsMobilePlatform() ? TITLE_MOBILE : TITLE_PC;
    }
}