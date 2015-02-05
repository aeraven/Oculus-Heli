using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("OculusTH");
    }

    void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        PhotonNetwork.JoinRandomRoom();
    }

    void  OnPhotonRandomJoinFailed() 
    {
        Debug.Log("OnJoinRandomRoomFailed");
        PhotonNetwork.CreateRoom("yeah");
    }

    void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
    }
}
