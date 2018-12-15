using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField]
    private Menu[] menus;
    [SerializeField]
    private int currentMenuIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void SwitchMenu(int menuIndex)
    {
        menus[currentMenuIndex].Disabe();
        currentMenuIndex = menuIndex;
        menus[currentMenuIndex].Enable();
    }
}
