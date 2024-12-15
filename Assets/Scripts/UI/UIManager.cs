using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Models;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public ReplayController replayController;
    public GameObject textPrefab; // Reference to the text prefab
    private GameObject text;
    public float spacing = 20f; // Spacing between texts
    public Canvas canvas; // Reference to the Canvas
    public ModeController modeController;
    string mode;

    void SpawnTexts()
    {
        mode = ModeController.IsReplayMode() ? "ReplayMode" : "InteractMode";
        text = Instantiate(textPrefab, canvas.transform);
        if (ModeController.IsInit()) text.GetComponent<Text>().text = "Initing ，，，\n";
        else text.GetComponent<Text>().text = "Play Mode: " + mode + "\n";
        text.GetComponent<Text>().text += $"Level: {Models.Data.level}\n";
        text.GetComponent<Text>().text += $"Round: {Models.Data.nowround}\n";
        text.GetComponent<Text>().text += Pacman.GetInfo();
        text.GetComponent<Text>().text += Ghost.GetInfo();
        RectTransform rectTransform = text.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 1); // Set anchor to top-left
        rectTransform.anchorMax = new Vector2(0, 1);
        rectTransform.pivot = new Vector2(0, 1); // Set pivot to top-left
        rectTransform.anchoredPosition = new Vector2(0, 0);
    }

    void UpdateTexts()
    {
        mode = ModeController.IsReplayMode() ? "ReplayMode" : "InteractMode";
        var textComponent = text.GetComponent<Text>();
        if (ModeController.IsInit()) text.GetComponent<Text>().text = "Initing ，，，\n";
        else text.GetComponent<Text>().text = "Play Mode: " + mode + "\n";
        text.GetComponent<Text>().text += $"Level: {Models.Data.level}\n";
        text.GetComponent<Text>().text += $"Round: {Models.Data.nowround}\n";
        textComponent.text += Pacman.GetInfo();
        textComponent.text += Ghost.GetInfo();
    }

    private void Start()
    {
        replayController = GetComponent<ReplayController>();
        SpawnTexts();
        ReplayController.onNewFrameLoaded += UpdateTexts;
        ReplayController.UpdateUI += UpdateTexts;
        InteractController.UpdateUI += UpdateTexts;
    }
}
