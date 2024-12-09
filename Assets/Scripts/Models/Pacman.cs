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
        public static List<List<int>> Route;
        public static int Speed = 1;
        public static int Magnet = 0;
        public static int Acc = 0;
        public static int Bonus = 0;
        public static int Shield = 0;
        public static event Action OnUpdated;
        public static bool eaten = false;
        public static int current_level = 1;

        public static void Init (GameData jsonGameData)
        {
            CurrentPosition = new Vector2(jsonGameData.pacman_coord[0], jsonGameData.pacman_coord[1]);
            PlayerID = jsonGameData.Player_id;
        }

        public static void Update(GameData jsonGameData)
        {
            PlayerID = jsonGameData.Player_id;
            Route = jsonGameData.pacman_step_block;
            current_level = jsonGameData.level;
            if (jsonGameData.pacman_skills != null && jsonGameData.pacman_skills.Count != 0)
            {
                Bonus = jsonGameData.pacman_skills[0];
                Acc = jsonGameData.pacman_skills[1];
                Magnet = jsonGameData.pacman_skills[2];
                Shield = jsonGameData.pacman_skills[3];
            }
            if(jsonGameData.pacman_step_block.Count == 0) {
                CurrentPosition = new Vector2(jsonGameData.pacman_coord[1], jsonGameData.pacman_coord[0]);
            }
            else
            {
                Speed = jsonGameData.pacman_step_block.Count - 1;
                CurrentPosition = new Vector2(jsonGameData.pacman_step_block[0][1], jsonGameData.pacman_step_block[0][0]);
            }
            OnUpdated?.Invoke();
        }
        public static void ClearRoute()
        {
            Route.Clear();
        }

        public static string GetInfo()
        {
            string message = "";
            message += ($"Pacman Info: Player ID: {Pacman.PlayerID}\n");
            message += ($"\tCurrent Position: {Pacman.CurrentPosition}\n");
            message += $"\tRoute: ";
            if(Pacman.Route != null)
            {
                for (int i = 0; i < Pacman.Route.Count; i++)
                {
                    var position = Pacman.Route[i];
                    message += $"({position[0]}, {position[1]})";
                    if (i < Pacman.Route.Count - 1)
                        message += $" -> ";
                }
            }
            else
            {
                message += "NULL";
            }
            message += $"\n";
            return message;
        }
    }
}