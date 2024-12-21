using UnityEngine;
using UnityEngine.UI;
using TMPro; // ����TMP�����ռ�

public class RandomMusicController : MonoBehaviour
{
    public AudioClip[] musicClips; // �洢������ֵ�����
    public AudioSource audioSource; // ���ֲ��ŵ�Audio Source
    public Button toggleButton; // �������ֵİ�ť
    public TMP_Text buttonText; // ʹ��TextMeshPro�İ�ť�ı�

    private int currentTrackIndex = -1; // ��ǰ���ŵ���������
    private bool isPlaying = true; // �����Ƿ����ڲ���

    void Start()
    {
        // ȷ��AudioSourceѭ�����Źر�
        audioSource.loop = false;

        // Ϊ��ť��ӵ���¼�������
        toggleButton.onClick.AddListener(ToggleMusic);

        // ��ʼ����ť�ı�
        UpdateButtonText();

        // ��ʼ���ŵ�һ���������
        PlayRandomMusic();
    }

    void Update()
    {
        // ��鵱ǰ�����Ƿ񲥷����
        if (isPlaying && !audioSource.isPlaying)
        {
            PlayRandomMusic();
        }
    }

    private void PlayRandomMusic()
    {
        // ���ѡ��һ���뵱ǰ��ͬ������
        int nextTrackIndex;
        do
        {
            nextTrackIndex = Random.Range(0, musicClips.Length);
        } while (nextTrackIndex == currentTrackIndex && musicClips.Length > 1);

        currentTrackIndex = nextTrackIndex;

        // ���ò�����ѡ��������
        audioSource.clip = musicClips[currentTrackIndex];
        audioSource.Play();
    }

    public void ToggleMusic()
    {
        if (isPlaying)
        {
            // ��ͣ����
            audioSource.Pause();
        }
        else
        {
            // ��������
            audioSource.Play();
        }

        // �л�����״̬
        isPlaying = !isPlaying;

        // ���°�ť�ı�
        UpdateButtonText();
    }

    private void UpdateButtonText()
    {
        // ʹ��TMP���°�ť����
        buttonText.text = isPlaying ? "Music Off" : "Music On";
    }
}
