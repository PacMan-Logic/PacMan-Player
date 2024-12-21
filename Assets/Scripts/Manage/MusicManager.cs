using UnityEngine;
using UnityEngine.UI;
using TMPro; // 引入TMP命名空间

public class RandomMusicController : MonoBehaviour
{
    public AudioClip[] musicClips; // 存储多个音乐的数组
    public AudioSource audioSource; // 音乐播放的Audio Source
    public Button toggleButton; // 控制音乐的按钮
    public TMP_Text buttonText; // 使用TextMeshPro的按钮文本

    private int currentTrackIndex = -1; // 当前播放的音乐索引
    private bool isPlaying = true; // 音乐是否正在播放

    void Start()
    {
        // 确保AudioSource循环播放关闭
        audioSource.loop = false;

        // 为按钮添加点击事件监听器
        toggleButton.onClick.AddListener(ToggleMusic);

        // 初始化按钮文本
        UpdateButtonText();

        // 开始播放第一首随机音乐
        PlayRandomMusic();
    }

    void Update()
    {
        // 检查当前音乐是否播放完毕
        if (isPlaying && !audioSource.isPlaying)
        {
            PlayRandomMusic();
        }
    }

    private void PlayRandomMusic()
    {
        // 随机选择一首与当前不同的音乐
        int nextTrackIndex;
        do
        {
            nextTrackIndex = Random.Range(0, musicClips.Length);
        } while (nextTrackIndex == currentTrackIndex && musicClips.Length > 1);

        currentTrackIndex = nextTrackIndex;

        // 设置并播放选定的音乐
        audioSource.clip = musicClips[currentTrackIndex];
        audioSource.Play();
    }

    public void ToggleMusic()
    {
        if (isPlaying)
        {
            // 暂停音乐
            audioSource.Pause();
        }
        else
        {
            // 播放音乐
            audioSource.Play();
        }

        // 切换播放状态
        isPlaying = !isPlaying;

        // 更新按钮文本
        UpdateButtonText();
    }

    private void UpdateButtonText()
    {
        // 使用TMP更新按钮文字
        buttonText.text = isPlaying ? "Music Off" : "Music On";
    }
}
