using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Camera")]
    public CinemachineCamera vCam;

    [Header("Sample Car Spawn")]
    //public GameObject pandaCivilTypeR;
    public Transform spawnPoint;

    [Header("Modular Car Spawn")]
    public List<GameObject> carList;
    public GameObject selectedCar;
    public GameObject spawnedCar;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        selectedCar = carList[Random.Range(0,carList.Count-1)];
    }

    public void setCameraTarget()
    {
        vCam.Target.TrackingTarget = spawnedCar.transform;
    }

    public void spawnSampleCar()
    {
        spawnedCar = Instantiate(selectedCar, spawnPoint);
    }
}
