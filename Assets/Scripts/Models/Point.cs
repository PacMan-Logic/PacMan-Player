using Json;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Android;
using UnityEngine;

namespace Models
{
    public class Point : MonoBehaviour{ 
        public static Vector2 InitPosition = Vector2.zero;
        public static bool isRedended = false;

        public static void Init(GameData gameData)
        {
            var map = gameData.Map;
            foreach (var tile in map.TileList)
            {
                if (tile.Type == Enums.TileType.PacDot)
                {
                    InitPosition = new Vector2(tile.x + 0.5f, tile.y + 0.5f);
                    isRedended = true;
                    PointMove.generate_point(InitPosition);
                }
                
            }
        }
        public static void Clear()
        {
            GameObject[] Points = GameObject.FindGameObjectsWithTag("Point");
            if (Points != null && Points.Length > 0)
            {
                foreach (var point in Points)
                {
                    Object.Destroy(point);
                }
            }
        }
    }

}

