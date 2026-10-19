using TMPro;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    public TextMeshProUGUI count;
    public float number;

    private void Start()
    {
        number = PlayerPrefs.GetFloat("Highscore");
        setHighscore();
    }

    void setHighscore()
    {
        number.ToString(count.ToString());
    }
}
