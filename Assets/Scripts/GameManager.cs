using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using HyperCasualLibrary;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text currentCharacterText;
    public GameObject _LoadScene;
    public Slider _LoadSlider;
    public List<GameObject> AI_Characters;
    public List<GameObject> AI_SpawnEffects;
    public List<GameObject> AI_DeathEffects;
    public static int currentCharacter = 1;

    [Header("Data")] public List<MarketItems> _MarketItems;
    
    [Header("Customization")] 
    public GameObject[] Skins;
    
    private MemoryManagement _MemoryManagement = new MemoryManagement();
    private DataManagement _DataManagement = new DataManagement();
    private AdManagement _AdManagement = new AdManagement();

    private void Start()
    {
        
        bool sceneController =
            SceneManager.GetActiveScene().buildIndex != _MemoryManagement.ReadData_int("CurrentLevel");
            
        bool creatorLevel = SceneManager.GetActiveScene().buildIndex == 16;
        
        //_MemoryManagement.WriteData_int("CurrentLevel",0);
        
        if (_MemoryManagement.ReadData_int("CurrentLevel") > 15)
        {
            if (!creatorLevel)
            { 
                StartCoroutine(LoadAsync(16));
            }
            else if (creatorLevel)
            {
                currentCharacter = 1;
                _DataManagement.Load();
                _MarketItems = _DataManagement.ListReturner();
                CheckCustomization();
                _AdManagement.RequestInterstitialAd();
                _AdManagement.ShowInterstitialAd();
                _AdManagement.RequestRewardedAd();
            }
        }
        else if (!sceneController)
        {
            currentCharacter = 1;
            _DataManagement.Load();
            _MarketItems = _DataManagement.ListReturner();
            CheckCustomization();
            _AdManagement.RequestInterstitialAd();
            _AdManagement.ShowInterstitialAd();
            _AdManagement.RequestRewardedAd();
        }
        else if (_MemoryManagement.ReadData_int("CurrentLevel") == 0 && sceneController)
        {
            currentCharacter = 1;
            _DataManagement.Load();
            _MarketItems = _DataManagement.ListReturner();
            CheckCustomization();
            _AdManagement.RequestInterstitialAd();
            _AdManagement.ShowInterstitialAd();
            _AdManagement.RequestRewardedAd();
        }
        else if (_MemoryManagement.ReadData_int("CurrentLevel") != 0 && _MemoryManagement.ReadData_int("CurrentLevel") <= 15 && sceneController)
        {
            int index = _MemoryManagement.ReadData_int("CurrentLevel");
            StartCoroutine(LoadAsync(index));
            currentCharacter = 1;
            _DataManagement.Load();
            _MarketItems = _DataManagement.ListReturner();
            CheckCustomization();
            _AdManagement.RequestInterstitialAd();
            _AdManagement.ShowInterstitialAd();
            _AdManagement.RequestRewardedAd();
        }
    }

    private void Update()
    {
        currentCharacterText.text = currentCharacter.ToString();
    }
    public void AI_Instantiate(string processType, int index, Transform transform)
    {
        switch (processType)
        {
            case "Addition":
                MathProcess.Addition(index,AI_Characters,transform,AI_SpawnEffects);
                break;
            case "Extraction":
                MathProcess.Extraction(index,AI_Characters,AI_DeathEffects);
                break;
            case "Multiplication":
                MathProcess.Multiplication(index,AI_Characters,transform,AI_SpawnEffects);
                break;
            case "Division":
                MathProcess.Division(index,AI_Characters,AI_DeathEffects);
                break;
        }
    }
    public void AI_InstantiateDeathEffect(Vector3 transform)
    {
        foreach (var item in AI_DeathEffects)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                item.transform.position = transform;
                item.GetComponent<ParticleSystem>().Play();
                item.GetComponent<AudioSource>().Play();
                currentCharacter--;
                break;
            }
        }
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneIndex);

        _LoadScene.SetActive(true);
        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / .9f);
            _LoadSlider.value = progress;
            yield return null;
        }
    }

    private void CheckCustomization()
    {
        if (_MemoryManagement.ReadData_int("CurrentSkin") != -1)
        {
            foreach (var item in Skins)
            {
                item.SetActive(false);
            }
            Skins[_MemoryManagement.ReadData_int("CurrentSkin") + 1].SetActive(true);
        }
        else
        {
            foreach (var item in Skins)
            {
                item.SetActive(false);
            }
            Skins[0].SetActive(true);
        }
    }
    public void OpenMarket()
    {
        StartCoroutine(LoadAsync(1));
    }

    public void LevelButton(bool isWin)
    {
        if (isWin && _MemoryManagement.ReadData_int("CurrentLevel") == 0)
        {
            int index = _MemoryManagement.ReadData_int("CurrentLevel") + 2;
            _MemoryManagement.WriteData_int("CurrentLevel",index);
            StartCoroutine(LoadAsync(index));
        }
        else if (isWin && _MemoryManagement.ReadData_int("CurrentLevel") != 0 && _MemoryManagement.ReadData_int("CurrentLevel") > 15)
        {
            int index = _MemoryManagement.ReadData_int("CurrentLevel") + 1;
            _MemoryManagement.WriteData_int("CurrentLevel",index);
            StartCoroutine(LoadAsync(16));
        }
        else if (isWin && _MemoryManagement.ReadData_int("CurrentLevel") != 0)
        {
            int index = _MemoryManagement.ReadData_int("CurrentLevel") + 1;
            _MemoryManagement.WriteData_int("CurrentLevel",index);
            StartCoroutine(LoadAsync(index));
        }
        else if (!isWin && _MemoryManagement.ReadData_int("CurrentLevel") > 15)
        {
            StartCoroutine(LoadAsync(16));
        }
        else
            StartCoroutine(LoadAsync(_MemoryManagement.ReadData_int("CurrentLevel")));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
