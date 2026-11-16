using Unity.VisualScripting;
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

    //Gutierrez F Type
    [SerializeField] bool gutierrezFTypeUnlocked;
    [SerializeField] int gutierrezFTypeIndex;

    //Aron SLS
    [SerializeField] bool aronSLSUnlocked;
    [SerializeField] int aronSLSIndex;

    //Amber Panini
    [SerializeField] bool amberPaniniUnlocked;
    [SerializeField] int amberPaniniIndex;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPrefs.SetInt("BMK", BMKIndex);
        PlayerPrefs.SetInt("MZ-7", silverbirdMZ7Index);
        PlayerPrefs.SetInt("4Rings", fourRingsSuperSixIndex);
        PlayerPrefs.SetInt("Gear-San", gearSanGizmoGTRIndex);

        PlayerPrefs.SetInt("MZ-5", yurikosMZ5Index);
        PlayerPrefs.SetInt("Ftype", gutierrezFTypeIndex);
        PlayerPrefs.SetInt("SLS", aronSLSIndex);
        PlayerPrefs.SetInt("Panini", amberPaniniIndex);


    }

    // Update is called once per frame
    void Update()
    {
        //Normal Cars
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
        if(PlayerPrefs.GetInt("Gear-San") == 0)
        {
            gearSanGizmoGTRUnlocked = false;
            GameManager.instance.fourRingsNext.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Gear-San") == 1)
        {
            gearSanGizmoGTRUnlocked = true;
            GameManager.instance.fourRingsNext.SetActive(true);
        }

        //Rival Cars
        if(PlayerPrefs.GetInt("MZ-5") == 0)
        {
            GameManager.instance.switchToRival.SetActive(false);
            GameManager.instance.rivalIndex = 0;
        }
        if (PlayerPrefs.GetInt("MZ-5") == 1)
        {
            GameManager.instance.switchToRival.SetActive(true);
            GameManager.instance.rivalIndex = 1;
        }
        if (PlayerPrefs.GetInt("Ftype") == 0)
        {
            GameManager.instance.yurikoMZ5Next.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Ftype") == 1)
        {
            GameManager.instance.yurikoMZ5Next.SetActive(true);
            GameManager.instance.rivalIndex = 2;
        }
        if (PlayerPrefs.GetInt("SLS") == 0)
        {
            GameManager.instance.gutierrezFypeNext.SetActive(false);
        }
        if (PlayerPrefs.GetInt("SLS") == 1)
        {
            GameManager.instance.gutierrezFypeNext.SetActive(true);
            GameManager.instance.rivalIndex = 3;
        }
        if (PlayerPrefs.GetInt("Panini") == 0)
        {
            GameManager.instance.aronSLSNext.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Panini") == 1)
        {
            GameManager.instance.aronSLSNext.SetActive(true);
        }
    }
}
