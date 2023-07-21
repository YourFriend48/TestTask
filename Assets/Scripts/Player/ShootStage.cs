using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootStage : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;

    private Player _player;
    private Joystick _joystick;
    private bool _readyForShoot;
    private float _currentCooldown;
    private Vector3 _shootDirection;

    public Player Player => _player;
    public Vector3 ShootDirection => _shootDirection;

    private const float _cooldownBetweenShoots = 0.5f;

    private void Awake()
    {
        _joystick = FindObjectOfType<ShootJoystick>().GetComponent<Joystick>();
        _player = GetComponent<Player>();
        _readyForShoot = true;
    }

    private void Update()
    {
        if (_player.View.IsMine && _player.Fight.IsFighting)
            if (_player.IsAlive && _readyForShoot && _joystick.Horizontal != 0 && _joystick.Vertical != 0)
            {
                _shootDirection = new Vector3(-_joystick.Horizontal, -_joystick.Vertical);
                var bullet = PhotonNetwork.Instantiate(_bulletPrefab.name, transform.position, Quaternion.identity);
                bullet.GetComponent<Projectile>().SetParameters(_shootDirection, transform.position, _player);
                _currentCooldown = 0;
                _readyForShoot = false;
                StartCoroutine(Cooldown());
            }
    }

    private IEnumerator Cooldown()
    {
        while (_currentCooldown < _cooldownBetweenShoots)
        {
            _currentCooldown += Time.deltaTime;
            yield return null;
        }
        _readyForShoot = true;
    }
}
