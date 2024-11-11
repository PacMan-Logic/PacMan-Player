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
        public static bool Magnet = false;
        public static event Action OnUpdated;

        public static void Init (GameData jsonGameData)
        {
            CurrentPosition = new Vector2(jsonGameData.pacman_coord[0], jsonGameData.pacman_coord[1]);
            PlayerID = jsonGameData.Player_id;
        }

        public static void Update(GameData jsonGameData)
        {
            PlayerID = jsonGameData.Player_id;
            Route = jsonGameData.pacman_step_block;
            Speed = jsonGameData.pacman_step_block.Count - 1;
            if (jsonGameData.skills != null && jsonGameData.skills.Count != 0)
            {
                Magnet = jsonGameData.skills[2] > 0;
            }
            if(jsonGameData.pacman_step_block.Count == 0) {
                CurrentPosition = new Vector2(jsonGameData.pacman_coord[1], jsonGameData.pacman_coord[0]);
            }
            else
            {
                CurrentPosition = new Vector2(jsonGameData.pacman_step_block[0][1], jsonGameData.pacman_step_block[0][0]);
            }
            OnUpdated?.Invoke();
        }
        public static void ClearRoute(){
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
            message += ($"\tSpeed: {Pacman.Speed}\n");
            message += ($"\tMagnet Active: {Pacman.Magnet}\n");
            return message;
        }
    }
}