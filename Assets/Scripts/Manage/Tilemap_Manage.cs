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

public class Tilemap_Manage : MonoBehaviour
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
        //在Asset/Resources/Tiles 中寻找所需Tile
    }

    //TODO ： 暂定 道具/分数 也使用TileMap形式
    public void load_props(MapData mapdata)  //这个函数会在后端每次传来新的信息时使用，包括初始化
    {
        GameObject tilemapObject = GameObject.Find("props");

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

        foreach (Json.Tile tile in mapdata.TileList)
        {
            if (tile.Type != Enums.TileType.Wall)
            {
                TileBase input_tile = Resources.Load<TileBase>("Sprits/" + tile.TileName);
                tilemap.SetTile(new Vector3Int(tile.x, tile.y, 0), input_tile);
            }
        }
    }

    public void update_props()
    {

    }//预留作为变量更新的接口

    public void clear_map() //可能需要的 清除所有东西以进入下一关
    {
        GameObject gameobject = GameObject.Find("wall");
        tilemap = gameobject.GetComponent<Tilemap>();
        tilemap.ClearAllTiles();
        gameobject = GameObject.Find("props");
        tilemap = gameobject.GetComponent<Tilemap>();
        tilemap.ClearAllTiles();
    }
}
