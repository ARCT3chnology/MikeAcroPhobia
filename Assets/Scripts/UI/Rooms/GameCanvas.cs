using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] CreateOrJoinRoomPanel _createOrJoinRoomPanel;
    public CreateOrJoinRoomPanel createOrJoinRoomPanel { get { return _createOrJoinRoomPanel; } }
    [SerializeField] RoomPanel _roomPanel;
    public RoomPanel roomPanel { get { return _roomPanel; } }

    private void Awake()
    {
        FirstInitialize();
    }

    private void FirstInitialize()
    {
        createOrJoinRoomPanel.firstInitialize(this);
        roomPanel.firstInitialize(this);
    }
}
