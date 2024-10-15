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

    [Serializable]
    public class PacmanData
    {
        public int player;
        public int[] Position;
        public int Speed;
        public bool Magnet;
    }

    [Serializable]
    public class GhostData
    {
        public int Player;
        public int Id;
        public int[] Position;
    }

    [Serializable]
    public class GameData
    {
        public bool IsMap;
        public int Round;
        public int Player;
        [CanBeNull] public MapData Map = null;
        public List<List<int>> Actions;
        public int Code;
        public PacmanData Pacman;
        public List<GhostData> Ghost;
    }
}

