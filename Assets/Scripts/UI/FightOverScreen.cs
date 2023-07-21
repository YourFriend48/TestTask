using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FightOverScreen : MonoBehaviour
{
    [SerializeField] private Fight _fight;
    [SerializeField] private GameObject _fightoverScreen;
    [SerializeField] private TMP_Text _nicknameField;
    [SerializeField] private TMP_Text _coinCountField;

    private void OnEnable()
    {
        _fight.FightOver += ShowEndScreen;
        _fightoverScreen.SetActive(false);
    }

    private void OnDisable()
    {
        _fight.FightOver += ShowEndScreen;
    }

    private void ShowEndScreen(Player player)
    {
        _fightoverScreen.SetActive(true);
        _nicknameField.text = "Winner: " + player.Nickname;
        _coinCountField.text = "Collected " + player.CoinCollected + " coins";
    }

}
