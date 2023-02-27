using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //private static GameObject instance;
    private GameObject listObj1;
    private GameObject listObj2;
    public AudioSource _GameMusic;
    public List<AudioSource> _GameFxList = new List<AudioSource>();

    private void Start()
    {
        _GameMusic.volume = PlayerPrefs.GetFloat("GameMusic");
        UpdateGameFxList();
        /*DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = gameObject;
        else
            Destroy(gameObject);*/
    }

    private void UpdateGameFxList()
    {
        listObj1 = GameObject.FindWithTag("Effects1");
        listObj2 = GameObject.FindWithTag("Effects2");
        for (int i = 0; i < listObj1.transform.childCount; i++)
        {
            _GameFxList.Add(listObj1.transform.GetChild(i).GetComponentInChildren<AudioSource>());
            _GameFxList.Add(listObj2.transform.GetChild(i).GetComponentInChildren<AudioSource>());
        }
        
        foreach (var item in _GameFxList)
        {
            item.volume = PlayerPrefs.GetFloat("GameFx");
        }
    }

    private void Update()
    {
        _GameMusic.volume = PlayerPrefs.GetFloat("GameMusic");
        if (_GameFxList != null)
        {
            foreach (var item in _GameFxList)
            {
                item.volume = PlayerPrefs.GetFloat("GameFx");
            }
        }
    }
}
