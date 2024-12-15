using System;
using UnityEngine;

public class WebGLMouseControl : MonoBehaviour
{
    public Camera targetCamera; // Ҫ���Ƶ����
    public float zoomSpeed = 10f; // �����ٶ�
    public float minZoom = 5f; // ��С���ž���
    public float maxZoom = 50f; // ������ž���
    public float dragSpeed = 25f; // �϶��ٶ�

    private Vector3 dragOrigin; // ��¼�϶�����ʼ��
    private int last_map_width = 0;
    ReplayController replayController;

    private void Start()
    {
        targetCamera = Camera.main;
        dragSpeed = 25f;
        last_map_width = 0;
        replayController = GetComponent<ReplayController>();
        targetCamera.transform.position = new Vector3(20, 19, -10);
        targetCamera.orthographicSize = 21; //��ʼ��
    }

    void Update()
    {
        HandleZoom();
        HandleDrag();
        if (!Application.isFocused) // ��鴰���Ƿ�ʧȥ����
        {
            targetCamera.Render(); // ǿ����Ⱦ
        }
        camera_follow();
    }

    private void HandleZoom()
    {
        // ��ȡ����������
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            // �����������Ұ��λ�ã������͸���������� fieldOfView������������������ orthographicSize��
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
            // ����갴��ʱ�������ý���
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
        // ǿ��������Ⱦ���
        targetCamera.enabled = false;
        targetCamera.enabled = true;

        // ���ֶ�����һ֡
        GL.Flush(); // ǿ��ˢ�� OpenGL ����
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
