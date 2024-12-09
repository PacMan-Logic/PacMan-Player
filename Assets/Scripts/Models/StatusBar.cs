using Models;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public GameObject statusBarContainer;  // ״̬������
    public GameObject statusItemPrefab;    // ״̬��Ԥ���壨����ͼ������֣�
    public Sprite[] statusSprites;         // �洢���п��ܵ�״̬ͼ��

    public ReplayController replayController;

    // �������������ʾ��ǰ��״̬����
    public int[] currentStatusTypes;  // һ�������ʾ��ǰ��ʾ��״̬����

    void Start()
    {
        // ��ʼʱ���״̬��
        UpdateStatusBar(currentStatusTypes);
        GameObject gameObject = GameObject.Find("Main Controller");
        replayController = gameObject.GetComponent<ReplayController>();
        ReplayController.onNewFrameLoaded += ChangeStatus;
        ReplayController.UpdateUI += ChangeStatus;
    }

    // ����״̬���ķ���������һ��״̬����������Ϊ����
    public void UpdateStatusBar(int[] statusIndexes)
    {
        //��յ�ǰ״̬ͼ�������
        foreach (Transform child in statusBarContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // ���ݴ����״̬�������飬����µ�״̬��
        foreach (int index in statusIndexes)
        {
            if (index < statusSprites.Length)
            {
                // ʵ����һ���µ�״̬��
                GameObject statusItem = Instantiate(statusItemPrefab, statusBarContainer.transform);

                // ��ȡImage��Text���
                Image iconImage = statusItem.transform.Find("Image").GetComponent<Image>();
                Text labelText = statusItem.transform.Find("Text").GetComponent<Text>();

                // ����״̬ͼ��
                iconImage.sprite = statusSprites[index];  // ����״̬����ѡ��ͼ��

                // ��ʽ�����ֲ���ʾ
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

                labelText.text = label;  // �����ı�
            }
        }
    }

    // ʾ������̬�ı�״̬
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
