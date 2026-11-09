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
    [SerializeField] GameObject copCar;
    [SerializeField] GameObject spawnedCop;
    [SerializeField] ChaseManager chaseManager;

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
    }

    public void setCarIndex(int index)
    {
        carIndex = index;
        selectedCar = carList[carIndex];
    }

    private void Update()
    {
        
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
            if (spawnedCar.transform.position.x > chaseDistanceCounter)
            {
                chaseDistanceCounter += chaseDistanceIntervall;
                chaseManager.currentLevel++;
                chaseDistanceIntervall += 100;
                chaseManager.startChase();
            }
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

    public void spawnCop()
    {
        if(chaseManager.canSpawn == true)
        {
            spawnedCop = Instantiate(copCar, spawnPoint);
            spawnedCop.GetComponent<Police>().chaseManager = chaseManager;
            chaseManager.countOfCops.Add(spawnedCop);
            chaseManager.actualAmount++;
        }
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
}
