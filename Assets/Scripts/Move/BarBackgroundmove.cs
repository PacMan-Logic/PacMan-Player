using UnityEngine;
using UnityEngine.UI;

public class AdjustBlurBackgroundWithContent : MonoBehaviour
{
    public RectTransform container;        // Container ������
    public RectTransform blurBackground;   // BlurBackground ������
    public float screenScaleFactor = 0.1f; // ������Ļ��С��չ����
    public float verticalOffset = 50f;     // ������Container�߳�����

    void Update()
    {
        if (container != null && blurBackground != null)
        {
            // ���Container�Ƿ�����Ԫ��
            bool hasChildren = container.childCount > 0;

            if (hasChildren)
            {
                // ��ȡContainer��ʵ�����ݴ�С
                Vector2 containerSize = GetContentSize(container);

                // ��ȡ��Ļ�ߴ�
                float screenWidth = Screen.width;
                float screenHeight = Screen.height;

                // ����������չֵ��������Ļ�ߴ磩
                Vector2 extraSize = new Vector2(screenWidth * screenScaleFactor, screenHeight * screenScaleFactor);

                // ��̬����BlurBackground�Ĵ�С
                blurBackground.sizeDelta = containerSize + extraSize;

                // ��̬����BlurBackground��λ�ã�����ƫ�ƣ�
                blurBackground.position = container.position + new Vector3(0, verticalOffset, 0);
            }
            else
            {
                // ���û����Ԫ�أ����ر���
                blurBackground.gameObject.SetActive(false);
            }
        }
    }

    // ��ȡContainer�����ݴ�С
    private Vector2 GetContentSize(RectTransform rectTransform)
    {
        float width = 0f;
        float height = 0f;

        foreach (RectTransform child in rectTransform)
        {
            // ��ȡ��Ԫ�ص�λ�úʹ�С
            width = Mathf.Max(width, child.localPosition.x + child.sizeDelta.x * child.pivot.x);
            height = Mathf.Max(height, child.localPosition.y + child.sizeDelta.y * child.pivot.y);
        }

        return new Vector2(width, height);
    }
}
