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
    public void load(MapData mapdata) {
        GameObject tilemapObject = GameObject.Find("tilemap");

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
        //保留在Inspector中手动添加的方式，如需要，再改为Asset中寻找
    }
}
