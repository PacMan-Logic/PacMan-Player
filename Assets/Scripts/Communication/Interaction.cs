using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class Interaction : MonoBehaviour
{
    private MovementType action = MovementType.Right;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverUI())
            {
                return;
            }
            float screenHeight = Screen.height / 4;
            float screenWidth = Screen.width / 2;
            Vector3 MousePosition = Input.mousePosition;
            if(MousePosition.y > 3 * screenHeight)
            {
                Debug.Log("up");
            }else if(MousePosition.y < screenHeight)
            {
                Debug.Log("down");
            }else if(MousePosition.x < screenWidth)
            {
                Debug.Log("left");
            }else if(MousePosition.x >  screenWidth)
            {
                Debug.Log("right");
            }
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
