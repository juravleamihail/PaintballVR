using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public Vector3 spawnButtonPos;
    public Vector3 offset;
    public GameObject buttonPrefab;
    public InputField TextField;
    public NetworkManager manager;

    public void AddLobby()
    {
        if (TextField.text == "")
            return;

        var newButton = Instantiate(buttonPrefab, this.transform);
        newButton.transform.localPosition = spawnButtonPos;
        newButton.GetComponentInChildren<Text>().text = TextField.text;
        spawnButtonPos -= offset;

        manager.StartMatchMaker();
        manager.matchMaker.CreateMatch(TextField.text, 10, true, "", "", "", 0, 0, manager.OnMatchCreate);
    }
}
