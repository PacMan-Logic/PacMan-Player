using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Unity.VisualScripting;

public class Interaction : MonoBehaviour
{
    private MovementType action = MovementType.Right;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            action = MovementType.Up;
            Debug.Log("Up");
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            action = MovementType.Down;
            Debug.Log("Down");
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            action = MovementType.Right;
            Debug.Log("Right");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            action = MovementType.Left;
            Debug.Log("Left");
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
}
