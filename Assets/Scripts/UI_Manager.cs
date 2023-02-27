using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HyperCasualLibrary;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public GameObject _StartPanel;
    public GameObject _SettingsPanel;
    public Animator _PlayerAnimator;
    public MemoryManagement _MemoryManagement = new MemoryManagement();
    public Slider GameMusicSlider;
    public Slider GameFxSlider;
    public GameObject PlayerPosition;
    public Slider LevelSlider;
    
    public TMP_Text levelNameText;
    private GameObject FinishPoint;
    private Scene currentScene;
    
    private void Start()
    {
        FinishPoint = GameObject.FindWithTag("Finish");
        currentScene = SceneManager.GetActiveScene();
        if (_MemoryManagement.ReadData_int("CurrentLevel") == 0)
        {
            int level = _MemoryManagement.ReadData_int("CurrentLevel");
            levelNameText.text = "Level " + (level + 1).ToString() ;
        }
        else
        {
            levelNameText.text = "Level " + _MemoryManagement.ReadData_int("CurrentLevel").ToString();
        }
        float Distance = Vector3.Distance(PlayerPosition.transform.position,FinishPoint.transform.position);
        LevelSlider.maxValue = Distance;
        GameMusicSlider.value = _MemoryManagement.ReadData_float("GameMusic");
        GameFxSlider.value = _MemoryManagement.ReadData_float("GameFx");
    }

    private void Update()
    {
        float Distance = Vector3.Distance(PlayerPosition.transform.position,FinishPoint.transform.position);
        LevelSlider.value = Distance;
    }

    public void ChangeState(bool state)
    {
        _PlayerAnimator.SetBool("tapToStart",true);
        CharacterForward.isStart = true;
        _StartPanel.SetActive(state);
    }

    public void SettingsState(bool state)
    {
        _SettingsPanel.SetActive(state);
        if (state)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ChangeValue(string valueType)
    {
        switch (valueType)
        {
            case "Music":
                _MemoryManagement.WriteData_float("GameMusic",GameMusicSlider.value);
                break;
            case "Fx":
                _MemoryManagement.WriteData_float("GameFx",GameFxSlider.value);
                break;
        }
    }
}
