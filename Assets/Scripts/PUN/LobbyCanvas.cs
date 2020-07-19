using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCanvas : MonoBehaviour
{
    [SerializeField]
    private RoomLayoutGroup _roomListDisplay;
    public RoomLayoutGroup RoomListDisplay
    {
        get { return _roomListDisplay; }
    }

    public void OnClickRoom(string roomName)
    {
        if (PhotonNetwork.JoinRoom(roomName))
        {
            Debug.Log("Player Joined in the Room");
        }
        else
        {
            Debug.Log("Failed to join in the room, please fix the error!");
        }
    }
}
