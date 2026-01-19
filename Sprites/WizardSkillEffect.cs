using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardSkillEffect : MonoBehaviour
{
    Transform PlayerTransform;
    float playerPos;
    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Destroy(gameObject, 10);
        playerPos = PlayerTransform.transform.localScale.x;
    }


    void Update()
    {
        if (playerPos == 1)
        {
            gameObject.transform.localScale = Vector3.one;
            transform.Translate(new Vector2(1, 0) * 10 * Time.deltaTime);
        }
        else if (playerPos == -1)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            transform.Translate(new Vector2(-1, 0) * 10 * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MonsterSkill"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
