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
        //��Asset/Resources/Tiles ��Ѱ������Tile
    }

    //TODO �� �ݶ� ����/���� Ҳʹ��TileMap��ʽ
    public void load_props(MapData mapdata)  //����������ں��ÿ�δ����µ���Ϣʱʹ�ã�������ʼ��
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

    }//Ԥ����Ϊ�������µĽӿ�

    public void clear_map() //������Ҫ�� ������ж����Խ�����һ��
    {
        GameObject gameobject = GameObject.Find("wall");
        tilemap = gameobject.GetComponent<Tilemap>();
        tilemap.ClearAllTiles();
        gameobject = GameObject.Find("props");
        tilemap = gameobject.GetComponent<Tilemap>();
        tilemap.ClearAllTiles();
    }
}
