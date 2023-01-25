using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOrJoinRoomPanel : MonoBehaviour
{
    private GameCanvas _gamePanel;
    [SerializeField] CreateRoom _createRoomMenu;
    [SerializeField] RoomListingMenu roomListingMenu;
    public void firstInitialize(GameCanvas panel)
    {
        _gamePanel = panel;
        _createRoomMenu.firstInitialize(panel);
        roomListingMenu.FirstInitialize(panel);
    }


     
}
