using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    [SerializeField]
    private LobbyCanvas _lobbyCanvas;
    public LobbyCanvas LobbyCanvas
    {
        get { return _lobbyCanvas; }
    }

    private void Awake()
    {
        Instance = this;
    }
}
