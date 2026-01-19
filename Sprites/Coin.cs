using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody2D _rb;
    public EatCoin ec;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Vector2 randomPos = new Vector2(Random.Range(-3, 3), Random.Range(3, 6));
        _rb.AddForce(randomPos, ForceMode2D.Impulse);

    }
    private void Update()
    {
        float dis = Vector3.Distance(PlayerStats.Instance.PlayerTransform, transform.position);
        float h = PlayerStats.Instance.PlayerTransform.x - transform.position.x;
        float v = PlayerStats.Instance.PlayerTransform.y - transform.position.y;
        if (ec.ActiveCoin)
        {
            transform.Translate(new Vector3(h, v, 0).normalized * 17 *  Time.deltaTime);
            
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.CompareTag("Wall"))
        {
            _rb.velocity = Vector3.zero;
        }
    }
}
