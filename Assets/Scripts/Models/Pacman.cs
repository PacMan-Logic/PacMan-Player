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
        public static List<MovementType> Route = new List<MovementType>();
        public static int Speed = 1;
        public static bool Magnet = false;

        public static void Init (int playerID, Vector2 initialPosition)
        {
            CurrentPosition = initialPosition;
            PlayerID = playerID;
        }

        public static void Update(GameData jsonGameData)
        {
            PlayerID = jsonGameData.Pacman.player;
            CurrentPosition = new Vector2(jsonGameData.Pacman.Position[0], jsonGameData.Pacman.Position[1]);
            Route = jsonGameData.Actions[0].ConvertAll(code => (MovementType)code);
            Speed = jsonGameData.Pacman.Speed;
            Magnet = jsonGameData.Pacman.Magnet;
        }
    }
}