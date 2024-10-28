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
    private Vector3 targetPosition; // 目标位置
    private bool isMoving = false; // 是否正在移动到目标位置

    void Start()
    {
        if(Models.Ghost.AllGhosts != null && Models.Ghost.AllGhosts.Count > Id) {
            transform.position = new Vector3(Models.Ghost.AllGhosts[Id].CurrentPosition.x + 0.5f, Models.Ghost.AllGhosts[Id].CurrentPosition.y + 0.5f, transform.position.z);
            route = Models.Ghost.AllGhosts[Id].routes;
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
        else if (route != null && currentInstructionIndex < route.Count - 1)
        {
            currentInstructionIndex++;
            UpdateTargetPosition();
        }
    }

    void UpdateTargetPosition()
    {
        Vector3 moveDirection = Vector3.zero;
        if (route != null && currentInstructionIndex < route.Count)
        {
            targetPosition = new Vector3(route[currentInstructionIndex][0] + 0.5f, route[currentInstructionIndex][1] + 0.5f, 0);
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
        route = Models.Ghost.AllGhosts[Id].routes;
        UpdateTargetPosition();
    }
}
