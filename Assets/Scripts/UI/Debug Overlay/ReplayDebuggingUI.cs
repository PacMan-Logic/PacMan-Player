//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Models;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.UI;

//namespace UI.Debug_Overlay
//{
//    public class ReplayDebuggingUI : MonoBehaviour
//    {
//        public ReplayController replayController;
//        public GameObject textPrefab; // Reference to the text prefab
//        public GameObject buttonPrefab;
//        private GameObject text;
//        public float spacing = 20f; // Spacing between texts
//        public Canvas canvas; // Reference to the Canvas

//        void SpawnTexts()
//        {
//            text = Instantiate(textPrefab, canvas.transform);
//            text.GetComponent<Text>().text = $"Play Mode: Replay\nRound: {replayController.nowRound}\n \n";
//            text.GetComponent<Text>().text += Pacman.GetInfo();
//            text.GetComponent<Text>().text += Ghost.GetInfo();
//            RectTransform rectTransform = text.GetComponent<RectTransform>();
//            rectTransform.anchorMin = new Vector2(0, 1); // Set anchor to top-left
//            rectTransform.anchorMax = new Vector2(0, 1);
//            rectTransform.pivot = new Vector2(0, 1); // Set pivot to top-left
//            rectTransform.anchoredPosition = new Vector2(0, 0);   
//        }

//        void UpdateTexts()
//        {
//            var textComponent = text.GetComponent<Text>();
//            textComponent.text = $"Play Mode: Replay\nRound: {replayController.nowRound}\n";
//            textComponent.text += Pacman.GetInfo();
//            textComponent.text += Ghost.GetInfo();
//        }

//        private void Start()
//        {
//            replayController = GetComponent<ReplayController>();
//            SpawnTexts();
//            ReplayController.onNewFrameLoaded += UpdateTexts;
//        }
//    }
//}

