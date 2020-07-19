using UnityEngine;
using Photon;
using UnityEngine.UI;

public class CreateNewRoom : PunBehaviour
{
    [SerializeField]
    private Text _roomName;
    [SerializeField]
    private GameObject CurrentRoom, LobbyRoom;
    private Text RoomName
    {
        get { return _roomName; }
    }

    private void Start()
    {
        _roomName.text = "default";
    }

    public void OnClick_CreateRoom()
    {
        CreateRoom();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            CreateRoom();
        }
    }

    void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4 };

        if (PhotonNetwork.CreateRoom(RoomName.text, roomOptions, TypedLobby.Default))
        {
            print("Create room succed");
            CurrentRoom.SetActive(true);
            LobbyRoom.SetActive(false);
        }
        else
        {
            print("Create room failed");
        }
    }

    public override void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
        print("Create Room Failed: " + codeAndMsg[1]);
    }
}
