using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class manager : MonoBehaviour
{
    [SerializeField]
    public List<TileBase> tiles = new List<TileBase>();

    [ContextMenu("Load")]
    public void Load()
    {
        if (tiles.Count == 0)
        {
            Debug.Log("?");
            return;
        }
        else
        {
            Debug.Log(tiles.Count);
        }
    }
}
