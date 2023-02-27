using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HyperCasualLibrary;
using TMPro;
using UnityEngine;

public class GemMovement : MonoBehaviour
{
    private MemoryManagement _MemoryManagement = new MemoryManagement();
    public LevelSettings _LevelSettings;
    public Transform targetPanel;
    public TMP_Text GemStats;
    public TMP_Text GemAmount;
    private Sequence gemAnimation;
    private Sequence gemAnimation2;
    private int gem;
    private void Start()
    {
        gem = _MemoryManagement.ReadData_int("Gem");
        GemStats.text = gem.ToString();
        GemAmount.text = _LevelSettings.LevelGemAmount.ToString();
        gemAnimation = DOTween.Sequence();
        gemAnimation.Append(transform.DOMove(targetPanel.position, 1.1f)
            .SetEase(Ease.InOutElastic)
            .OnComplete(UpdateGem));

    }

    private void UpdateGem()
    {
        GemStats.DOCounter(gem, gem + _LevelSettings.LevelGemAmount, 1f);
        _MemoryManagement.WriteData_int("Gem",gem + _LevelSettings.LevelGemAmount);
        Destroy(gameObject);
    }
}
