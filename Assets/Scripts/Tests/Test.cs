using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Enums;
using Json;
using Models;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

public class Test01 : MonoBehaviour
{
    GameData gameData;
    List<GameData> gamedatas = new List<GameData>();
    public int timer;        //多少步
    void Start()
    {
        timer = 0;
        List<String> jsonDataCollection = new List<string>();
        for (int i = 1; i <= 30; i++)
        {
            jsonDataCollection.Add("Assets/Scripts/Tests/Data/" + i + ".json");
        }

        for (int i = 0; i < jsonDataCollection.Count; i++)
        {
            string jsonData = File.ReadAllText(jsonDataCollection[i]);
            gameData = JsonConvert.DeserializeObject<GameData>(jsonData);
            gamedatas.Add(gameData);
        }

        //Models.TileMap tileMap = new Models.TileMap();
        //tileMap.Init(gamedatas[0]);
        //PrintTiles();

        Pacman.Update(gamedatas[0]);
        var ghost0 = new Ghost(0, new Vector2(0, 0));
        var ghost1 = new Ghost(1, new Vector2(0, 0));
        var ghost2 = new Ghost(2, new Vector2(0, 0));
        var ghost3 = new Ghost(3, new Vector2(0, 0));
        Ghost.Update(gamedatas[0]);
        Debug.Log("ghost count:"+Ghost.AllGhosts.Count);
        PrintPacmanInfo();
        PrintGhostsInfo();
    }

    private void FixedUpdate()
    {
        if (timer >= gamedatas.Count() - 1)
        {
            Debug.Log("End");
            Time.timeScale = 0; //暂停
        }
        Debug.Log("Move");
        Pacman.Update(gamedatas[timer]);
        Ghost.Update(gamedatas[timer]);
        timer++;
    }       //暂且改了fixed timestep 如果不行的话就用Time.frameCount

    private void PrintPacmanInfo()
    {
        string message = new string("Pacman Info:\n");
        message += ($"Player ID: {Pacman.PlayerID}\n");
        message += ($"Current Position: {Pacman.CurrentPosition}\n");
        message += ($"Speed: {Pacman.Speed}\n");
        message += ($"Magnet Active: {Pacman.Magnet}\n");

    }


    private void PrintGhostsInfo()
    {
        string message = new string("Ghosts Info:\n");

        foreach (var ghost in Ghost.AllGhosts)
        {
            message += $"Ghost ID: {ghost.GhostID}\n";
            message += $"Current Position: {ghost.CurrentPosition}\n";
            message += "Route: ";
        }

        Debug.Log(message);
    }

}
