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
            // ��¼��갴��ʱ����ʼλ��
            dragOrigin = Input.mousePosition;
            return;
        }

        if (Input.GetMouseButton(0))
        {
            // ��������϶���ƫ����
            Vector3 difference = targetCamera.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
            dragOrigin = Input.mousePosition;

            // ����ƫ�����ƶ����
            Vector3 move = new Vector3(difference.x * dragSpeed, difference.y * dragSpeed, 0);
            targetCamera.transform.Translate(move, Space.World);
        }
    }
}
