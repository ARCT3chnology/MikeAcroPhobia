using UnityEngine;


public enum menuName
{
    StartPanel,
    CreateRoomPanel,
    Room,
}
public class Menu : MonoBehaviour
{

    public menuName selectMenuName;
    public bool open;
    public void Open()
    {
        open = true;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        open = false;
        gameObject.SetActive(false);
    }
}
