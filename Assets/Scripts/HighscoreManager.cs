using TMPro;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    public TextMeshProUGUI count;
    public float score;
    public float highscore;

    private void Start()
    {
        score = PlayerPrefs.GetFloat("Highscore");
        if(score> PlayerPrefs.GetFloat("Highscore"))
        {
            highscore = score;
            setHighscore();
        }
        else
        {
            setScore();
        }
    }

    void setHighscore()
    {
        count.SetText(highscore.ToString());
    }

    void setScore()
    {
        count.SetText(score.ToString());
    }
}
