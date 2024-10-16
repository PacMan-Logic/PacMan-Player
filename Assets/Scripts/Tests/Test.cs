using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Enums;
using Json;
using Models;
using UnityEngine;
using Newtonsoft.Json;

public class Test01 : MonoBehaviour
{
    void Start()
    {
        List<String> jsonDataCollection = new List<string>
        {
            "Assets/Scripts/Tests/Data/1.json"
        };
        
        string jsonData = File.ReadAllText(jsonDataCollection[0]);
        GameData gameData = JsonConvert.DeserializeObject<GameData>(jsonData);
        Models.TileMap tileMap = new Models.TileMap();
        tileMap.Init(gameData);
        //PrintTiles();
        
        Pacman.Update(gameData);
        var ghost0 = new Ghost(0, new Vector2(0, 0));
        var ghost1 = new Ghost(1, new Vector2(0, 0));
        var ghost2 = new Ghost(2, new Vector2(0, 0));
        var ghost3 = new Ghost(3, new Vector2(0, 0));
        Ghost.Update(gameData);
        Debug.Log("ghost count:"+Ghost.AllGhosts.Count);
        PrintPacmanInfo();
        PrintGhostsInfo();
    }

    void Update()
    {
        
    }

    //private void PrintTiles()
    //{
    //    Debug.Log("printing tiles");
    //    var tileMap = TileMap.TileList;
    //    string message = new String("");
    //    for (int i = 0; i < 10; i++)
    //    {
    //        for (int j = 0; j < 10; j++)
    //        {
    //            var tile = tileMap[i, j];
    //            string msg = new string("");
    //            switch (tile.Type)
    //            {
    //                case TileType.Wall:
    //                    msg = "墙";
    //                    break;
    //                case TileType.Empty:
    //                    msg = " ";
    //                    break;
    //                case TileType.PacDot:
    //                    msg = "O";
    //                    break;
    //                case TileType.Acceleration:
    //                    msg = "速";
    //                    break;
    //                case TileType.Energizer:
    //                    msg = "大";
    //                    break;
    //                case TileType.Magnet:
    //                    msg = "磁";
    //                    break;
    //            }

    //            message += msg;
    //            message += "\t";
    //        }

    //        message += "\n \n \n";
    //    }

    //    Debug.Log(message);
    //}

    private void PrintPacmanInfo()
    {
        string message = new string("Pacman Info:\n");
        message += ($"Player ID: {Pacman.PlayerID}\n");
        message += ($"Current Position: {Pacman.CurrentPosition}\n");
        message += ($"Speed: {Pacman.Speed}\n");
        message += ($"Magnet Active: {Pacman.Magnet}\n");

        // Print out the route (movement types)
        message += ("Route: ");
        foreach (var movement in Pacman.Route)
        {
            message += (movement);
            message += " ";
        }
        Debug.Log(message);
    }


    private void PrintGhostsInfo()
    {
        string message = new string("Ghosts Info:\n");

        foreach (var ghost in Ghost.AllGhosts)
        {
            message += $"Ghost ID: {ghost.GhostID}\n";
            message += $"Current Position: {ghost.CurrentPosition}\n";
            message += "Route: ";

            foreach (var movement in ghost.Route)
            {
                message += movement + " ";
            }

            message += "\n";
        }

        Debug.Log(message);
    }

}
