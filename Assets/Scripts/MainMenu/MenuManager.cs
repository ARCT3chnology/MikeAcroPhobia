using System.Runtime.CompilerServices;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Menu[] menus;
    public Menu[] gameMenus 
    { 
        get 
        {
            return menus;
        }
        set { } 
    }
    public static MenuManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        ResetMenu();
    }

    public void OpenMenu(menuName name)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].selectMenuName == name)
            {
                menus[i].Open();
            }
            else if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
    }

    void ResetMenu()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (PlayerPrefs.GetInt(menus[i].selectMenuName.ToString()) == 1)
            {
                menus[i].Open();
            }
            else
            {
                if (menus[i].open)
                {
                    menus[i].Open();
                }
                else if (!menus[i].open)
                {
                    menus[i].Close();
                }
            }
        }
        RemovePlayerPrefsForMenu();
    }

    private void RemovePlayerPrefsForMenu()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            //Debug.Log("DELETEING SAVED MENU PLAYER PREFS");
            //Debug.Log("Saved Prefs are :" + menus[i].selectMenuName);
            PlayerPrefs.DeleteKey(menus[i].selectMenuName.ToString());
        }
    }

    public void abcd(menuName name)
    {
        //Saving menu player pref
        PlayerPrefs.SetInt(name.ToString(),1);
        for (int i = 0; i < menus.Length; i++)
        {
            
            if (menus[i].selectMenuName == name && PlayerPrefs.GetInt(name.ToString()) == 1)
            {
                menus[i].Open();
            }
            else
            {
                CloseMenu(menus[i]);
            }
        }
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }




}
