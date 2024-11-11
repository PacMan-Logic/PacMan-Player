using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PointMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Pacmen")
        {
            //����ֻд����ʧ�����ǿ���Ҫ��Animator
            Debug.Log("Eat a Point.");
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
            // �� point ��Ϊ pointsParent ���Ӷ���
            point.transform.SetParent(pointsParent.transform);
        }
        else
        {
            Debug.LogError("δ�ҵ���Ϊ 'Points' �� GameObject����ȷ���������ڳ����С�");
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
                        Debug.LogError("δ�ҵ���Ϊ 'Props' �� GameObject����ȷ���������ڳ����С�");
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
                        Debug.LogError("δ�ҵ���Ϊ 'Props' �� GameObject����ȷ���������ڳ����С�");
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
                        Debug.LogError("δ�ҵ���Ϊ 'Props' �� GameObject����ȷ���������ڳ����С�");
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
                        Debug.LogError("δ�ҵ���Ϊ 'Props' �� GameObject����ȷ���������ڳ����С�");
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
                        Debug.LogError("δ�ҵ���Ϊ 'Props' �� GameObject����ȷ���������ڳ����С�");
                    }
                    break;
                }
        }
    }
}
