using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using Models;

public class PacmanMove : MonoBehaviour
{
    public float speed = 1f; // 小球移动的速度
    private List<MovementType> moveInstructions; // 存储移动指令的列表
    public float moveDistance = 1f; // 每次移动的距离

    private int currentInnstructionIndex = 0; // 当前执行的指令索引
    private Vector3 targetPosition; // 目标位置
    private bool isMoving = false; // 是否正在移动到目标位置

    void Start()
    {
        if(Models.Pacman.Route != null && Models.Pacman.CurrentPosition != null){
            transform.position = new Vector3(Models.Pacman.CurrentPosition.x + 0.5f, Models.Pacman.CurrentPosition.y + 0.5f, transform.position.z);
            moveInstructions = Models.Pacman.Route;
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
        else if (currentInnstructionIndex < moveInstructions.Count - 1)
        {
            currentInnstructionIndex++;
            UpdateTargetPosition();
        }
    }

    void UpdateTargetPosition()
    {
        Vector3 moveDirection = Vector3.zero;
        if (moveInstructions != null && currentInnstructionIndex < moveInstructions.Count)
        {
            switch (moveInstructions[currentInnstructionIndex])
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
        transform.position = new Vector3(Models.Pacman.CurrentPosition.x + 0.5f, Models.Pacman.CurrentPosition.y + 0.5f, transform.position.z);
        moveInstructions = Models.Pacman.Route;
        UpdateTargetPosition();
    }
}
