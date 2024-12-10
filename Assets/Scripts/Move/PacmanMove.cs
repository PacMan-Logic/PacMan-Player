using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using Models;

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

    private Vector3 GetRenderingPosition(Vector3 logicalPosition)
    {
        return (new Vector3(0.5f, 0.5f, 0) + logicalPosition);
    }

    void Start()
    {
        animator = GameObject.Find("animation_controller").GetComponent<Animator>();
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        if(Models.Pacman.Route != null && Models.Pacman.CurrentPosition != null){
            transform.position = new Vector3(Models.Pacman.CurrentPosition.y + 0.5f, Models.Pacman.CurrentPosition.x + 0.5f, transform.position.z);
            route = Models.Pacman.Route;
            speed = level * Models.Pacman.Speed;
        }
        UpdateTargetPosition();
        Models.Pacman.OnUpdated += UpdateRoute; // 订阅 Pacman 的 OnUpdated 事件
        prevposition = transform.position;
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
        }
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
            moveDirection = targetPosition - transform.position;
            if(moveDirection == Vector3.right){
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }else if(moveDirection == Vector3.left){
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }else if(moveDirection == Vector3.up){
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }else if(moveDirection == Vector3.down){
                transform.rotation = Quaternion.Euler(0, 0, 270);
            }
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
        animator.enabled = true;
        Debug.Log("UpdateRoute Invoked");
        currentInstructionIndex = 1;
        transform.position = GetRenderingPosition(Models.Pacman.CurrentPosition);
        route = Models.Pacman.Route;
        speed = level * Models.Pacman.Speed;
        if(Models.Pacman.eaten){
            animator.enabled = false;  //让动画停止，之后应该改为播放死亡动画，到回合结束
            Models.Pacman.eaten = false;
        }
        UpdateTargetPosition();
    }
}
