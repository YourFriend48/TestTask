using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PhotonTransformViewClassic))]
[RequireComponent(typeof(PhotonView))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Player _shootedPlayer;
    private Vector3 _direction;
    private float _currentLifeTime;

    private const float _maxLifeTime = 3;
    private const float _damage = 20;

    private void Awake()
    {
        _currentLifeTime = 0;
    }

    private void Update()
    {

        _currentLifeTime += Time.deltaTime;
        transform.Translate(new Vector3(_direction.x * Time.deltaTime * _speed, _direction.y * Time.deltaTime * _speed));

        if (_currentLifeTime >= _maxLifeTime)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != _shootedPlayer && _shootedPlayer != null)
        {
            if (collision.GetComponent<Player>())
                collision.GetComponent<Player>().TakeDamage(_damage);
            if (gameObject.GetComponent<PhotonView>().IsMine && !collision.GetComponent<Coin>())
                PhotonNetwork.Destroy(gameObject);
        }
    }

    public void SetParameters(Vector3 direction, Vector3 position, Player player)
    {
        _direction = direction;
        transform.position = position;
        _shootedPlayer = player;
    }

}
