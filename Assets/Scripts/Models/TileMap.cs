using System.Diagnostics;
using Enums;
using Json;
using UnityEngine.Assertions;


namespace Models
{
    public class Tile
    {
        public TileType Type = TileType.Empty;
    }
    
    public static class TileMap
    {
        public static int Length;
        public static int Width;
        public static Tile[,] Map;

        public static void Init(GameData jsonGameData)
        {
            var tileMapData = jsonGameData.Map;
            Debug.Assert(tileMapData != null, nameof(tileMapData) + " != null");
            
            Length = tileMapData.Length;
            Width = tileMapData.Width;
            Map = new Tile[Length, Width];
            // todo: 看看“length”对应的是横向还是纵向
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Map[i, j] = new Tile
                    {
                        Type = (TileType)tileMapData.Code[i][j],
                    };
                }
            }
        }

        // todo: 这个是全量更新，要是搞明白了增量更新的协议，就需要修改一下
        public static void Update(GameData jsonGameData)
        {
            var tileMapData = jsonGameData.Map;
            Debug.Assert(tileMapData != null, nameof(tileMapData) + " is null");
            
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Map[i, j].Type = (TileType)tileMapData.Code[i][j];
                }
            }
        }
    }
}