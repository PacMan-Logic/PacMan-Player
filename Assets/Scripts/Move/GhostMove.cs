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

    public static int level = 1; // 当前所在的关卡
    private List<List<int>> route;

    private int currentInstructionIndex = 1; // 当前执行的指令索引
    public Vector3 targetPosition; // 目标位置
    private bool isMoving = false; // 是否正在移动到目标位置

    private Vector3 prevposition;

    private Animator animator;

    public GameObject ani_controller;

    [Header("Animation Parameters")]
    [SerializeField] private float minSpeedThreshold = 0.1f; // 最小速度阈值，低于此值视为静止
    [SerializeField] private float smoothingSpeed = 10f; // 动画参数平滑过渡速度

    private Vector3 previousPosition; // 上一帧的位置
    private Vector3 currentVelocity;  // 当前速度

    private Vector3 GetRenderingPosition(Vector3 logicalPosition)
    {
        return (new Vector3(0.5f, 0.5f, 0) + logicalPosition);
    }

    void Start()
    {
        animator = ani_controller.GetComponent<Animator>();
        if(Models.Ghost.AllGhosts != null && Models.Ghost.AllGhosts.Count > Id)
        {
            transform.position = GetRenderingPosition(Models.Ghost.AllGhosts[Id].CurrentPosition);
            route = Models.Ghost.AllGhosts[Id].Route;
            speed = level * Models.Ghost.AllGhosts[Id].Speed;
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
        }else{
            transform.position = GetRenderingPosition(Models.Ghost.AllGhosts[Id].NextPosition);
        }
        UpdateAnimationParameters();
    }

    void UpdateTargetPosition()
    {
        if (route != null && currentInstructionIndex < route.Count)
        {
            if (route[currentInstructionIndex][0] < 0) // hit a wall
            {
                isMoving = false;
                return;
            }
            else
            {
                targetPosition = GetRenderingPosition(new Vector3(route[currentInstructionIndex][1],route[currentInstructionIndex][0], 0));
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
        speed = level * Models.Ghost.AllGhosts[Id].Speed;
        UpdateTargetPosition();
    }

    private void UpdateAnimationParameters()
    {
        currentVelocity = (transform.position - previousPosition) / Time.deltaTime;
        previousPosition = transform.position; // 更新上一帧的位置

        // 更新动画器参数
        animator.SetFloat("Horizontal", currentVelocity.x);
        animator.SetFloat("Vertical", currentVelocity.y);

        if (Mathf.Abs(currentVelocity.x) > minSpeedThreshold)
        {
            transform.localScale = new Vector3(
                Mathf.Sign(currentVelocity.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }
}
