using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Canvas canvas; // 需要控制的 Canvas
    public Button toggleButton; // 触发控制的按钮

    private bool isCanvasVisible = false; // Canvas 是否可见的状态

    void Start()
    {
        // 为按钮添加点击事件
        toggleButton.onClick.AddListener(ToggleCanvasVisibility);
        canvas.gameObject.SetActive(isCanvasVisible); // 显示或隐藏 Canvas
    }
    
    void ToggleCanvasVisibility()
    {
        isCanvasVisible = !isCanvasVisible;
        canvas.gameObject.SetActive(isCanvasVisible); // 显示或隐藏 Canvas
    }
}
