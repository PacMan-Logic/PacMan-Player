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
        public List<List<int>> routes;
        public static event Action OnUpdated; 

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

            int index = 0;
            foreach (var route in jsonGameData.ghosts_step_block)
            {
                try
                {
                    AllGhosts[index].CurrentPosition = new Vector2(route[0][0], route[0][1]);
                    AllGhosts[index].routes = route;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Make sure ghost id starts with 0.");
                    throw;
                }
                index++;
            }
            OnUpdated?.Invoke();
        }

        public static void ClearRoute()
        {
            
            foreach (Ghost ghost in AllGhosts)
            {
                try
                {
                    ghost.routes.Clear();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Make sure ghost id starts with 0.");
                    throw;
                }
                
            }
            OnUpdated?.Invoke();
        }
    }
}