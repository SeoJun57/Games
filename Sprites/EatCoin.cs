using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatCoin : MonoBehaviour
{
    public GameObject realCoin;
    private bool _activeCoin = false;
    public bool ActiveCoin
    {
        get { return _activeCoin; }
    }
    void Start()
    {
        Invoke("CoinActive", 1f);
    }
    void CoinActive()
    {
        _activeCoin = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _activeCoin)
        {
            PlayerStats.Instance.Gold += Random.Range(1, 10);
            CameraMove.eatCoin = true;
            Destroy(realCoin);
        }
    }
}
