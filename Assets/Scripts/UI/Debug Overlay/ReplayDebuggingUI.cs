using System;
using System.Collections;
using System.Collections.Generic;
using Models;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Debug_Overlay
{
    public class ReplayDebuggingUI : MonoBehaviour
    {
        public ReplayController replayController;
        public GameObject textPrefab; // Reference to the text prefab
        public GameObject buttonPrefab;
        private GameObject text;
        public float spacing = 20f; // Spacing between texts
        public Canvas canvas; // Reference to the Canvas

        void SpawnTexts()
        {
            text = Instantiate(textPrefab, canvas.transform);
            text.GetComponent<Text>().text = $"Play Mode: Replay\nRound: {replayController.nowRound}\n \n";
            text.GetComponent<Text>().text += Pacman.GetInfo();
            text.GetComponent<Text>().text += Ghost.GetInfo();
            RectTransform rectTransform = text.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 1); // Set anchor to top-left
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.pivot = new Vector2(0, 1); // Set pivot to top-left
            rectTransform.anchoredPosition = new Vector2(0, 0);
            
        }

        void SpawnButtons()
        {
            GameObject autoPlay = Instantiate(buttonPrefab, canvas.transform);

            // Get the RectTransform component
            RectTransform rectTransform = autoPlay.GetComponent<RectTransform>();

            // Set the anchor and pivot to bottom-right
            rectTransform.anchorMin = new Vector2(1, 0); // Anchor to bottom-right
            rectTransform.anchorMax = new Vector2(1, 0);
            rectTransform.pivot = new Vector2(1, 0); // Set pivot to bottom-right

            // Calculate the position based on the button index and spacing
            rectTransform.anchoredPosition = new Vector2(-10, rectTransform.rect.height + spacing); // Set x offset and y position

            // Optionally, set the button text
            Text buttonText = autoPlay.GetComponentInChildren<Text>();
            buttonText.text = "autoplay";
            var buttonComponent = autoPlay.GetComponent<Button>();
            buttonComponent.onClick.AddListener(() =>
                replayController.debugAutoUpdate = !replayController.debugAutoUpdate);
            
            GameObject step = Instantiate(buttonPrefab, canvas.transform);

            // Get the RectTransform component
            RectTransform rectTransform2 = step.GetComponent<RectTransform>();

            // Set the anchor and pivot to bottom-right
            rectTransform2.anchorMin = new Vector2(1, 0); // Anchor to bottom-right
            rectTransform2.anchorMax = new Vector2(1, 0);
            rectTransform2.pivot = new Vector2(1, 0); // Set pivot to bottom-right

            // Calculate the position based on the button index and spacing
            rectTransform2.anchoredPosition = new Vector2(-10, rectTransform.rect.height); // Set x offset and y position

            // Optionally, set the button text
            Text buttonText2 = step.GetComponentInChildren<Text>();
            buttonText2.text = "step";
            var buttonComponent2 = step.GetComponent<Button>();
            buttonComponent2.onClick.AddListener(() =>
                ReplayController.stepFrame());
            
        }

        public void UpdateTexts()
        {
            var textComponent = text.GetComponent<Text>();
            textComponent.text = $"Play Mode: Replay\nRound: {replayController.nowRound}\n";
            textComponent.text += Pacman.GetInfo();
            textComponent.text += Ghost.GetInfo();
        }

        private void Start()
        {
            replayController = GetComponent<ReplayController>();
            SpawnTexts();
            SpawnButtons();
            ReplayController.onNewFrameLoaded += UpdateTexts;
        }
    }
}

