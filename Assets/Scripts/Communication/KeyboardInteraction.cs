using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using System.Numerics;
using Models;

public class KeyboardInteraction : MonoBehaviour
{
    private MovementType action = MovementType.Zero;
    private UnityEngine.Vector3 direction = UnityEngine.Vector3.zero;

    private GameObject obj;

    // Update is called once per frame
    private void Update()
    {
        if(true){
            obj =  GameObject.FindWithTag("Pacmen");
            if (Input.GetKeyDown(KeyCode.UpArrow)){
                action = MovementType.Up;
                direction = new UnityEngine.Vector3(0, 1, 0);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)){
                action = MovementType.Down;
                direction = new UnityEngine.Vector3(0, -1, 0);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)){
                action = MovementType.Left;
                direction = new UnityEngine.Vector3(-1, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)){
                action = MovementType.Right;
                direction = new UnityEngine.Vector3(1, 0, 0);
            }
            GameObject clone = Instantiate(obj, obj.transform.position, obj.transform.rotation);
            clone.transform.position = obj.transform.position+direction*Models.Pacman.Speed;
            ChangeColorToRed(clone);
        }
    }

    private void ChangeColorToRed(GameObject clone)
    {
        // 获取克隆GameObject的SpriteRenderer组件
        SpriteRenderer spriteRenderer = clone.GetComponent<SpriteRenderer>();

        // 如果存在SpriteRenderer组件，则改变颜色
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
        }
    }

    public void FixedUpdate()
    {
        string msg = ((int)action).ToString();
        if (string.IsNullOrEmpty(msg))
        {
            Debug.Log("No Message");
        }
        //SendToFrontend(msg);
    }

    private bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
