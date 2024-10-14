using System;
using System.Collections.Generic;
using Enums;
using Json;
using UnityEngine;
using UnityEngine.Assertions;

namespace Models
{
    public class Ghost
    {
        public static List<Ghost> AllGhosts = new List<Ghost>();

        public Vector2 CurrentPosition;
        public int GhostID;
        public List<MovementType> Route = new List<MovementType>();

        public Ghost(int ghostID, Vector2 initialPosition)
        {
            CurrentPosition = initialPosition;
            GhostID = ghostID;
            AllGhosts.Add(this);
        }

        public static void Update(GameData jsonGameData)
        {
            Debug.Assert(AllGhosts.Count == 4, "ghosts update failed: not enough ghosts instantiated");
            if (jsonGameData.round == 1)
            {
                AllGhosts.Sort((g1, g2) => g1.GhostID.CompareTo(g2.GhostID));
            }

            var ghostsData = jsonGameData.ghosts;
            foreach (GhostData ghostData in ghostsData)
            {
                try
                {
                    AllGhosts[ghostData.id].Route = ghostData.route.ConvertAll(code => (MovementType)code);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Make sure ghost id starts with 0.");
                    throw;
                }
                
            }
        }
    }
}