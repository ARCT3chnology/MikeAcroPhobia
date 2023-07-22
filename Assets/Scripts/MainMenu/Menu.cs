using UnityEngine;


public enum menuName
{
    StartPanel,
    CreateRoomPanel,
    Room,
    PlayPanel,
    LobbyPanel,
    RoomPanel,
    LoadingPanel
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
