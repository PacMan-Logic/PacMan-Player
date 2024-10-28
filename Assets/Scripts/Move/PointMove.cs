using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PointMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //GameObject prefab = Resources.Load<GameObject>("Prefab/Point");
        //if (Models.Point.isRedended == true)
        //{
        //    Instantiate(prefab, new Vector3(Models.Point.InitPosition.x, Models.Point.InitPosition.y, 0), Quaternion.identity);
        //}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Pacmen")
        {
            //暂且只写了消失，但是可能要加Animator
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
        GameObject prefab = Resources.Load<GameObject>("Prefab/Point");
        GameObject point =  Instantiate(prefab, new Vector3(Models.Point.InitPosition.x, Models.Point.InitPosition.y, 0), Quaternion.identity);
        point.tag = "Point";
        point.name = "Point";
    }
}
