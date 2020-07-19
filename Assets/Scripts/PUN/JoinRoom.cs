using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinRoom : MonoBehaviour
{
    public RoomListing roomListing;
    GameObject Canvas;

    private void Awake()
    {
        Canvas = GameObject.Find("Canvas");
    }

    public void OnClick_JoinRoom()
    {
        if (PhotonNetwork.JoinRoom(roomListing.name))
        {
            Debug.Log("Player Joined in the Room");
            Utils.FindObject(Canvas, "CurrentRoom").SetActive(true);
            Utils.FindObject(Canvas, "Lobby").SetActive(false);
        }
        else
        {
            Debug.Log("Failed to join in the room, please fix the error!");
        }
    }
}
