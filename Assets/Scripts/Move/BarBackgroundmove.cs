using UnityEngine;
using UnityEngine.UI;

public class AdjustBlurBackgroundWithContent : MonoBehaviour
{
    public RectTransform container;        // Container 的引用
    public RectTransform blurBackground;   // BlurBackground 的引用
    public float screenScaleFactor = 0.1f; // 根据屏幕大小扩展比例
    public float verticalOffset = 50f;     // 背景比Container高出多少

    void Update()
    {
        if (container != null && blurBackground != null)
        {
            // 检查Container是否有子元素
            bool hasChildren = container.childCount > 0;

            if (hasChildren)
            {
                // 获取Container的实际内容大小
                Vector2 containerSize = GetContentSize(container);

                // 获取屏幕尺寸
                float screenWidth = Screen.width;
                float screenHeight = Screen.height;

                // 计算额外的扩展值（基于屏幕尺寸）
                Vector2 extraSize = new Vector2(screenWidth * screenScaleFactor, screenHeight * screenScaleFactor);

                // 动态设置BlurBackground的大小
                blurBackground.sizeDelta = containerSize + extraSize;

                // 动态设置BlurBackground的位置（向上偏移）
                blurBackground.position = container.position + new Vector3(0, verticalOffset, 0);
            }
            else
            {
                // 如果没有子元素，隐藏背景
                blurBackground.gameObject.SetActive(false);
            }
        }
    }

    // 获取Container的内容大小
    private Vector2 GetContentSize(RectTransform rectTransform)
    {
        float width = 0f;
        float height = 0f;

        foreach (RectTransform child in rectTransform)
        {
            // 获取子元素的位置和大小
            width = Mathf.Max(width, child.localPosition.x + child.sizeDelta.x * child.pivot.x);
            height = Mathf.Max(height, child.localPosition.y + child.sizeDelta.y * child.pivot.y);
        }

        return new Vector2(width, height);
    }
}
