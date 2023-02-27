using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using HyperCasualLibrary;
using TMPro;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [Header("Generetor Objects")]
    public GameObject[] Grounds;
    public GameObject FinishPoint;

    [Header("Level Objects")] 
    public GameObject NumericPole;
    public GameObject[] Obstacles;

    [Header("Pole Materials")] 
    public Sprite numericBlockBlue;
    public Sprite numericBlockRed;
    
    private int levelUpgrader = 30;
    private int levelMod = 4;
    private Vector3 firstPlatform;
    private List<GameObject> currentGround = new List<GameObject>();
    private List<Transform> obstaclePos = new List<Transform>();
    private MemoryManagement _MemoryManagement = new MemoryManagement();

    [Header("Level Finish GameObjects")] 
    public GameObject WinPanel;
    public GameObject LosePanel;
    private void Start()
    {
        for (int i = 0; i < levelMod; i++)
        {
            GameObject ground = Instantiate(Grounds[Random.Range(0, Grounds.Length)],firstPlatform,Quaternion.Euler(-90f,0,-90));
            firstPlatform = ground.transform.position + new Vector3(0,0,26.38687f);
            currentGround.Add(ground);
            for (int j = 1; j < 4; j++)
            {
                obstaclePos.Add(currentGround[i].transform.GetChild(j));
            }
            PoleSpawn(i);
        }
        ObstacleSpawn();
        Instantiate(FinishPoint, new Vector3(0,0,26.38687f) * levelMod - new Vector3(0,0,13f), Quaternion.Euler(-90f, 0, -90f));
        foreach (var item in currentGround)
        {
            PoleStatusChanger(item.transform.GetChild(0).GetChild(0).gameObject);
        }
    }

    private void PoleSpawn(int i)
    {
        GameObject polePos = currentGround[i].gameObject;
        GameObject go = Instantiate(NumericPole, polePos.transform.GetChild(0).transform);
        
    }

    private void PoleStatusChanger(GameObject go)
    {
        
        GameObject left = go.transform.GetChild(3).gameObject;
        GameObject right = go.transform.GetChild(4).gameObject;

        #region Right

        int ranR = Random.Range(1, 12);
        int ranRTag = Random.Range(0, 4);
        switch (ranRTag)
        {
            case 0:
                right.name = ranR.ToString();
                right.tag = "Addition";
                right.GetComponent<SpriteRenderer>().sprite = numericBlockBlue;
                right.GetComponentInChildren<TMP_Text>().text = (string)("+" + ranR);
                break;
            case 1:
                right.name = ranR.ToString();
                right.tag = "Extraction";
                right.GetComponent<SpriteRenderer>().sprite = numericBlockRed;
                right.GetComponentInChildren<TMP_Text>().text = (string)("-" + ranR);
                break;
            case 2:
                right.name = ranR.ToString();
                right.tag = "Multiplication";
                right.GetComponent<SpriteRenderer>().sprite = numericBlockBlue;
                right.GetComponentInChildren<TMP_Text>().text = (string)("x" + ranR);
                break;
            case 3: 
                right.name = ranR.ToString();
                right.tag = "Division";
                right.GetComponent<SpriteRenderer>().sprite = numericBlockRed;
                right.GetComponentInChildren<TMP_Text>().text = (string)("÷" + ranR);
                
                break;
        }

        #endregion
        #region Left

        int ranL = Random.Range(1, 12);
        int ranLTag = Random.Range(0, 4);
        switch (ranLTag)
        {
            case 0:
                left.name = ranL.ToString();
                left.tag = "Addition";
                left.GetComponent<SpriteRenderer>().sprite = numericBlockBlue;
                left.GetComponentInChildren<TMP_Text>().text = (string)("+" + ranL);
                break;
            case 1:
                left.name = ranL.ToString();
                left.tag = "Extraction";
                left.GetComponent<SpriteRenderer>().sprite = numericBlockRed;
                left.GetComponentInChildren<TMP_Text>().text = (string)("-" + ranL);
                break;
            case 2:
                left.name = ranL.ToString();
                left.tag = "Multiplication";
                left.GetComponent<SpriteRenderer>().sprite = numericBlockBlue;
                left.GetComponentInChildren<TMP_Text>().text = (string)("x" + ranL);
                break;
            case 3: 
                left.name = ranL.ToString();
                left.tag = "Division";
                left.GetComponent<SpriteRenderer>().sprite = numericBlockRed;
                left.GetComponentInChildren<TMP_Text>().text = (string)("÷" + ranL);
                
                break;
        }

        #endregion
    }

    private void ObstacleSpawn()
    {
        foreach (var item in obstaclePos)
        {
            GameObject go = Instantiate(Obstacles[Random.Range(0, Obstacles.Length)],item.position, Quaternion.identity);
            go.transform.parent = item.transform;
        }
        
    }
}
