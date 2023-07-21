using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class Fight : MonoBehaviour
{

    private bool _isFighting = false;
    private List<Player> _players;

    public bool IsFighting => _isFighting;

    public event Action<Player> FightOver;

    private void Start()
    {
        StartCoroutine(CheckNewPlayersForBegin());
    }

    private IEnumerator CheckNewPlayersForBegin()
    {
        while (CheckLastAlive())
            yield return null;
        StartCoroutine(Fighting());
    }

    private IEnumerator Fighting()
    {
        _isFighting = true;
        while (_isFighting)
        {
            if (CheckLastAlive())
            {
                _isFighting = false;
                FightOver.Invoke(_players.First());
            }
            yield return null;
        }
    }

    private bool CheckLastAlive()
    {
        _players = FindObjectsOfType<Player>().ToList();
        for (int i = 0; i < _players.Count; i++)
            if (!_players[i].IsAlive)
                _players.RemoveAt(i);
        if (_players.Count > 1)
            return false;
        else
            return true;
    }
}
