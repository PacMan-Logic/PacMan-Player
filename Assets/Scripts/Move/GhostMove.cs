using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Enums;
using Models;
using UnityEngine;

public class GhostMove : MonoBehaviour
{
    public int Id;
    public static float speed = 1f; // 幽灵移动的速度
    private List<List<int>> route;

    private int currentInstructionIndex = 1; // 当前执行的指令索引
    public Vector3 targetPosition; // 目标位置
    private bool isMoving = false; // 是否正在移动到目标位置

    private Vector3 prevposition;

    private Vector3 GetRenderingPosition(Vector3 logicalPosition)
    {
        return (new Vector3(0.5f, 0.5f, 0) + logicalPosition);
    }

    void Start()
    {
        if(Models.Ghost.AllGhosts != null && Models.Ghost.AllGhosts.Count > Id)
        {
            transform.position = GetRenderingPosition(Models.Ghost.AllGhosts[Id].CurrentPosition);
            route = Models.Ghost.AllGhosts[Id].Route;
        }
        UpdateTargetPosition();
        Models.Ghost.OnUpdated += UpdateRoute; // 订阅 Ghost 的 OnUpdated 事件
        prevposition = transform.position;
    }

    void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
        else if (route != null && currentInstructionIndex < route.Count - 1)
        {
            currentInstructionIndex++;
            UpdateTargetPosition();
        }
    }

    void UpdateTargetPosition()
    {
        if (route != null && currentInstructionIndex < route.Count)
        {
            if (route[currentInstructionIndex][0] < 0) // hit a wall
            {
                isMoving = false;
            }
            else
            {
                targetPosition = GetRenderingPosition(new Vector3(route[currentInstructionIndex][0],
                    route[currentInstructionIndex][1], 0));
                prevposition = transform.position;
                isMoving = true;
            }
        }
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.001f || Vector3.Dot(transform.position - targetPosition, prevposition - targetPosition) < 0)
        {
            transform.position = targetPosition;
            isMoving = false;
        }
    }
    
    void UpdateRoute()
    {
        transform.position = GetRenderingPosition(Models.Ghost.AllGhosts[Id].CurrentPosition);
        route = Models.Ghost.AllGhosts[Id].Route;
        UpdateTargetPosition();
    }
}
