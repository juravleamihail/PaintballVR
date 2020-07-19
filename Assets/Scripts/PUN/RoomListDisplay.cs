using UnityEngine;

public class RoomListDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject _roomPrefab;
    private GameObject RoomPrefab
    {
        get { return _roomPrefab; }
    }
}
