using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class PointMove : MonoBehaviour
{
    public float detectionInterval = 0.01f; // 自定义检测间隔
    private float detectionTimer = 0f;
    private GameObject pacmen = null;

    void Start()
    {
        pacmen = GameObject.FindWithTag("Pacmen");
    }

    void Update()
    {
        detectionTimer += Time.deltaTime;
        if (detectionTimer >= detectionInterval)
        {
            detectionTimer = 0f;
            PerformCollisionCheck();
            if (Models.Pacman.Magnet)
            {
                if (Magnetcheck())
                {
                    MagnetMove();
                }
            }
        }
    }

    void PerformCollisionCheck()
    {
        float detectionRadius = 0.5f;
        Vector2 currentPosition = transform.position;
        Collider2D[] hits = Physics2D.OverlapCircleAll(currentPosition, detectionRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Pacmen"))
            {
                Debug.Log("Eat a Point.");
                gameObject.SetActive(false);
                break;
            }
        }
    }

    bool Magnetcheck()
    {
        float detectionRadius = Constants.Constants.MagnetRadius;
        Vector2 currentPosition = transform.position;
        Collider2D[] hits = Physics2D.OverlapCircleAll(currentPosition, detectionRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Pacmen"))
            {
                return true;
            }
        }
        return false;
    }

    public static void generate_point(Vector2 position)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Point");
        GameObject point =  Instantiate(prefab, new Vector3(Models.Point.InitPosition.x, Models.Point.InitPosition.y, 0), Quaternion.identity);
        point.tag = "Point";
        point.name = "Point";

        GameObject pointsParent = GameObject.Find("Points");
        if (pointsParent != null)
        {
            // 将 point 设为 pointsParent 的子对象
            point.transform.SetParent(pointsParent.transform);
        }
        else
        {
            Debug.LogError("未找到名为 'Points' 的 GameObject，请确保它存在于场景中。");
        }
    }

    public static void generate_props(Vector2 Position , Enums.TileType type)
    {
        switch (type)
        {
            case Enums.TileType.Bonus:
                {
                    GameObject prefab = Resources.Load<GameObject>("Prefabs/Bonus");
                    GameObject prop = Instantiate(prefab, new Vector3(Models.Point.InitPosition.x, Models.Point.InitPosition.y, 0), Quaternion.identity);

                    prop.tag = "Bonus";
                    prop.name = "Bonus";

                    GameObject propsParent = GameObject.Find("Props");
                    if(propsParent != null)
                    {
                        prop.transform.SetParent(propsParent.transform);
                    }
                    else{
                        Debug.LogError("未找到名为 'Props' 的 GameObject，请确保它存在于场景中。");
                    }
                    break;
                }
            case Enums.TileType.Acceleration:
                {
                    GameObject prefab = Resources.Load<GameObject>("Prefabs/Acceleration");
                    GameObject prop = Instantiate(prefab, new Vector3(Models.Point.InitPosition.x, Models.Point.InitPosition.y, 0), Quaternion.identity);

                    prop.tag = "Acceleration";
                    prop.name = "Acceleration";

                    GameObject propsParent = GameObject.Find("Props");
                    if (propsParent != null)
                    {
                        prop.transform.SetParent(propsParent.transform);
                    }
                    else
                    {
                        Debug.LogError("未找到名为 'Props' 的 GameObject，请确保它存在于场景中。");
                    }
                    break;
                }
            case Enums.TileType.Magnet:
                {
                    GameObject prefab = Resources.Load<GameObject>("Prefabs/Magnet");
                    GameObject prop = Instantiate(prefab, new Vector3(Models.Point.InitPosition.x, Models.Point.InitPosition.y, 0), Quaternion.identity);

                    prop.tag = "Magnet";
                    prop.name = "Magnet";

                    GameObject propsParent = GameObject.Find("Props");
                    if (propsParent != null)
                    {
                        prop.transform.SetParent(propsParent.transform);
                    }
                    else
                    {
                        Debug.LogError("未找到名为 'Props' 的 GameObject，请确保它存在于场景中。");
                    }
                    break;
                }
            case Enums.TileType.Shield:
                {
                    GameObject prefab = Resources.Load<GameObject>("Prefabs/Shield");
                    GameObject prop = Instantiate(prefab, new Vector3(Models.Point.InitPosition.x, Models.Point.InitPosition.y, 0), Quaternion.identity);

                    prop.tag = "Shield";
                    prop.name = "Shield";

                    GameObject propsParent = GameObject.Find("Props");
                    if (propsParent != null)
                    {
                        prop.transform.SetParent(propsParent.transform);
                    }
                    else
                    {
                        Debug.LogError("未找到名为 'Props' 的 GameObject，请确保它存在于场景中。");
                    }
                    break;
                }
            case Enums.TileType.Double:
                {
                    GameObject prefab = Resources.Load<GameObject>("Prefabs/Double");
                    GameObject prop = Instantiate(prefab, new Vector3(Models.Point.InitPosition.x, Models.Point.InitPosition.y, 0), Quaternion.identity);

                    prop.tag = "Double";
                    prop.name = "Double";

                    GameObject propsParent = GameObject.Find("Props");
                    if (propsParent != null)
                    {
                        prop.transform.SetParent(propsParent.transform);
                    }
                    else
                    {
                        Debug.LogError("未找到名为 'Props' 的 GameObject，请确保它存在于场景中。");
                    }
                    break;
                }
        }
    }

    void MagnetMove()
    {
        Vector3 target = pacmen.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target, 7f * Time.deltaTime);
    }
}