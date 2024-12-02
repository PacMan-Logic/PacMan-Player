using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
//不确定是否能使用，所以就注释掉了
public class Interaction : MonoBehaviour
{
    // private MovementType action = MovementType.Right;
    // private float timer = 0;
    // public bool needOP???;
    // // Update is called once per frame

    // private void Update()

    // {
    //     if (needOP)
    //     {
    //         timer += Time.deltaTime;
    //         if (timer >= timer???) {
    //             needOP = false;
    //             timer = 0;
    //         }
    //         //监听主键盘'='
    //         if (Input.GetKeyDown(KeyCode.Equals))
    //         {
    //             OnKeyPressed(0);
    //             action[i] = 0
    //         }
    //         //监听鼠标小键盘'='
    //         if (Input.GetKeyDown(KeyCode.KeypadEquals))
    //         {
    //             OnKeyPressed(0);
    //         }
    //         //监听↑
    //         if (Input.GetKeyDown(KeyCode.UpArrow))
    //         {
    //             OnKeyPressed(1);
    //             //OnUpArrowKeyPressed();
    //         }
    //         //监听←
    //         if (Input.GetKeyDown(KeyCode.LeftArrow))
    //         {
    //             OnKeyPressed(2);
    //         }
    //         //监听↓
    //         if (Input.GetKeyDown(KeyCode.DownArrow))
    //         {
    //             OnKeyPressed(3);
    //             //OnDownArrowKeyPressed();
    //         }
    //         //监听→
    //         if (Input.GetKeyDown(KeyCode.RightArrow))
    //         {
    //             OnKeyPressed(4);
    //         }
    //     }
    // }

    // public void FixedUpdate()
    // {
    //     string msg = ((int)action).ToString();
    //     if (string.IsNullOrEmpty(msg))
    //     {
    //         Debug.Log("No Message");
    //     }
    //     //SendToFrontend(msg);
    // }

    // private bool IsPointerOverUI()
    // {
    //     return EventSystem.current.IsPointerOverGameObject();
    // }
    // public void OnKeyPressed(int action_code)
    // {
    //     GetComponentInParent<WebInteractionController>.SendAction(action_code);//GetComponentInParent是什么意思
    // }
}
