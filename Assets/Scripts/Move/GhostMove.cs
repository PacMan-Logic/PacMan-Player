using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Enums;
using Models;
using UnityEngine;

public class GhostMove : MonoBehaviour
{
    public int Id;
    public float speed = 1f; // 幽灵移动的速度
    private List<MovementType> moveInstructions; // 存储移动指令的列表
    public float moveDistance = 1f; // 每次移动的距离

    private int currentInstructionIndex = 0; // 当前执行的指令索引
    private Vector3 targetPosition; // 目标位置
    private bool isMoving = false; // 是否正在移动到目标位置

    void Start()
    {
        if(Models.Ghost.AllGhosts != null && Models.Ghost.AllGhosts.Count > Id) {
            transform.position = new Vector3(Models.Ghost.AllGhosts[Id].CurrentPosition.x + 0.5f, Models.Ghost.AllGhosts[Id].CurrentPosition.y + 0.5f, transform.position.z);
            moveInstructions = Models.Ghost.AllGhosts[Id].Route;
        }
        UpdateTargetPosition();
        Models.Ghost.OnUpdated += UpdateRoute; // 订阅 Ghost 的 OnUpdated 事件
    }

    void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
        else if (currentInstructionIndex < moveInstructions.Count - 1)
        {
            currentInstructionIndex++;
            UpdateTargetPosition();
        }
    }

    void UpdateTargetPosition()
    {
        Vector3 moveDirection = Vector3.zero;
        if (moveInstructions != null && currentInstructionIndex < moveInstructions.Count)
        {
            switch (moveInstructions[currentInstructionIndex])
            {
                case MovementType.Up:
                    moveDirection = Vector3.up;
                    break;
                case MovementType.Down:
                    moveDirection = Vector3.down;
                    break;
                case MovementType.Left:
                    moveDirection = Vector3.left;
                    break;
                case MovementType.Right:
                    moveDirection = Vector3.right;
                    break;
            }
            targetPosition = transform.position + moveDirection * moveDistance;
            isMoving = true;
        }
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            isMoving = false;
        }
    }
    
    void UpdateRoute(){
        transform.position = new Vector3(Models.Ghost.AllGhosts[Id].CurrentPosition.x + 0.5f, Models.Ghost.AllGhosts[Id].CurrentPosition.y + 0.5f, transform.position.z);
        moveInstructions = Models.Ghost.AllGhosts[Id].Route;
        UpdateTargetPosition();
    }
}
