using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : MonoBehaviour
{
    [SerializeField] PlayerListingMenu _playerListingsMenu;
    [SerializeField] LeaveRoomMenu _leaveRoomMenu;
    public LeaveRoomMenu leaveRoomMenu { get { return _leaveRoomMenu; } }
    private GameCanvas _gamePanel;
    [SerializeField] Text _text;
    public void firstInitialize(GameCanvas panel)
    {
        _gamePanel = panel;
        _playerListingsMenu.FirstInitialize(panel);
        _leaveRoomMenu.FirstInitialize(panel);
    }

    public void show()
    {
        gameObject.SetActive(true);
    }

    public void hide()
    {
        gameObject.SetActive(false);
    }

    public void UpdatePlayerCount(int totalPlayer, int CurrentPlayer)
    {
        _text.text = CurrentPlayer.ToString() + " / " + totalPlayer.ToString();
    }
}
