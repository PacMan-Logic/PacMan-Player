using System;
using Unity.VisualScripting;
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

    public int index = 0;
    public Vector3 offset = new Vector3(0, 5, -10); // ��ͷ��Ŀ�������ƫ����
    public float smoothSpeed = 0.125f; // ��ͷ�ƶ���ƽ���ٶ�
    public Transform target;

    public bool is_first = true;
    public bool is_following = false;
    public bool is_rolled = false;

    private void Start()
    {
        targetCamera = Camera.main;
        dragSpeed = 25f;
        last_map_width = 0;
        replayController = GetComponent<ReplayController>();
        targetCamera.transform.position = new Vector3(20, 19, -10);
        targetCamera.orthographicSize = 21; //��ʼ
        if (ModeController.IsInteractMode())
        {
            index = 0;
            is_first = true;
            target = null;
        }
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
        if (ModeController.IsReplayMode()) {
            Get_Keybroad_inout();
        }
    }

    private void LateUpdate()
    {
        if (index == 0 && is_first)
        {
            Console.WriteLine("Overall Mode Working");
            is_following = false;
            if (replayController.map_width == 41)
            {
                targetCamera.transform.position = new Vector3(20, 19, -10);
                last_map_width = replayController.map_width;
                targetCamera.orthographicSize = 21;
            }
            else if (replayController.map_width == 32)
            {
                targetCamera.transform.position = new Vector3(15, 15, -10);
                last_map_width = replayController.map_width;
                targetCamera.orthographicSize = 16;
            }
            else if (replayController.map_width == 22)
            {
                targetCamera.transform.position = new Vector3(10, 10, -10);
                last_map_width = replayController.map_width;
                targetCamera.orthographicSize = 11;
            }
            is_first = false;
            return;
        }

        if(target == null)
        {
            if(index != 0)
            {
                Console.WriteLine(index);
                Console.WriteLine("???");
            }
            return;
        }

        is_following = true;
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(targetCamera.transform.position, desiredPosition, smoothSpeed);
        targetCamera.transform.position = smoothedPosition;
        if (!is_rolled)
        {
            targetCamera.orthographicSize = 8;
        }
        
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
            is_rolled = true;
        }
    }

    private void HandleDrag()
    {
        if(!is_following) { 
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
        if (index == 0)
        {
            if (replayController.map_width != last_map_width && replayController.map_width == 41)
            {
                targetCamera.transform.position = new Vector3(20, 19, -10);
                last_map_width = replayController.map_width;
                targetCamera.orthographicSize = 21;
            }
            else if (replayController.map_width != last_map_width && replayController.map_width == 32)
            {
                targetCamera.transform.position = new Vector3(15, 15, -10);
                last_map_width = replayController.map_width;
                targetCamera.orthographicSize = 16;
            }
            else if (replayController.map_width != last_map_width && replayController.map_width == 22)
            {
                targetCamera.transform.position = new Vector3(10, 10, -10);
                last_map_width = replayController.map_width;
                targetCamera.orthographicSize = 11;
            }
        }

    }

    private void Get_Keybroad_inout()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            index = 0;
            is_first = true;
            target = null;
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            index = 1;
            is_rolled = false;
            is_first = true;
            target = GameObject.FindWithTag("Pacmen").transform;
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            index = 2;
            is_rolled = false;
            is_first = true;
            target = GameObject.FindWithTag("Ghost0").transform;
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            index = 3;
            is_rolled = false;
            is_first = true;
            target = GameObject.FindWithTag("Ghost1").transform;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            index = 4;
            is_rolled = false;
            is_first = true;
            target = GameObject.FindWithTag("Ghost2").transform;
        }
    }

}
