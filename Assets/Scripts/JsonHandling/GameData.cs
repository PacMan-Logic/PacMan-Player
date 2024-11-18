using UnityEngine;
using System;
using Enums;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Serialization;

namespace Json
{
    [Serializable]
    public class Tile
    {
        public TileType Type = TileType.Empty;
        public string TileName;
        public int x, y;
    }

    [Serializable]
    public class MapData
    {
        public int Length;
        public int Width;
        public List<Tile> TileList = new List<Tile>();
    }

    [Serializable]
    public class GameData
    {
        public bool Initalmap = false;
        public int Round = 0;
        public int Player_id = 0;
        [CanBeNull] public MapData Map =new MapData();
        public List<List<int>> board = new List<List<int>>();
        public int status = 0;
        public List<List<int>> pacman_step_block = new List<List<int>>();
        public List<List<List<int>>> ghosts_step_block = new List<List<List<int>>> ();
        public List<int> pacman_skills = new List<int>();
        public List<int> score = new List<int> ();
        public List<int> pacman_coord = new List<int> ();
        public List<List<int>> ghosts_coord = new List<List<int>> ();
        [CanBeNull]public string StopReason = null;
        public int level = 0;
    }
}