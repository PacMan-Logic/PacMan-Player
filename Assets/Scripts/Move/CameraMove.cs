using System;
using UnityEngine;

public class WebGLMouseControl : MonoBehaviour
{
    public Camera targetCamera; // 要控制的相机
    public float zoomSpeed = 10f; // 缩放速度
    public float minZoom = 5f; // 最小缩放距离
    public float maxZoom = 50f; // 最大缩放距离
    public float dragSpeed = 25f; // 拖动速度

    private Vector3 dragOrigin; // 记录拖动的起始点
    private int last_map_width = 0;
    ReplayController replayController;

    private void Start()
    {
        targetCamera = Camera.main;
        dragSpeed = 25f;
        last_map_width = 0;
        replayController = GetComponent<ReplayController>();
        targetCamera.transform.position = new Vector3(20, 19, -10);
        targetCamera.orthographicSize = 21; //初始化
    }

    void Update()
    {
        HandleZoom();
        HandleDrag();
        if (!Application.isFocused) // 检查窗口是否失去焦点
        {
            targetCamera.Render(); // 强制渲染
        }
        camera_follow();
    }

    private void HandleZoom()
    {
        // 获取鼠标滚轮输入
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            // 调整相机的视野或位置（如果是透视相机则调整 fieldOfView，如果是正交相机调整 orthographicSize）
            if (targetCamera.orthographic)
            {
                targetCamera.orthographicSize = Mathf.Clamp(targetCamera.orthographicSize - scroll * zoomSpeed, minZoom, maxZoom);
            }
            else
            {
                targetCamera.fieldOfView = Mathf.Clamp(targetCamera.fieldOfView - scroll * zoomSpeed, minZoom, maxZoom);
            }
        }
    }

    private void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            // 在鼠标按下时重新设置焦点
            UnityEngine.EventSystems.EventSystem.current?.SetSelectedGameObject(gameObject);
            return;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = targetCamera.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
            dragOrigin = Input.mousePosition;

            Vector3 move = new Vector3(difference.x * dragSpeed, difference.y * dragSpeed, 0);
            targetCamera.transform.Translate(move, Space.World);
            ForceRepaint();
        }
    }
    void ForceRepaint()
    {
        // 强制重新渲染相机
        targetCamera.enabled = false;
        targetCamera.enabled = true;

        // 或手动触发一帧
        GL.Flush(); // 强制刷新 OpenGL 缓存
    }

    private void camera_follow()
    {
        if (replayController.map_width != last_map_width && replayController.map_width == 38)
        {
            targetCamera.transform.position = new Vector3(20, 20, -10);
            last_map_width = replayController.map_width;
            targetCamera.orthographicSize = 21;
        }else if (replayController.map_width != last_map_width && replayController.map_width == 29)
        {
            targetCamera.transform.position = new Vector3(15, 15, -10);
            last_map_width = replayController.map_width;
            targetCamera.orthographicSize = 16;
        }
        else if (replayController.map_width != last_map_width && replayController.map_width == 20)
        {
            targetCamera.transform.position = new Vector3(10, 10, -10);
            last_map_width = replayController.map_width;
            targetCamera.orthographicSize = 11;
        }

    }

}
