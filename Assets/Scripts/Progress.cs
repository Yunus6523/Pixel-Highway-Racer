using UnityEngine;

public class Progress : MonoBehaviour
{
    [Header("Normal Cars")]
    //BMK K2
    [SerializeField] bool BMKUnlocked;
    [SerializeField] int BMKIndex;

    //SilverBird MZ-7
    [SerializeField] bool silverbirdMZ7Unlocked;
    [SerializeField] int silverbirdMZ7Index;

    //FourRings Super Six
    [SerializeField] bool fourRingsSuperSixUnlocked;
    [SerializeField] int fourRingsSuperSixIndex;

    //Gear-San Gizmo GTR
    [SerializeField] bool gearSanGizmoGTRUnlocked;
    [SerializeField] int gearSanGizmoGTRIndex;

    [Header("Rival Cars")]
    //Yuriko's MZ-5
    [SerializeField] bool yurikosMZ5Unlocked;
    [SerializeField] int yurikosMZ5Index;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPrefs.SetInt("BMK", BMKIndex);
        //BMKIndex = PlayerPrefs.GetInt("BMK");

        PlayerPrefs.SetInt("MZ-7", silverbirdMZ7Index);
        PlayerPrefs.SetInt("4Rings", fourRingsSuperSixIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("BMK") == 0)
        {
            BMKUnlocked = false;
            GameManager.instance.pandaNext.SetActive(false);
        }
        if (PlayerPrefs.GetInt("BMK") == 1)
        {
            BMKUnlocked = true;
            GameManager.instance.pandaNext.SetActive(true);
        }

        if(GameManager.instance.count >= 20)
        {
            silverbirdMZ7Index = 1;
        }

        if (PlayerPrefs.GetInt("MZ-7") == 0)
        {
            silverbirdMZ7Unlocked = false;
            GameManager.instance.bmkNext.SetActive(false);
        }
        if (PlayerPrefs.GetInt("MZ-7") == 1)
        {
            silverbirdMZ7Unlocked = true;
            GameManager.instance.bmkNext.SetActive(true);
        }
        if (PlayerPrefs.GetInt("4Rings") == 0)
        {
            fourRingsSuperSixUnlocked = false;
            GameManager.instance.SilverBirdNext.SetActive(false);
        }
        if (PlayerPrefs.GetInt("4Rings") == 1)
        {
            fourRingsSuperSixUnlocked = true;
            GameManager.instance.SilverBirdNext.SetActive(true);
        }
    }
}
