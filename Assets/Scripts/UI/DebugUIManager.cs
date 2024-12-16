using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Canvas canvas; // ��Ҫ���Ƶ� Canvas
    public Button toggleButton; // �������Ƶİ�ť

    private bool isCanvasVisible = false; // Canvas �Ƿ�ɼ���״̬

    void Start()
    {
        // Ϊ��ť��ӵ���¼�
        toggleButton.onClick.AddListener(ToggleCanvasVisibility);
        canvas.gameObject.SetActive(isCanvasVisible); // ��ʾ������ Canvas
        Debug.Log("Init Success");
    }
    
    void ToggleCanvasVisibility()
    {
        isCanvasVisible = !isCanvasVisible;
        canvas.gameObject.SetActive(isCanvasVisible); // ��ʾ������ Canvas
        Debug.Log("Changed");
    }
}
