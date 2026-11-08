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

    [Header("Rivalry Cars")]
    //Yuriko's MZ-5
    [SerializeField] bool yurikosMZ5Unlocked;
    [SerializeField] int yurikosMZ5Index;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BMKIndex = PlayerPrefs.GetInt("BMK");
        PlayerPrefs.SetInt("BMK", BMKIndex);
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
        }
    }
}
