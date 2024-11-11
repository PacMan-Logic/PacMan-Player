using Json;
using Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using JetBrains.Annotations;
using static UnityEditor.Progress;
using Unity.VisualScripting;

public class Tilemap_Manage
{
    private Tilemap tilemap;
    public void load_wall(MapData mapdata) {
        GameObject tilemapObject = GameObject.Find("wall");

        if (tilemapObject == null)
        {
            Debug.LogError("tilemapobject is no exist");
            return;
        }

        tilemap = tilemapObject.GetComponent<Tilemap>();

        if (mapdata.TileList == null)
        {
            Debug.Log("Tilemap is Empty.");
            return;
        }

        foreach(Json.Tile tile in mapdata.TileList)
        {
            if (tile.Type == Enums.TileType.Wall)
            {
                TileBase input_tile = Resources.Load<TileBase>("Tiles/" + tile.TileName);
                tilemap.SetTile(new Vector3Int(tile.x, tile.y, 0), input_tile);
            }
        }
    }

    public void clear_map()
    {
        GameObject gameobject = GameObject.Find("wall");
        tilemap = gameobject.GetComponent<Tilemap>();
        tilemap.ClearAllTiles();
    }

    public static MapData convert(List<List<int>> board)
    {
        MapData mapdata = new MapData();
        mapdata.Length = mapdata.Width = board.Count;
        int tile_num = 0;
        for (int i = 0; i < board.Count ; i++)
        {
            for(int j = 0; j < board.Count; j++)
            {
                Json.Tile tile = new Json.Tile();
                tile_num = board[i][j];
                    // Debug.Log("Changed" + j + " " + i);
                switch(tile_num)
                {
                    case 0:
                        {
                            tile.Type = Enums.TileType.Wall;
                            tile.TileName = "Wall_00";
                            tile.x = j; tile.y = i;
                            mapdata.TileList.Add(tile);
                            break;
                        }
                    case 2:
                        {
                            tile.Type = Enums.TileType.PacDot;
                            tile.TileName = "PacDot";
                            tile.x = j;tile.y = i;
                            mapdata.TileList.Add(tile); 
                            break;
                        }
                    case 3:
                        {
                            tile.Type = Enums.TileType.Bonus;
                            tile.TileName = "Bouns";
                            tile.x = j;tile.y = i;
                            mapdata.TileList.Add(tile);
                            break;
                        }
                    case 4:
                        {
                            tile.Type = Enums.TileType.Acceleration;
                            tile.TileName = "Acceleration";
                            tile.x = j;tile.y= i;
                            mapdata.TileList.Add(tile);
                            break;
                        }
                    case 5:
                        {
                            tile.Type = Enums.TileType.Magnet;
                            tile.TileName = "Magnet";
                            tile.x = j;tile.y= i;
                            mapdata.TileList.Add(tile);
                            break;
                        }
                    case 6:
                        {
                            tile.Type = Enums.TileType.Shield;
                            tile.TileName = "Shield";
                            tile.x = j;tile.y = i;
                            mapdata.TileList.Add(tile);
                            break;
                        }
                    case 7:
                        {
                            tile.Type = Enums.TileType.Double;
                            tile.TileName = "Double";
                            tile.x = j;tile.y = i;
                            mapdata.TileList.Add(tile);
                            break;
                        }
                }
            }
        }
        return mapdata;
    }
}
