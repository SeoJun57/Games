using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullLeftHands : MonoBehaviour
{
    public GameObject Cast;
    Transform CastPos;
    int v = 1;
    bool isDonDu = true;
    private void Awake()
    {

        CastPos = GameObject.FindGameObjectWithTag("CastPos").transform;
    }
    private void Start()
    {
        StartCoroutine(CoAttack());
    }
    private void Update()
    {
        if (transform.position.y >= 2.5f)
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

        int ran = Random.Range(0, 101);
        if (ran > 40 && isDonDu)
        {
            isDonDu = false;
            Instantiate(Cast, CastPos.transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(3);
        isDonDu = true;
        StartCoroutine(CoAttack());
    }
}
