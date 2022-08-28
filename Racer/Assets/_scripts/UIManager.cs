using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text.RegularExpressions;
public class UIManager : MonoBehaviour
{
    Text speedText;
    Text scoreText;
    [SerializeField]Text bestScoreText;
    [SerializeField]Text currentScoreText;
    private static float _score=0;
    private static UIManager S;
    private void Awake()
    {
        S = this;
        speedText = GameObject.Find("SpeedText").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
    }
    private void Start()
    {
        _score = 0;
    }

    public static void SaveSatistics() {
        float currentScore;
        if (S.scoreText.text.Contains("x")) currentScore = float.Parse(S.scoreText.text.Trim('x', '2'));
        else currentScore = float.Parse(S.scoreText.text);
        if (PlayerPrefs.HasKey("BestScore")) {
            if (PlayerPrefs.GetFloat("BestScore") < currentScore) PlayerPrefs.SetFloat("BestScore", currentScore);
            
        }
        else PlayerPrefs.SetFloat("BestScore", currentScore);
        S.bestScoreText.text = PlayerPrefs.GetFloat("BestScore").ToString();
        S.currentScoreText.text = currentScore.ToString();
        _score = 0;

    }


    void Update()
    {
        speedText.text = Mathf.Ceil(PlayerCar.CURRENT_SPEED).ToString();
        GetScore();
    }
    void GetScore() {
        if (PlayerCar.CURRENT_SPEED < PlayerCar.MIN_SPEED_TO_GAME_ACTIONS || PlayerCar.ExecuteCodeIfCarStayInDisplay()) return;
        else {
            if (PlayerCar.PosOnTheAheadPartOfRoad) {
                scoreText.color= Color.green;
                scoreText.text = (float.Parse(Regex.Replace(scoreText.text, "x2", "")) + PlayerCar.CURRENT_SPEED * Time.deltaTime / 5).ToString("0.00") +"x2";
                return;
            }
            scoreText.color = Color.white;
            if (scoreText.text.Contains("x2")) scoreText.text = Regex.Replace(scoreText.text, "x2", "");
            scoreText.text = (float.Parse(scoreText.text) + PlayerCar.CURRENT_SPEED * Time.deltaTime/10).ToString("0.00");
        }
    }
}
