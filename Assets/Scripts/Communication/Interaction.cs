using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class Interaction : MonoBehaviour
{
    private MovementType action = MovementType.Right;
    private float timer = 0;
    public bool needOP???;
    // Update is called once per frame

    private void Update()

    {
        if (needOP)
        {
            timer += Time.deltaTime;
            if (timer >= timer???) {
                needOP = false;
                timer = 0;
            }
            //监听主键盘'='
            if (Input.GetKeyDown(KeyCode.Equals))
            {
                OnKeyPressed(0);
                action[i] = 0
            }
            //监听鼠标小键盘'='
            if (Input.GetKeyDown(KeyCode.KeypadEquals))
            {
                OnKeyPressed(0);
            }
            //监听↑
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                OnKeyPressed(1);
                //OnUpArrowKeyPressed();
            }
            //监听←
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                OnKeyPressed(2);
            }
            //监听↓
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                OnKeyPressed(3);
                //OnDownArrowKeyPressed();
            }
            //监听→
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                OnKeyPressed(4);
            }
        }
        // if (Input.GetMouseButtonDown(0))
        // {
        //     if (IsPointerOverUI())
        //     {
        //         return;
        //     }
        //     float screenHeight = Screen.height / 4;
        //     float screenWidth = Screen.width / 2;
        //     Vector3 MousePosition = Input.mousePosition;
        //     if(MousePosition.y > 3 * screenHeight)
        //     {
        //         Debug.Log("up");
        //     }else if(MousePosition.y < screenHeight)
        //     {
        //         Debug.Log("down");
        //     }else if(MousePosition.x < screenWidth)
        //     {
        //         Debug.Log("left");
        //     }else if(MousePosition.x >  screenWidth)
        //     {
        //         Debug.Log("right");
        //     }
        // }
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
    public void OnKeyPressed(int action_code)
    {
        GetComponentInParent<WebInteractionController>.SendAction(???);
    }
}
