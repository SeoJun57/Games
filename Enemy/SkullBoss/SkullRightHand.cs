using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullRightHands : MonoBehaviour
{
    public GameObject Cast2;
    int v = 1;
    private void Awake()
    {
    }
    private void Start()
    {
        StartCoroutine(CoAttack());
    }
    private void Update()
    {
        if (transform.position.y >= 2)
        {
            v = -1;
        }
        else if (transform.position.y <= -4)
        {
            v = 1;
        }
        transform.Translate(new Vector3(0, v, 0) * Time.deltaTime);
    }
    IEnumerator CoAttack()
    {
        while (true)
        {
            int ran = Random.Range(0, 101);
            if (ran > 20)
            {
                Instantiate(Cast2, transform.position, Quaternion.identity);
            }

            yield return new WaitForSeconds(3);
        }
    }
}
