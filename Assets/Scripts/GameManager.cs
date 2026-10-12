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
    public GameObject pandaCivilTypeR;
    public Transform spawnPoint;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
    }

    public void setCameraTarget()
    {
        vCam.Target.TrackingTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void spawnSampleCar()
    {
        Instantiate(pandaCivilTypeR, spawnPoint);
    }
}
