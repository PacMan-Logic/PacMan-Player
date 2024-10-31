using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using Models;

public class PacmanMove : MonoBehaviour
{
    public static float speed = 1f; // 小球移动的速度
    private List<List<int>> route;

    private int currentInnstructionIndex = 1; // 当前执行的指令索引
    private Vector3 targetPosition; // 目标位置
    private bool isMoving = false; // 是否正在移动到目标位置

    private Vector3 GetRenderingPosition(Vector3 logicalPosition)
    {
        return (new Vector3(0.5f, 0.5f, 0) + logicalPosition);
    }

    void Start()
    {
        if(Models.Pacman.Route != null && Models.Pacman.CurrentPosition != null){
            transform.position = new Vector3(Models.Pacman.CurrentPosition.x + 0.5f, Models.Pacman.CurrentPosition.y + 0.5f, transform.position.z);
            route = Models.Pacman.Route;
        }
        UpdateTargetPosition();
        Models.Pacman.OnUpdated += UpdateRoute; // 订阅 Pacman 的 OnUpdated 事件
    }

    void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
        else if (route != null &&  currentInnstructionIndex < route.Count - 1)
        {
            currentInnstructionIndex++;
            UpdateTargetPosition();
        }
    }

    void UpdateTargetPosition()
    {
        Vector3 moveDirection = Vector3.zero;
        if (route != null && currentInnstructionIndex < route.Count)
        {
            targetPosition = new Vector3(route[currentInnstructionIndex][0] + 0.5f, route[currentInnstructionIndex][1] + 0.5f, 0);
            isMoving = true;
        }
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            isMoving = false;
        }
    }
    
    void UpdateRoute(){
        transform.position = GetRenderingPosition(Models.Pacman.CurrentPosition);
        if (route != null && currentInnstructionIndex < route.Count)
        {
            if (route[currentInnstructionIndex][0] < 0) // hit a wall
            {
                isMoving = false;
            }
            else
            {
                targetPosition = GetRenderingPosition(new Vector3(route[currentInnstructionIndex][0],
                    route[currentInnstructionIndex][1], 0));
                isMoving = true;
            }
        }
    }
}
