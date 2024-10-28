using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Enums;
using Json;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

namespace Models
{
    public static class Pacman
    {
        public static Vector2 CurrentPosition;
        public static int PlayerID;
        public static List<List<int>> Routes;
        public static int Speed = 1;
        public static bool Magnet = false;
        public static event Action OnUpdated; 

        public static void Init (int playerID, Vector2 initialPosition)
        {
            CurrentPosition = initialPosition;
            PlayerID = playerID;
        }

        public static void Update(GameData jsonGameData)
        {
            PlayerID = jsonGameData.Player_id[0];
            CurrentPosition = new Vector2(jsonGameData.pacman_step_block[0][0], jsonGameData.pacman_step_block[0][1]);
            Routes = jsonGameData.pacman_step_block;
            Speed = jsonGameData.pacman_step_block.ToArray().Length - 1;
            Magnet = jsonGameData.skills[2] > 0;
            OnUpdated?.Invoke();
        }
        public static void ClearRoute(){
            Routes.Clear();
        }
    }
}