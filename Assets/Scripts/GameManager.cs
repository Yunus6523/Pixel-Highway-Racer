using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Gameplay")]
    [SerializeField] public bool isPaused;
    [SerializeField] SceneLoader scene;
    [SerializeField] GameObject pauseScreen;

    [Header("Camera")]
    public CinemachineCamera vCam;
    public Camera camera;

    [Header("Car Ui")]
    public Transform uiSpawnPoint;

    [Header("Cars")]
    public GameObject pandaCivilTypeR;
    public GameObject pandaNext;
    public GameObject bmkK2;
    public GameObject bmkNext;
    public GameObject fourRingsSuperFour;
    public Transform spawnPoint;

    [Header("Modular Car Spawn")]
    public List<GameObject> carList;
    public GameObject selectedCar;
    public GameObject spawnedCar;
    public int carIndex;

    [Header("Chase System")]
    [SerializeField] public GameObject copCar;
    [SerializeField] GameObject spawnedCop;
    [SerializeField] ChaseManager chaseManager;
    public Transform copSpawnPoint;
    public Transform copSpawnPointTwo;
    public Transform copSpawnPointThree;
    public copSpawnPointRef spawnPointRef;


    [Header("Rivalry")]
    public List<GameObject> rivalCarList;
    public GameObject currentRival;
    public bool isInRivalRace;
    public bool yurikoBeaten;
    public float rivalryTimer;
    public bool rivalryTimerOngoing;
    

    [Header("Highscore")]
    public TextMeshProUGUI counter;
    public float count;
    public float distanceHighscoreIntervall;
    public float currentDistanceCounter;

    [Header("Chase System")]
    public GameObject gameOverScreen;
    public float chaseDistanceIntervall;
    public float chaseDistanceCounter;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameOverScreen.SetActive(false);
        counter.SetText(count.ToString());

        pandaCivilTypeR.SetActive(true);
        bmkK2.SetActive(false);
        fourRingsSuperFour.SetActive(false);

        isPaused = false;
        pauseScreen.SetActive(false);

        pandaNext.SetActive(false);
        bmkNext.SetActive(false);

        currentRival = rivalCarList[0];
    }

    public void setCarIndex(int index)
    {
        carIndex = index;
        selectedCar = carList[carIndex];
    }

    private void Update()
    {
        rivalTimer();

        copSpawnPoint = spawnPointRef.spawnOne;
        copSpawnPointTwo = spawnPointRef.spawnTwo;
        copSpawnPointThree = spawnPointRef.spawnThree;

        updateHighScoreCounter();

        updateChaseSystem();

        progress();
    }

    void updateHighScoreCounter()
    {
        if(spawnedCar.transform.position.x > currentDistanceCounter)
        {
            currentDistanceCounter += distanceHighscoreIntervall;
            count++;
            counter.SetText(count.ToString());
        }
    }

    void updateChaseSystem()
    {
        if(chaseManager.levelCapped == false)
        {
            chaseManager.startChase();
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

    public void progress()
    {
        PlayerPrefs.SetFloat("Highscore", count);
    }

    public void gameOver()
    {
        Destroy(spawnedCar);
        gameOverScreen.SetActive(true);
        chaseManager.destroyAllCruisers();
        Time.timeScale = 0.5f;
    }

    public void endGame()
    {
        scene.loadScene();
    }

    public void togglePause()
    {
        isPaused = ! isPaused;
        if(isPaused == true)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
        if(isPaused == false)
        {
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
        }
    }

    public void destroyRival()
    {
        //Debug
        Destroy(currentRival);
    }

    public void rivalTimer()
    {
        if(isInRivalRace == true && rivalryTimerOngoing == true)
        {
            rivalryTimer++;
            if(rivalryTimer >= 500f)
            {
                destroyRival();
                isInRivalRace = false;
            }
        }
        if(isInRivalRace == false || rivalryTimerOngoing == false)
        {
            rivalryTimer = 0;
        }
    }
}
