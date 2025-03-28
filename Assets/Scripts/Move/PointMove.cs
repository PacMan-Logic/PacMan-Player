using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class PointMove : MonoBehaviour
{
    public float detectionInterval = 0.00001f; // 自定义检测间隔
    private float detectionTimer = 0f;
    private GameObject pacmen = null;
    private bool is_magneted;

    public List<Sprite> sprites = new List<Sprite>();

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
            if(this.CompareTag("Teleport")){
                if (Models.Data.portal)
                {
                    GetComponent<SpriteRenderer>().sprite = sprites[1];

                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = sprites[0];
                }
                return;
            }
            PerformCollisionCheck();
            if (Models.Pacman.Magnet > 0)
            {
                if (Magnetcheck())
                {
                    is_magneted = true;
                }
            }
            if (is_magneted)
            {
                MagnetMove();
            }
        }
    }

    void PerformCollisionCheck()
    {
        if(Vector3.Distance(transform.position, Models.Pacman.NowPosition) <= Constants.Constants.NormalRadius){
                if (this.CompareTag("Acceleration"))
                {
                    Models.Pacman.Speed = 2; //加速，为了交互时显示
                    InteractController.speedupstop = false;
                }
                gameObject.SetActive(false);
        }
        // float detectionRadius = Constants.Constants.NormalRadius;
        // Vector2 currentPosition = transform.position;
        // Collider2D[] hits = Physics2D.OverlapCircleAll(currentPosition, detectionRadius);
        // foreach (var hit in hits)
        // {
        //     if (hit.CompareTag("Pacmen"))
        //     {
        //         Debug.Log("Eat a Point.");
        //         if (this.CompareTag("Acceleration"))
        //         {
        //             Models.Pacman.Speed = 2; //加速，为了交互时显示
        //             InteractController.speedupstop = false;
        //         }
        //         gameObject.SetActive(false);
        //         break;
        //     }
        // }
    }

    bool Magnetcheck()
    {
        float detectionRadius = Constants.Constants.MagnetRadius;
        // if (Models.Pacman.current_level == 1)
        // {
        //     detectionRadius = Constants.Constants.MagnetRadius_in_f;
        // }
        if(Vector3.Distance(transform.position, Models.Pacman.NowPosition) <= detectionRadius){
            return true;
        }
        // Vector2 currentPosition = transform.position;
        // Collider2D[] hits = Physics2D.OverlapCircleAll(currentPosition, detectionRadius);
        // foreach (var hit in hits)
        // {
        //     if (hit.CompareTag("Pacmen"))
        //     {
        //         return true;
        //     }
        // }
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
            case Enums.TileType.Teleport:
                {
                    GameObject prefab = Resources.Load<GameObject>("Prefabs/Teleport");
                    GameObject prop = Instantiate(prefab, new Vector3(Models.Point.InitPosition.x, Models.Point.InitPosition.y, 0), Quaternion.identity);

                    prop.tag = "Teleport";
                    prop.name = "Teleport";

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
            case Enums.TileType.Stop:
                {
                    GameObject prefab = Resources.Load<GameObject>("Prefabs/Stop");
                    GameObject prop = Instantiate(prefab, new Vector3(Models.Point.InitPosition.x, Models.Point.InitPosition.y, 0), Quaternion.identity);

                    prop.tag = "Stop";
                    prop.name = "Stop";

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
        transform.position = Vector3.MoveTowards(transform.position, target, 7f*ReplayController.replayspeed*Time.deltaTime);
    }
}