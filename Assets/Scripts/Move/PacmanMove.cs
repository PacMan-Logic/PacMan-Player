using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using Models;
using System;
using Unity.VisualScripting;

public class PacmanMove : MonoBehaviour
{
    public static float speed = 1f; // 小球移动的速度

    public static int level = 1; // 小球当前所在的关卡
    private List<List<int>> route;

    private int currentInstructionIndex = 1; // 当前执行的指令索引
    private Vector3 targetPosition; // 目标位置
    private bool isMoving = false; // 是否正在移动到目标位置

    private Vector3 prevposition;

    private Animator animator;
    private Animator trail_animator;

    public GameObject ani_controller;
    public GameObject trail_controller;

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
        trail_animator = trail_controller.GetComponent<Animator>();
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        previousPosition = transform.position; // 初始化上一帧的位置

        if (Models.Pacman.Route != null && Models.Pacman.CurrentPosition != null){
            transform.position = new Vector3(Models.Pacman.CurrentPosition.y + 0.5f, Models.Pacman.CurrentPosition.x + 0.5f, transform.position.z);
            route = Models.Pacman.Route;
            speed = level * Models.Pacman.Speed;
        }
        UpdateTargetPosition();
        Models.Pacman.OnUpdated += UpdateRoute; // 订阅 Pacman 的 OnUpdated 事件
        prevposition = transform.position;
        trail_animator.SetBool("boost", false);
    }

    void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
        else if (route != null &&  currentInstructionIndex < route.Count - 1)
        {
            currentInstructionIndex++;
            UpdateTargetPosition();
        }else{
            if(Models.Pacman.eaten){
                Models.Pacman.eaten = false;
                Models.Pacman.Speed = 1;
                Models.Pacman.Magnet = 0;
                Models.Pacman.Acc = 0;
                Models.Pacman.Shield = 0;
            }
            transform.position = GetRenderingPosition(Models.Pacman.NextPosition);
        }
        Models.Pacman.NowPosition = transform.position;

        UpdateAnimationParameters();
        UpdateTrail();
    }

    void UpdateTargetPosition()
    {
        Vector3 moveDirection = Vector3.zero;
        if (route != null && currentInstructionIndex < route.Count)
        {
            if (route[currentInstructionIndex][0] < 0)
            {
                isMoving = false;
                return;
            }
            targetPosition = new Vector3(route[currentInstructionIndex][1] + 0.5f, route[currentInstructionIndex][0] + 0.5f, 0);
            //moveDirection = targetPosition - transform.position;
            //if(moveDirection == Vector3.right){
            //    transform.rotation = Quaternion.Euler(0, 0, 0);
            //}else if(moveDirection == Vector3.left){
            //    transform.rotation = Quaternion.Euler(0, 180, 0);
            //}else if(moveDirection == Vector3.up){
            //    transform.rotation = Quaternion.Euler(0, 0, 90);
            //}else if(moveDirection == Vector3.down){
            //    transform.rotation = Quaternion.Euler(0, 0, 270);
            //}
            prevposition = transform.position;
            isMoving = true;
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

    void UpdateRoute(){
        //animator.Play("pacman_eat");
        //animator.speed = ReplayController.replayspeed;
        currentInstructionIndex = 1;
        transform.position = GetRenderingPosition(Models.Pacman.CurrentPosition);
        Models.Pacman.NowPosition = transform.position;
        route = Models.Pacman.Route;
        speed = level * Models.Pacman.Speed;
        // if(Models.Pacman.eaten){
        //     animator.Play("pacman_death");
        // }
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

    private void UpdateTrail()
    {
        if (Models.Pacman.Acc > 0)
        {
            trail_animator.SetBool("boost", true);
        }
        else
        {
            trail_animator.SetBool("boost", false);
        }

        if (Mathf.Abs(currentVelocity.y) > 0)
        {
            trail_controller.transform.localPosition = new Vector3(
                    0,
                    currentVelocity.y > 0 ? -1f : 1f,
                    0
                );
            trail_controller.transform.eulerAngles = new Vector3(
                    0,
                    0,
                    currentVelocity.y >0 ? 180 : 0
                );
            trail_controller.transform.localScale = new Vector3(5, 5, 0);
        }
        if (Mathf.Abs(currentVelocity.x) > 0)
        {
            trail_controller.transform.localPosition = new Vector3(
                    -1f,
                    0,
                    0
                );
            trail_controller.transform.eulerAngles = new Vector3(
                    0,
                    0,
                    90
                );
            trail_controller.transform.localScale = new Vector3(5 , currentVelocity.x > 0 ? 5 : -5, 0);
        }
    }
}
