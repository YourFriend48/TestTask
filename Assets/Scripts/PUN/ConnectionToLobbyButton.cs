using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectionToLobbyButton : MonoBehaviourPunCallbacks
{


    public void OnToLobbyButtonClick() => PhotonNetwork.LeaveRoom();

    public override void OnLeftRoom() => SceneManager.LoadScene("Lobby");

}
