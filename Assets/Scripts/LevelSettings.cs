using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyperCasualLibrary;

public class LevelSettings : MonoBehaviour
{
    private GameManager _GameManager;
    public int LevelGemAmount;
    public int MinCharacter;
    public GameObject WinPanel;
    public GameObject LosePanel;
    public ParticleSystem[] WinParticleSystem;
    public AudioClip _WinSound;
    public AudioClip _LoseSound;

    private LevelGenerator _LevelGenerator;
    private MemoryManagement _MemoryManagement = new MemoryManagement();

    private void Awake()
    {
        _LevelGenerator = FindObjectOfType<LevelGenerator>();
    }

    private void Start()
    {
        
        if (WinPanel == null)
        {
            WinPanel = _LevelGenerator.WinPanel;
        }

        if (LosePanel == null)
        {
            LosePanel = _LevelGenerator.LosePanel;
        }
        Time.timeScale = 1;
        _GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    public void Rating(GameObject gameObject)
    {
        foreach (var item in _GameManager.AI_Characters)
        {
            if (item.activeInHierarchy)
            {
                item.SetActive(false);
            }
        }
        if (GameManager.currentCharacter >= MinCharacter)
        {
            gameObject.GetComponent<Animator>().SetBool("isWin",true);
            foreach (var item in WinParticleSystem)
            {
                item.Play();
            }

            _WinSound.LoadAudioData();
            WinPanel.SetActive(true);
            LosePanel.SetActive(false);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("isLose",true);
            _LoseSound.LoadAudioData();
            LosePanel.SetActive(true);
            WinPanel.SetActive(false);
        }
    }
}
