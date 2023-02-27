using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GoogleMobileAds;
using GoogleMobileAds.Api;

namespace HyperCasualLibrary
{
    public class MathProcess
    {
        public static void Addition(int index, List<GameObject> AI_Characters, Transform transform,List<GameObject> SpawnEffects)
        {
            int number = 0;
            foreach (var item in AI_Characters)
            {
                if (number < index)
                {
                    if (!item.activeInHierarchy)
                    {
                        foreach (var item2 in SpawnEffects)
                        {
                            if (!item.activeInHierarchy)
                            {
                                item2.SetActive(true);
                                item2.transform.position = transform.position;
                                item2.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();
                            }
                        }
                        item.transform.position = transform.position;
                        item.SetActive(true);
                        number++;
                    }
                }
                else
                {
                    number = 0;
                    break;
                }
            }
            GameManager.currentCharacter += index;
        }
        public static void Extraction(int index, List<GameObject> AI_Characters,List<GameObject> DeathEffects)
        {
            if (GameManager.currentCharacter <= index)
            {
                foreach (var item in AI_Characters)
                {
                    foreach (var item2 in DeathEffects)
                    {
                        item2.SetActive(true);
                        item2.transform.position = item.transform.position;
                        item2.GetComponent<ParticleSystem>().Play();
                        item2.GetComponent<AudioSource>().Play();
                        break;
                    }
                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }
                GameManager.currentCharacter = 1;
            }
            else
            {
                int number = 0;
                foreach (var item in AI_Characters)
                {
                    if (number != index)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var item2 in DeathEffects)
                            {
                                item2.SetActive(true);
                                item2.transform.position = item.transform.position;
                                item2.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            number++;
                        }
                    }
                    else
                    {
                        number = 0;
                        break;
                    }
                }
                GameManager.currentCharacter -= index;
            }
        }
        public static void Multiplication(int index, List<GameObject> AI_Characters, Transform transform,List<GameObject> SpawnEffects)
        {
            int loopNumber = (GameManager.currentCharacter * index) - GameManager.currentCharacter;
            int number = 0;
            foreach (var item in AI_Characters)
            {
                if (number < loopNumber)
                {
                    if (!item.activeInHierarchy)
                    {
                        foreach (var item2 in SpawnEffects)
                        {
                            if (!item2.activeInHierarchy)
                            {
                                item2.SetActive(true);
                                item2.transform.position = item.transform.position;
                                item2.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();
                            }
                        }
                        item.transform.position = transform.position;
                        item.SetActive(true);
                        number++;
                    }
                }
                else
                {
                    number = 0;
                    break;
                }
            }
            GameManager.currentCharacter *= index;
        }
        public static void Division(int index, List<GameObject> AI_Characters,List<GameObject> DeathEffects)
        {
            if (GameManager.currentCharacter <= index)
            {
                foreach (var item in AI_Characters)
                {
                    foreach (var item2 in DeathEffects)
                    {
                        item2.SetActive(true);
                        item2.transform.position = item.transform.position;
                        item2.GetComponent<ParticleSystem>().Play();
                        item2.GetComponent<AudioSource>().Play();
                        break;
                    }
                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }
                GameManager.currentCharacter = 1;
            }
            else
            {
                int dividing = GameManager.currentCharacter / index;
                int number = 0;
                foreach (var item in AI_Characters)
                {
                    if (number != dividing)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var item2 in DeathEffects)
                            {
                                item2.SetActive(true);
                                item2.transform.position = item.transform.position;
                                item2.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            number++;
                        }
                    }
                    else
                    {
                        number = 0;
                        break;
                    }
                }

                if (GameManager.currentCharacter % index == 0)
                {
                    GameManager.currentCharacter /= index;
                }
                else if (GameManager.currentCharacter % index == 1)
                {
                    GameManager.currentCharacter /= index;
                    GameManager.currentCharacter++;
                }
                else if (GameManager.currentCharacter % index == 2)
                {
                    GameManager.currentCharacter /= index;
                    GameManager.currentCharacter += 2;
                }
                else if (GameManager.currentCharacter % index == 3)
                {
                    GameManager.currentCharacter /= index;
                    GameManager.currentCharacter += 3;
                }
            }
        }
    }

    public class MemoryManagement
    {
        public string ReadData_string(string key)
        {
            return PlayerPrefs.GetString(key);
        }
        public int ReadData_int(string key)
        {
            return PlayerPrefs.GetInt(key);
        }
        public float ReadData_float(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }
        
        public void WriteData_string(string key,string value)
        {
            PlayerPrefs.SetString(key,value);
            PlayerPrefs.Save();
        }
        public void WriteData_int(string key,int value)
        {
            PlayerPrefs.SetInt(key,value);
            PlayerPrefs.Save();
        }
        public void WriteData_float(string key,float value)
        {
            PlayerPrefs.SetFloat(key,value);
            PlayerPrefs.Save();
        }
        public void FirstTimeSetup()
        {
            WriteData_int("CurrentSkin",-1);
            WriteData_int("CurrentCloak",-1);
            WriteData_int("CurrentMonster",-1);
            WriteData_int("CurrentLevel",0);
            WriteData_int("Gem",0);
            WriteData_int("GameMusic",1);
            WriteData_int("GameFx",1);
        }
    }

    public class AdManagement
    {
        private InterstitialAd interstitial;
        private RewardedAd rewardedAd;

        #region InterstitialAd

        public void RequestInterstitialAd()
        {
            string AdUnitId;
#if UNITY_ANDROID
            AdUnitId = "";
#elif UNITY_IOS
            AdUnitId = "";
#endif
            interstitial = new InterstitialAd(AdUnitId);

            AdRequest request = new AdRequest.Builder().Build();
            interstitial.LoadAd(request);
            interstitial.OnAdClosed += InterstitialAdClosed;
        }
        
        void InterstitialAdClosed(object sender, EventArgs args)
        {
            interstitial.Destroy();
            RequestInterstitialAd();
        }

        public void ShowInterstitialAd()
        {
            if (interstitial.IsLoaded())
            {
                interstitial.Show();
            }
            else
            {
                interstitial.Destroy();
                RequestInterstitialAd();
            }
        }

        #endregion
        
        #region RewardedAd

        public void RequestRewardedAd()
        {
            string AdUnitId;
#if UNITY_ANDROID
            AdUnitId = "";
#endif
            rewardedAd = new RewardedAd(AdUnitId);
            
            AdRequest request = new AdRequest.Builder().Build();
            rewardedAd.LoadAd(request);
            rewardedAd.OnAdLoaded += RewardAdLoaded;
            rewardedAd.OnAdClosed += RewardAdClosed;
            rewardedAd.OnUserEarnedReward += RewardAdEarned;
        }

        void RewardAdLoaded(object sender, EventArgs args)
        {
        }

        void RewardAdClosed(object sender, EventArgs args)
        {
            RequestRewardedAd();
        }

        void RewardAdEarned(object sender, Reward reward)
        {
            string type = reward.Type;
            double amount = reward.Amount;
            PlayerPrefs.SetInt("Gem",PlayerPrefs.GetInt("Gem") + (int)amount);
        }

        public void ShowRewardAd()
        {
            if (rewardedAd.IsLoaded())
            {
                rewardedAd.Show();
            }
        }

        #endregion
    }
    public class DataManagement
    {
        public void Save(List<MarketItems> _MarketItems,bool isFirstTime = false)
        {
            if (!isFirstTime)
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemData.gd");
                bf.Serialize(file, _MarketItems);
                file.Close();
            }
            else
            {
                if (!File.Exists(Application.persistentDataPath + "/ItemData.gd"))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.Create(Application.persistentDataPath + "/ItemData.gd");
                    bf.Serialize(file,_MarketItems);
                    file.Close();
                }
            }
        }

        private List<MarketItems> _MarketList;
        public void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/ItemData.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/ItemData.gd",FileMode.Open);
                _MarketList = (List<MarketItems>)bf.Deserialize(file);
                file.Close();
            }
        }

        public List<MarketItems> ListReturner()
        {
            return _MarketList;
        }
    }

    [Serializable]
    public class MarketItems
    {
        public int ItemIndex;
        public string ItemName;
        public int ItemPrice;
        public bool isSold;
    }
    
}

