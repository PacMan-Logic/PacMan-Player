using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText; // 引用得分栏的 TMP_Text
    private int score = 0; // 当前得分

    void Start()
    {
        scoreText.text = $"Pacman : {Models.Data.pacman_score} | Ghost: {Models.Data.ghost_score}";
    }

    private void Update()
    {
        scoreText.text = $"Pacman : {Models.Data.pacman_score} | Ghost: {Models.Data.ghost_score}";
    }
}
