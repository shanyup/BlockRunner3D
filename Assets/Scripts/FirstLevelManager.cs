using System.Collections.Generic;
using System.IO;
using UnityEngine;
using HyperCasualLibrary;

public class FirstLevelManager : MonoBehaviour
{
    public List<MarketItems> _MarketItems;
    private MemoryManagement _MemoryManagement = new MemoryManagement();
    private DataManagement _DataManagement = new DataManagement();
    private void Awake()
    {
        if (!File.Exists(Application.persistentDataPath + "/ItemData.gd"))
        {
            _DataManagement.Save(_MarketItems,true);
            _MemoryManagement.FirstTimeSetup();
        }
    }
}
