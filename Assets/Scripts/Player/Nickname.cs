using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Nickname : MonoBehaviour
{
    private PhotonView _view;
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _view = GetComponent<PhotonView>();
    }

    private void Start()
    {
        _view.RPC(nameof(SetNickname), RpcTarget.All);
    }

    [PunRPC]
    private void SetNickname()
    {
        _text.text = _view.Owner.NickName;
    }
}
