using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyGameNetwork : Photon.PunBehaviour
{
    // Use this for initialization
    private void Start()
    {
        if (!PhotonNetwork.connected)
        {
            print("Connecting to server..");
            PhotonNetwork.ConnectUsingSettings("0.0.0");
        }
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to master.");
        PhotonNetwork.automaticallySyncScene = false;
        PhotonNetwork.playerName = PlayerGameNetwork.Instance.PlayerName;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        print("Joined lobby.");

        if (!PhotonNetwork.inRoom)
            CanvasManager.Instance.LobbyCanvas.transform.SetAsLastSibling();
    }
}
