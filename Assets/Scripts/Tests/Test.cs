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
        TileMap.Init(gameData);
        PrintTiles();
    }

    void Update()
    {
        
    }

    private void PrintTiles()
    {
        Debug.Log("printing tiles");
        var tileMap = TileMap.Map;
        string message = new String("");
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                var tile = tileMap[i, j];
                string msg = new string("");
                switch (tile.Type)
                {
                    case TileType.Wall:
                        msg = "墙";
                        break;
                    case TileType.Empty:
                        msg = " ";
                        break;
                    case TileType.PacDot:
                        msg = "O";
                        break;
                    case TileType.Acceleration:
                        msg = "速";
                        break;
                    case TileType.Energizer:
                        msg = "大";
                        break;
                    case TileType.Magnet:
                        msg = "磁";
                        break;
                }

                message += msg;
                message += "\t";
            }

            message += "\n";
        }

        Debug.Log(message);
    }
}
