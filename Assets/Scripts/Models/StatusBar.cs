using Models;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public GameObject statusBarContainer;  // 状态条容器
    public GameObject statusItemPrefab;    // 状态项预制体（包含图标和文字）
    public Sprite[] statusSprites;         // 存储所有可能的状态图标

    public ReplayController replayController;

    // 假设这个变量表示当前的状态类型
    public int[] currentStatusTypes;  // 一个数组表示当前显示的状态类型

    void Start()
    {
        // 初始时清空状态栏
        UpdateStatusBar(currentStatusTypes);
        GameObject gameObject = GameObject.Find("Main Controller");
        replayController = gameObject.GetComponent<ReplayController>();
        ReplayController.onNewFrameLoaded += ChangeStatus;
        ReplayController.UpdateUI += ChangeStatus;
    }

    // 更新状态条的方法，接受一个状态类型数组作为输入
    public void UpdateStatusBar(int[] statusIndexes)
    {
        //清空当前状态图标和文字
        foreach (Transform child in statusBarContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // 根据传入的状态类型数组，添加新的状态项
        foreach (int index in statusIndexes)
        {
            if (index < statusSprites.Length)
            {
                // 实例化一个新的状态项
                GameObject statusItem = Instantiate(statusItemPrefab, statusBarContainer.transform);

                // 获取Image和Text组件
                Image iconImage = statusItem.transform.Find("Image").GetComponent<Image>();
                Text labelText = statusItem.transform.Find("Text").GetComponent<Text>();

                // 设置状态图标
                iconImage.sprite = statusSprites[index];  // 根据状态索引选择图标

                // 格式化文字并显示
                string label = "";

                if (index == 0)
                {
                    label = string.Format(Pacman.Acc.ToString());
                }
                else if (index == 1)
                {
                    label = string.Format(Pacman.Bonus.ToString());
                }else if (index == 2)
                {
                    label = string.Format(Pacman.Magnet.ToString());
                }else if(index == 3)
                {
                    label = string.Format(Pacman.Shield.ToString());
                }

                labelText.text = label;  // 设置文本
            }
        }
    }

    // 示例：动态改变状态
    public void ChangeStatus()
    {
        int[] newStatus = new int[statusSprites.Length];
        newStatus[0] = Pacman.Acc > 0 ? 0 : 114514;
        newStatus[1] = Pacman.Bonus > 0 ? 1 : 114514;
        newStatus[2] = Pacman.Magnet > 0 ? 2 : 114514;
        newStatus[3] = Pacman.Shield > 0 ? 3 : 114514;
        currentStatusTypes = newStatus;
        UpdateStatusBar(currentStatusTypes);
    }
}
