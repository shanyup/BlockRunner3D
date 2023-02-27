using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterForward : MonoBehaviour
{
    public GameObject _MainScreen;
    public static bool isStart = false;
    public static bool isFinish = false;
    public Transform _FinishTarget;

    private float runSpeed = 3f;
    private void Start()
    {
        if (_FinishTarget == null)
        {
            GameObject.FindWithTag("Finish");
        }
        isStart = false;
        isFinish = false;
    }
    
    void FixedUpdate()
    {
        if(isStart)
            transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
        if (isFinish)
        {
            transform.position = Vector3.Lerp(gameObject.GetComponentInChildren<Character>().transform.position, _FinishTarget.position, 0.125f);
            gameObject.GetComponent<CharacterForward>().enabled = false;
        }
    }
}
