using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour, IPunObservable
{
    [SerializeField] private Player _player;
    [SerializeField] private Image _image;
    [SerializeField] private PhotonView _view;

    private void Awake()
    {
        _player.IsDamageTaken += OnValueChangedNetwork;
    }

    private void OnValueChangedNetwork(float value, float maxValue) => _view.RPC(nameof(OnValueChanged), RpcTarget.All, value, maxValue);

    [PunRPC]
    public void OnValueChanged(float value, float maxValue)
    {
        _image.fillAmount = value / maxValue;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
            stream.SendNext(_image.fillAmount);
        else
            _image.fillAmount = (float)stream.ReceiveNext();
    }
}
