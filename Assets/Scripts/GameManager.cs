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

    [Header("Music")]
    public GameObject musicUi;
    public TextMeshProUGUI title;
    public TextMeshProUGUI artist;

    [Header("Cars")]
    public GameObject normal;
    public GameObject pandaCivilTypeR;
    public GameObject pandaNext;
    public GameObject bmkK2;
    public GameObject bmkNext;
    public GameObject SilverBird;
    public GameObject SilverBirdNext;
    public GameObject FourRings;
    public GameObject fourRingsNext;
    public GameObject gearSan;
    public GameObject switchToRival;
    public Transform spawnPoint;

    [Header("Rival Cars")]
    public GameObject switchToNormal;
    public GameObject rivals;
    public GameObject yurikoMZ5;
    public GameObject yurikoMZ5Next;
    public GameObject gutierrezFytpe;
    public GameObject gutierrezFypeNext;
    public GameObject aronSLS;
    public GameObject aronSLSNext;
    public GameObject amberPanini;

    [Header("Modular Car Spawn")]
    public List<GameObject> carList;
    public GameObject selectedCar;
    public GameObject spawnedCar;
    public int carIndex;

    [Header("Chase System")]
    [SerializeField] ChaseManager chaseManager;
    public Transform copSpawnPoint;
    public Transform copSpawnPointTwo;
    public Transform copSpawnPointThree;
    public GameObject gameOverScreen;
    public float chaseDistanceIntervall;
    public float chaseDistanceCounter;
    public GameObject wantedLVL1;
    public GameObject wantedLVL2;
    public GameObject wantedLVL3;
    public GameObject wantedLVL4;
    public GameObject wantedLVL5;


    [Header("Rivalry")]
    public List<GameObject> rivalCarList;
    public GameObject currentRival;
    public Transform rivalSpawn;
    public int rivalIndex;
    public bool isInRivalRace;
    public bool yurikoBeaten;
    public bool gutierrezBeaten;
    public bool aronBeaten;
    public bool amberBeaten;
    public float rivalryTimer;
    public bool rivalryTimerOngoing;
    public bool rivalCanSpawn;
    public int spawnedRival;
    public int maxSpawnedRival;

    [Header("Encounter!")]
    public GameObject yurikoEncounter;
    public GameObject gutierrezEncounter;
    public GameObject aronEncounter;
    public GameObject amberEncounter;
    

    [Header("Highscore")]
    public TextMeshProUGUI counter;
    public float count;
    public float distanceHighscoreIntervall;
    public float currentDistanceCounter;

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
        SilverBird.SetActive(false);
        FourRings.SetActive(false);
        gearSan.SetActive(false);

        yurikoMZ5.SetActive(false);
        gutierrezFytpe.SetActive(false);
        aronSLS.SetActive(false);
        amberPanini.SetActive(false);

        switchToRival.SetActive(false);

        isPaused = false;
        pauseScreen.SetActive(false);

        pandaNext.SetActive(false);
        bmkNext.SetActive(false);

        musicUi.SetActive(false);

        yurikoBeaten = false;
        gutierrezBeaten = false;
        aronBeaten = false;
        amberBeaten = false;

        yurikoEncounter.SetActive(false);
        gutierrezEncounter.SetActive(false);
        aronEncounter.SetActive(false);
        amberEncounter.SetActive(false);
    }

    public void setCarIndex(int index)
    {
        carIndex = index;
        selectedCar = carList[carIndex];
    }

    private void Update()
    {
        rivalTimer();

        updateHighScoreCounter();

        updateChaseSystem();

        progress();

        currentRival = rivalCarList[rivalIndex];
        
        if (yurikoBeaten == true)
        {
            PlayerPrefs.SetInt("MZ-5", 1);
        }
        if(gutierrezBeaten == true)
        {
            PlayerPrefs.SetInt("Ftype", 1);
        }
        if (aronBeaten == true)
        {
            PlayerPrefs.SetInt("SLS", 1);
        }
        if(amberBeaten == true)
        {
            PlayerPrefs.SetInt("Panini", 1);
        }

        if(count >= 10)
        {
            switch (rivalIndex)
            {
                case 0:
                    yurikoEncounter.SetActive(true);
                    StartCoroutine(encounter());
                    break;
                case 1:
                    gutierrezEncounter.SetActive(true);
                    StartCoroutine(encounter());
                    break;
                case 2:
                    aronEncounter.SetActive(true);
                    StartCoroutine(encounter());
                    break;
                case 3:
                    amberEncounter.SetActive(true);
                    StartCoroutine(encounter());
                    break;
            }
        }
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
            if(spawnedCar.transform.position.x > chaseDistanceCounter)
            {
                chaseDistanceCounter += chaseDistanceIntervall;
                if(chaseManager.chase == false)
                {
                    chaseManager.startChase();
                }
                if(chaseManager.chase == true)
                {
                    chaseManager.currentLevel++;
                }
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

    public IEnumerator turnOffMusicUi()
    {
        yield return new WaitForSeconds(20f);
        musicUi.SetActive(false);
    }

    public IEnumerator encounter()
    {
        yield return new WaitForSeconds(5);

        yurikoEncounter.SetActive(false);
        gutierrezEncounter.SetActive(false);
        aronEncounter.SetActive(false);
        amberEncounter.SetActive(false);

        maxSpawnedRival = 0;

        spawnRival();
    }

    public void spawnRival()
    {
        if(maxSpawnedRival == 0)
        {
            Instantiate(currentRival, rivalSpawn);
            maxSpawnedRival++;
        }
    }
}
