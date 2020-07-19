using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour
{
    [SerializeField]
    private Text _roomNameText;
    private Text RoomNameText
    {
        get { return _roomNameText; }
    }
    public PhotonPlayer PhotonPlayer { get; private set; }

    public string RoomName { get; private set; }
   
    public bool Updated { get; set; }

    private void Start()
    {
        GameObject lobbyCanvasObj = CanvasManager.Instance.LobbyCanvas.gameObject;

        if (lobbyCanvasObj == null)
            return;

        LobbyCanvas lobbyCanvas = lobbyCanvasObj.GetComponent<LobbyCanvas>();

        GetComponent<Button>().onClick.AddListener(() => lobbyCanvas.OnClickRoom(RoomNameText.text));
    }

    private void OnDestroy()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }

    public void SetRoomNameText(string text)
    {
        RoomName = text;
        RoomNameText.text = RoomName;
    }
}
