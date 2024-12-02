using System;
using System.Collections.Generic;
using Enums;
using Json;
using UnityEngine;
using Constants;
using System.Linq;
using System.Reflection;

namespace Models
{
    public class Ghost
    {
        public static List<Ghost> AllGhosts = new List<Ghost>();

        public Vector2 CurrentPosition;
        public int GhostID;
        public List<List<int>> Route;
        public int Speed = 1;
        public static event Action OnUpdated; 

        public Ghost(int ghostID, Vector2 initialPosition)
        {
            CurrentPosition = initialPosition;
            GhostID = ghostID;
            if(AllGhosts.Count < Constants.Constants.GhostNumber)
                AllGhosts.Add(this);
        }

        public static void Init (GameData jsonGameData)
        {
            for (int i = 0; i < AllGhosts.Count; i++)
            {
                var ghost = AllGhosts[i];
                ghost.CurrentPosition = new Vector2(jsonGameData.ghosts_coord[i][1], jsonGameData.ghosts_coord[i][0]);
                ghost.GhostID = i;
            }
        }
        
        public static void Update(GameData jsonGameData)
        {
            Debug.Assert(AllGhosts.Count == Constants.Constants.GhostNumber, "ghosts update failed: not enough ghosts instantiated");
            if (jsonGameData.Round == 1)
            {
                AllGhosts.Sort((g1, g2) => g1.GhostID.CompareTo(g2.GhostID));
            }

            
            for(int i = 0; i < AllGhosts.Count; i++)
            {
                if (jsonGameData.ghosts_step_block.Count == 0)
                {
                    AllGhosts[i].CurrentPosition = new Vector2(jsonGameData.ghosts_coord[i][1], jsonGameData.ghosts_coord[i][0]);
                    AllGhosts[i].Route = null;
                }
                else
                {
                    AllGhosts[i].Route = jsonGameData.ghosts_step_block[i];
                    AllGhosts[i].Speed = jsonGameData.ghosts_step_block[i].Count - 1;
                    AllGhosts[i].CurrentPosition = new Vector2(jsonGameData.ghosts_step_block[i][0][1], jsonGameData.ghosts_step_block[i][0][0]);
                }
            }
            OnUpdated?.Invoke();
        }

        public static string GetInfo()
        {
            string message = "";
            foreach (var ghost in Ghost.AllGhosts)
            {
                message += $"Ghost ID: {ghost.GhostID}\n";
                message += $"\tCurrent Position: {ghost.CurrentPosition}\n";
                message += $"\tRoute: ";
                if(ghost.Route != null)
                {
                    for (int i = 0; i < ghost.Route.Count; i++)
                    {
                        var position = ghost.Route[i];
                        message += $"({position[0]}, {position[1]})";
                        if (i < ghost.Route.Count - 1)
                            message += $" -> ";
                    }
                }
                else
                {
                    message += "NULL";
                }
                message += $"\n";
            }

            return message;
        }
    }
}