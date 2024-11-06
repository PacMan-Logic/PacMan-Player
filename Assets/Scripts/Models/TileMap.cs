using System;
using System.Collections.Generic;
using System.Diagnostics;
using Enums;
using Json;
using UnityEngine.Assertions;


namespace Models
{
    public class TileMap
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

            Tilemap_Manage tilemap_Manage = new Tilemap_Manage();
            tilemap_Manage.load_wall(tileMapData); //加载包中的信息到地图中   这东西在一次地图中应该只刷新一次
            tilemap_Manage.load_props(tileMapData);
            //建议把道具和任务分开加载
        }
        public static void Update(GameData jsonGameData)
        {
            var tileMapData = jsonGameData.Map;
            Debug.Assert(tileMapData != null, nameof(tileMapData) + " is null");

            Tilemap_Manage tilemap_Manage = new Tilemap_Manage();
            tilemap_Manage.load_props(tileMapData);
        }
        //暂定全量更新，如果要变量的话要修改逻辑
    }
}