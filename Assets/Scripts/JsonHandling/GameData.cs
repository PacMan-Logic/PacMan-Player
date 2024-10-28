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
        public List<Tile> TileList;
    }

    //[Serializable]
    //public class PacmanData
    //{
    //    public int player;
    //    public List<List<int>> routes;
    //    public List<int> skills;
    //}

    //[Serializable]
    //public class GhostData
    //{
    //    public int Player;
    //    public int Id;
    //    public List<List<int>> routes;
    //}

    [Serializable]
    public class GameData
    {
        public bool Initalmap;
        public int Round;
        public List<int> Player_id;
        [CanBeNull] public MapData Map = null;
        public int Status_code;
        //public PacmanData Pacman; //注意magnet效果
        public List<List<int>> pacman_step_block;
        public List<List<List<int>>> ghosts_step_block;
        public List<int> skills;
        //public List<GhostData> Ghost;
        public List<int> Score;
    }
}

