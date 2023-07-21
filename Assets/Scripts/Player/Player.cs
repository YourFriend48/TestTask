using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(PhotonTransformViewClassic))]
[RequireComponent(typeof(PhotonAnimatorView))]
public class Player : MonoBehaviour, IPunObservable
{

    [SerializeField] private float _speed;

    private PhotonView _view;
    private BoxCollider2D _boxCollider2d;
    private Animator _animator;
    private Joystick _joystick;
    private Fight _fight;
    private SpriteRenderer _sprite;
    private float _currentHP;
    private bool _isAlive;
    private string _nickname;
    private int _coinCollected = 0;

    public Fight Fight => _fight;
    public int CoinCollected => _coinCollected;
    public string Nickname => _nickname;
    public PhotonView View => _view;
    public bool IsAlive => _isAlive;

    public event Action<float, float> IsDamageTaken;

    private const float _maxHP = 100;
    private const string _runAnimatorParameter = "IsRunning";
    private const string _hitAnimatorParameter = "TakeDamage";
    private const string _dieAnimatorParameter = "Die";


    private void Awake()
    {
        _fight = FindObjectOfType<Fight>();
        _view = GetComponent<PhotonView>();
        _boxCollider2d = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _joystick = FindObjectOfType<MoveJoystick>().GetComponent<Joystick>();
        _currentHP = _maxHP;
        _isAlive = true;
        _boxCollider2d.isTrigger = false;
        _nickname = _view.Owner.NickName;
    }

    private void Update()
    {
        if (_view.IsMine)
            if (_isAlive && _fight.IsFighting)
            {
                if (_joystick.Horizontal != 0 && _joystick.Vertical != 0)
                    _animator.SetBool(_runAnimatorParameter, true);
                else
                    _animator.SetBool(_runAnimatorParameter, false);
                transform.Translate(new Vector3(MathMovement(-_joystick.Horizontal), MathMovement(-_joystick.Vertical)));
                if (-_joystick.Horizontal > 0 && _sprite.flipX == true)
                    _sprite.flipX = false;
                else if (-_joystick.Horizontal < 0 && _sprite.flipX == false)
                    _sprite.flipX = true;
            }
    }

    public void TakeDamage(float damage) => _view.RPC(nameof(TakeDamageNetwork), RpcTarget.All, damage);


    [PunRPC]
    public void TakeDamageNetwork(float damage)
    {
        _animator.SetTrigger(_hitAnimatorParameter);
        _currentHP -= damage;
        IsDamageTaken.Invoke(_currentHP, _maxHP);
        if (_currentHP <= 0)
            _view.RPC(nameof(Die), RpcTarget.All);
    }

    [PunRPC]
    private void Die()
    {
        _isAlive = false;
        _animator.SetBool(_dieAnimatorParameter, true);
        _boxCollider2d.enabled = false;
    }


    private float MathMovement(float move) => move * Time.deltaTime * _speed;
    public void CollectCoin() => _coinCollected++;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
            stream.SendNext(_coinCollected);
        else
            _coinCollected = (int)stream.ReceiveNext();
    }
}
