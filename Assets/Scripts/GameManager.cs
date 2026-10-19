using System.Collections;
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

    [Header("Enemy Spawn")]
    [SerializeField] GameObject copCar;

    [Header("Progress")]
    [SerializeField] bool unlockedBMKK2;
    [SerializeField] int BMKK2;

    [Header("Highscore")]
    public TextMeshProUGUI counter;
    public float count;
    public float distanceHighscoreIntervall;
    public float currentDistanceCounter;

    [Header("Chase System")]
    public GameObject gameOverScreen;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameOverScreen.SetActive(false);
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

        progress();

        updateHighScoreCounter();
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

        PlayerPrefs.SetFloat("Highscore", count);
    }

    public void spawnCop()
    {
        StartCoroutine(startCopTimer());
    }

    IEnumerator startCopTimer()
    {
        yield return new WaitForSeconds(5f);
        Instantiate(copCar, spawnPoint);
    }

    public void gameOver()
    {
        Destroy(spawnedCar);
        gameOverScreen.SetActive(true);
        Time.timeScale = 0.5f;
    }

    public void endGame()
    {
        Application.Quit();
    }
}
