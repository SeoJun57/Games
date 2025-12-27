using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public AudioClip HitSound;
    public AudioClip CoindSound;
    private AudioSource _as;

    public float shakePower = 0.01f;
    public float shakeTime = 0.5f;
    public static bool startMove = false;
    public static bool eatCoin = false;
    public static bool GameClearMove = false;   


    private bool _donduplicate = true;
    private void Start()
    {
        _as = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(startMove && _donduplicate)
        {
            StartCoroutine(CoCameraMove());
            _donduplicate = false;
        }
        if(eatCoin && _donduplicate)
        {
            StartEatCoin();
        }
        if(GameClearMove && _donduplicate)
        {
            StartCoroutine(CoGameClearMove());
        }
    }
    IEnumerator CoCameraMove()
    {
        _as.PlayOneShot(HitSound);
        Vector3 savePos = transform.localPosition;
        float time = shakeTime;
        while(time > 0)
        {
            transform.localPosition =  savePos + new Vector3( Random.Range(-0.2f,0.2f), Random.Range(-0.1f, 0.1f), 0) * shakePower; 
            time -= 5 * Time.deltaTime;
            yield return null;
        }
        transform.localPosition = savePos;
    
        startMove = false;
        _donduplicate=true;
    }
    IEnumerator CoGameClearMove()
    {

        Vector3 savePos = transform.localPosition;
        float time = 3;
        while(time > 0)
        {
            transform.localPosition =  savePos + new Vector3( Random.Range(-0.4f,0.4f), Random.Range(-0.1f, 0.1f), 0) * shakePower; 
            time -= 5 * Time.deltaTime;
            yield return null;
        }
        transform.localPosition = savePos;
        GameClearMove = false;
        _donduplicate=true;
    }
    void StartEatCoin()
    {
        _donduplicate = false;
        _as.PlayOneShot(CoindSound);
        eatCoin = false;
        _donduplicate = true;
    }
}
