using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PhotonView))]
public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            if (collision.GetComponent<Player>())
                collision.GetComponent<Player>().CollectCoin();
            if (gameObject.GetComponent<PhotonView>().IsMine)
                PhotonNetwork.Destroy(gameObject);
        }
    }
}
