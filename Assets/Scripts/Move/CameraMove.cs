using UnityEngine;

public class WebGLMouseControl : MonoBehaviour
{
    public Camera targetCamera; // Ҫ���Ƶ����
    public float zoomSpeed = 10f; // �����ٶ�
    public float minZoom = 5f; // ��С���ž���
    public float maxZoom = 50f; // ������ž���
    public float dragSpeed = 25f; // �϶��ٶ�

    private Vector3 dragOrigin; // ��¼�϶�����ʼ��

    private void Start()
    {
        targetCamera = GetComponent<Camera>();
        dragSpeed = 25f;
    }

    void Update()
    {
        HandleZoom();
        HandleDrag();
        if (!Application.isFocused) // ��鴰���Ƿ�ʧȥ����
        {
            targetCamera.Render(); // ǿ����Ⱦ
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

}
