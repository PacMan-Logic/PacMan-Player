using Json;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Models
{
    public class Point : MonoBehaviour{ 
        public static Vector2 InitPosition = Vector2.zero;
        public static bool IsRendered = false;

        public static void Init(GameData gameData)
        {
            clear_props();
            var map = gameData.Map;
            Debug.Log(map);
            foreach (var tile in map.TileList)
            {
                if (tile.Type == Enums.TileType.PacDot)
                {
                    InitPosition = new Vector2(tile.x + 0.5f, tile.y + 0.5f);
                    IsRendered = true;
                    PointMove.generate_point(InitPosition);
                }
                else
                {
                    InitPosition = new Vector2(tile.x + 0.5f, tile.y + 0.5f);
                    IsRendered = true;
                    PointMove.generate_props(InitPosition, tile.Type);
                }
                
            }
        }
        public static void clear_props()
        {
            List<GameObject> Points = new List<GameObject>();
            Points.AddRange(GameObject.FindGameObjectsWithTag("Point"));
            Points.AddRange(GameObject.FindGameObjectsWithTag("Shield"));
            Points.AddRange(GameObject.FindGameObjectsWithTag("Double"));
            Points.AddRange(GameObject.FindGameObjectsWithTag("Bonus"));
            Points.AddRange(GameObject.FindGameObjectsWithTag("Acceleration"));
            Points.AddRange(GameObject.FindGameObjectsWithTag("Magnet"));

            if (Points != null && Points.Count > 0)
            {
                foreach (var point in Points)
                {
                    Object.Destroy(point);
                }
            }
        }
    }

}

