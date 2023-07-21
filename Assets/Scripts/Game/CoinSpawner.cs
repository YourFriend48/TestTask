using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _positions;
    [SerializeField] private GameObject _coinPrefab;

    private void Start()
    {
        foreach (var position in _positions)
            PhotonNetwork.Instantiate(_coinPrefab.name, position.position, Quaternion.identity);
    }
}
