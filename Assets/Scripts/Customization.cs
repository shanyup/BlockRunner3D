using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using HyperCasualLibrary;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Customization : MonoBehaviour
{
    public List<MarketItems> _MarketItems = new List<MarketItems>();

    private MemoryManagement _MemoryManagement = new MemoryManagement();
    private DataManagement _DataManagement = new DataManagement();
    private AdManagement _AdManagement = new AdManagement();

    [Header("General Settings")]
    public TMP_Text _GemText;

    [Header("Skin Panel")] 
    public GameObject SkinStandart;
    public List<GameObject> SkinList;
    public GameObject[] SkinBadges;
    public TMP_Text _SkinBuyText;
    public Button _SkinBuyButton;
    private int currentIndex;
    
    private void Start()
    {
        _DataManagement.Load();
        _MarketItems = _DataManagement.ListReturner();
        _GemText.text = _MemoryManagement.ReadData_int("Gem").ToString();
        _AdManagement.RequestRewardedAd();
        if (_MemoryManagement.ReadData_int("CurrentSkin") != -1)
        {
            SkinStandart.SetActive(false);
            SkinList[_MemoryManagement.ReadData_int("CurrentSkin")].SetActive(true);
            SkinBadges[_MemoryManagement.ReadData_int("CurrentSkin")].SetActive(true);
        }
    }
    public void SelectItem(int index)
    {
        currentIndex = index;
        SkinStandart.SetActive(false);
        foreach (var item in SkinList)
        {
            item.SetActive(false);
        }
        SkinList[currentIndex].SetActive(true);

        if (!_MarketItems[currentIndex].isSold)
        {
            if (_MemoryManagement.ReadData_int("Gem") >= _MarketItems[currentIndex].ItemPrice)
            {
                _SkinBuyButton.gameObject.SetActive(true);
                _SkinBuyText.text = _MarketItems[currentIndex].ItemPrice.ToString();
                _SkinBuyButton.interactable = true;
            }
            else if (_MemoryManagement.ReadData_int("Gem") < _MarketItems[currentIndex].ItemPrice)
            {
                _SkinBuyButton.gameObject.SetActive(true);
                _SkinBuyText.text = _MarketItems[currentIndex].ItemPrice.ToString();
                _SkinBuyButton.interactable = false;
            }
        }
        else
        {
            Equip();
        }
    }

    public void Equip()
    {
        if (_MarketItems[currentIndex].isSold && _MemoryManagement.ReadData_int("CurrentSkin") != currentIndex)
        {
            SkinStandart.SetActive(false);
            _MemoryManagement.WriteData_int("CurrentSkin",currentIndex);
            foreach (var item in SkinBadges)
            {
                item.SetActive(false);
            }
            SkinBadges[currentIndex].SetActive(true);
        }
        else if(SkinBadges[currentIndex])
        {
            _MemoryManagement.WriteData_int("CurrentSkin",-1);
            foreach (var item in SkinBadges)
            {
                item.SetActive(false);
            }
            SkinList[currentIndex].SetActive(false);
            SkinStandart.SetActive(true);
        }
    }
    public void Buy()
    {
        if (_MemoryManagement.ReadData_int("Gem") >= _MarketItems[currentIndex].ItemPrice && !_MarketItems[currentIndex].isSold)
        {
            int newGem = _MemoryManagement.ReadData_int("Gem") - _MarketItems[currentIndex].ItemPrice;
            _MemoryManagement.WriteData_int("Gem",newGem);
            _MarketItems[currentIndex].isSold = true;
            _GemText.text = _MemoryManagement.ReadData_int("Gem").ToString();
            _SkinBuyButton.gameObject.SetActive(false);
            Equip();
        }
    }
    public void ButtonBack()
    {
        _DataManagement.Save(_MarketItems);
        if(_MemoryManagement.ReadData_int("CurrentLevel") == 0)
            SceneManager.LoadScene(0);
        else if (_MemoryManagement.ReadData_int("CurrentLevel") > 15)
        {
            SceneManager.LoadScene(16);
        }
        else
        {
            int index = _MemoryManagement.ReadData_int("CurrentLevel");
            SceneManager.LoadScene(index);
        }
    }

    public void AddGem()
    {
        _AdManagement.ShowRewardAd();
        StartCoroutine(ScreenTextUpdate());
        _AdManagement.RequestRewardedAd();
    }

    IEnumerator ScreenTextUpdate()
    {
        int currentGem = _MemoryManagement.ReadData_int("Gem");
        yield return new WaitForSeconds(35);
        _GemText.DOCounter(currentGem, _MemoryManagement.ReadData_int("Gem"), 1f);
    }
}
