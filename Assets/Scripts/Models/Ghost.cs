using System;
using System.Collections.Generic;
using Enums;
using Json;
using UnityEngine;
using Constants;
using System.Linq;
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
            if(AllGhosts.Count < Constants.Constants.GhostNumber)
                AllGhosts.Add(this);
        }

        public static void Update(GameData jsonGameData)
        {
            Debug.Assert(AllGhosts.Count == Constants.Constants.GhostNumber, "ghosts update failed: not enough ghosts instantiated");
            if (jsonGameData.Round == 1)
            {
                AllGhosts.Sort((g1, g2) => g1.GhostID.CompareTo(g2.GhostID));
            }

            var ghostsData = jsonGameData.Ghost;
            foreach (GhostData ghostData in ghostsData)
            {
                try
                {
                    AllGhosts[ghostData.Id].CurrentPosition = new Vector2(ghostData.Position[0], ghostData.Position[1]);
                    AllGhosts[ghostData.Id].Route = jsonGameData.Actions[ghostData.Id + 1].ConvertAll(code => (MovementType)code);
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