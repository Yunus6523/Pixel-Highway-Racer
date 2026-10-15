using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Camera")]
    public CinemachineCamera vCam;
    public Camera camera;

    [Header("Sample Car Spawn")]
    //public GameObject pandaCivilTypeR;
    public Transform spawnPoint;

    [Header("Modular Car Spawn")]
    public List<GameObject> carList;
    public GameObject selectedCar;
    public GameObject spawnedCar;
    public int carIndex;

    [Header("Progress")]
    [SerializeField] bool unlockedBMKK2;
    [SerializeField] int BMKK2;

    [Header("Highscore")]
    public TextMeshProUGUI counter;
    public int count;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        counter.SetText(count.ToString());
        BMKK2 = 0;
    }

    public void setCarIndex(int index)
    {
        carIndex = index;
        selectedCar = carList[carIndex];
    }

    private void Update()
    {
        BMKK2 = PlayerPrefs.GetInt("BMK K2");
        if (BMKK2 == 1)
        {
            Debug.Log("BMK K2 is unlocked");
        }
        else if (BMKK2 < 1)
        {
            Debug.Log("BMK K2 is locked");
        }
    }

    private void FixedUpdate()
    {

    }

    public void setCameraTarget()
    {
        //vCam.Target.TrackingTarget = spawnedCar.transform;
        camera.GetComponent<CameraFollow>().target = spawnedCar.transform;
    }

    public void spawnSampleCar()
    {
        spawnedCar = Instantiate(selectedCar, spawnPoint);
    }

    public void toggleUnlockable()
    {
        unlockedBMKK2 =! unlockedBMKK2;
    }

    public void progress()
    {
        if(unlockedBMKK2 == true)
        {
            PlayerPrefs.SetInt("BMK K2", 1);
        }
    }
}
