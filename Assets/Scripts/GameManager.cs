using System.Collections.Generic;
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

    [Header("Progress")]
    [SerializeField] bool unlockedBMKK2;
    [SerializeField] int BMKK2;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        selectedCar = carList[Random.Range(0,carList.Count-1)];
        BMKK2 = 0;
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

    public void setCameraTarget()
    {
        vCam.Target.TrackingTarget = spawnedCar.transform;
        //camera.GetComponent<CameraFollow>().target = spawnedCar.transform;
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
