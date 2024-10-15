using System;
using System.Collections.Generic;
using System.Diagnostics;
using Enums;
using Json;
using UnityEngine.Assertions;


namespace Models
{
    public static class TileMap
    {
        public static int Length;
        public static int Width;
        public static List<Tile> TileList;

        public static void Init(GameData jsonGameData)
        {
            var tileMapData = jsonGameData.Map;
            Debug.Assert(tileMapData != null, nameof(tileMapData) + " != null");
            
            Length = tileMapData.Length;
            Width = tileMapData.Width;
            TileList = tileMapData.TileList;

        }

        // todo: 这个是全量更新，要是搞明白了增量更新的协议，就需要修改一下
        //public static void Update(GameData jsonGameData)
        //{
        //    var tileMapData = jsonGameData.Map;
        //    Debug.Assert(tileMapData != null, nameof(tileMapData) + " is null");
            
        //    for (int i = 0; i < Length; i++)
        //    {
        //        for (int j = 0; j < Width; j++)
        //        {
        //            Map[i, j].Type = (TileType)tileMapData.Code[i][j];
        //        }
        //    }
        //}
    }
}