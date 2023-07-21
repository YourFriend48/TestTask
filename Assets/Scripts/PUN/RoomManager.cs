using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField _createRoomField;
    [SerializeField] private TMP_InputField _joinRoomField;
    [SerializeField] private TMP_InputField _nicknameField;
    private ExitGames.Client.Photon.Hashtable playerInfo = new ExitGames.Client.Photon.Hashtable();

    public void CreateRoom()
    {
        if (_nicknameField.text.Length > 0)
        {
            SetNickname();
            PhotonNetwork.CreateRoom(_createRoomField.text);
        }
    }

    public void JoinRoom() 
    {
        if (_nicknameField.text.Length > 0)
        {
            SetNickname();
            PhotonNetwork.JoinRoom(_joinRoomField.text);
        }
    }

    public override void OnJoinedRoom() => PhotonNetwork.LoadLevel("Game");

    public void SetNickname()
    {
        playerInfo["nickname"] = _nicknameField.text;
        PhotonNetwork.NickName = _nicknameField.text;
        PhotonNetwork.SetPlayerCustomProperties(playerInfo);
    }

}
