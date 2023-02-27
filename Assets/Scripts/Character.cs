using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Character : MonoBehaviour
{
    private GameManager _GameManager;
    private CharacterForward _CharacterForward;
    public GameObject losePanel;
    private void Start()
    {
        _GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        _CharacterForward = GetComponentInParent<CharacterForward>();
        
    }

    private void Update()
    {
        if (Time.timeScale != 0 && !_CharacterForward._MainScreen.activeSelf)
        {
            #region Editor Input

            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (Input.GetAxis("Mouse X") < 0)
                {
                    transform.position = Vector3.Lerp(transform.position,
                        new Vector3(transform.position.x - .22f,transform.position.y,transform.position.z),.3f);
                }
                if (Input.GetAxis("Mouse X") > 0)
                {
                    transform.position = Vector3.Lerp(transform.position,
                        new Vector3(transform.position.x + .22f,transform.position.y,transform.position.z),.3f);
                }
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.position = Vector3.Lerp(transform.position,
                    new Vector3(transform.position.x - .22f,transform.position.y,transform.position.z),.3f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position = Vector3.Lerp(transform.position,
                    new Vector3(transform.position.x + .22f,transform.position.y,transform.position.z),.3f);
            }
            

            #endregion
            
            if (Input.touchCount > 0)
            {
                if (MobileInput.instance.Direction.x > 0)
                {
                    transform.position = Vector3.Lerp(transform.position,
                        new Vector3(transform.position.x + .35f,transform.position.y,transform.position.z),.3f);
                }
                else if (MobileInput.instance.Direction.x < 0)
                {
                    transform.position = Vector3.Lerp(transform.position,
                        new Vector3(transform.position.x - .35f,transform.position.y,transform.position.z),.3f);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Addition") || other.CompareTag("Extraction") || other.CompareTag("Multiplication") || other.CompareTag("Division"))
        {
            int index = int.Parse(other.name);
            _GameManager.AI_Instantiate(other.tag,index,other.transform);
            other.GetComponentInParent<Layer>().gameObject.SetActive(false);
        }
        else if (other.CompareTag("Finish"))
        {
            CharacterForward.isStart = false;
            CharacterForward.isFinish = true;
            other.GetComponent<LevelSettings>().Rating(gameObject);
        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Static"))
        {
            if (collision.gameObject.transform.position.x >= 0)
                transform.position = new Vector3(transform.position.x - .4f, transform.position.y, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x + .4f, transform.position.y, transform.position.z);
        }
        if (collision.gameObject.CompareTag("Cutter"))
        {
            Time.timeScale = 0;
            losePanel.gameObject.SetActive(true);
        }
    }
}
