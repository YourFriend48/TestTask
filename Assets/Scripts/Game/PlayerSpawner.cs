using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _characterPrefab;
    [SerializeField] private List<Vector2> _spawnPositions;
    private Fight _fight;

    private void Start()
    {
        _fight = GetComponent<Fight>();
        PhotonNetwork.Instantiate(SetCharracter().name, SetTransform(), Quaternion.identity);
    }

    public GameObject SetCharracter()
    {
        int random = Random.Range(0, _characterPrefab.Count);
        GameObject character = _characterPrefab[random];
        return character;
    }

    public Vector2 SetTransform()
    {
        int random = Random.Range(0, _characterPrefab.Count);
        Vector2 position = _spawnPositions[random];
        return position;
    }
}
