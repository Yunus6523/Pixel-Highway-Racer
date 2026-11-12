using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseManager : MonoBehaviour
{
    [Header("Police")]
    public bool chase;
    public bool canSpawn;
    public int spawnableAmount;
    public int actualAmount;
    public List<GameObject> countOfCops;
    public GameObject cop;

    [Header("Arrest")]
    [SerializeField] public float arrestTime;
    [SerializeField] bool isBeingArrested;
    Coroutine arrestCoroutine;

    [Header("Wanted Level")]
    [SerializeField] public int currentLevel;
    [SerializeField] public int maxLevel;
    [SerializeField] public bool levelCapped;

    [Header("Spawn Points")]
    [SerializeField] WorldGenerator worldGenerator;

    [SerializeField] GameObject player;

    //public float amountOfCops;
    void Start()
    {
        currentLevel = 0;
        maxLevel = 5;
        chase = false;
        spawnableAmount = 3;
        actualAmount = 0;
    }

    public void startChase()
    {
        chase = true;
        canSpawn = true;
        if (currentLevel == 0)
        {
            spawnCops();
        }
    }

    public void spawnCops()
    {
        if(actualAmount >= spawnableAmount)
        {
            canSpawn = false;
        }
        else if (actualAmount < spawnableAmount)
        {
            if (canSpawn == true)
            {
                if (actualAmount == spawnableAmount - 1)
                {
                    Instantiate(cop, GameManager.instance.copSpawnPoint);
                    actualAmount++;
                    canSpawn = false;
                }
                if (actualAmount == spawnableAmount - 2)
                {
                    Instantiate(cop, GameManager.instance.copSpawnPoint);
                    actualAmount++;
                    Instantiate(cop, GameManager.instance.copSpawnPointTwo);
                    actualAmount++;
                    canSpawn = false;
                }
                if (actualAmount == spawnableAmount - 3)
                {
                    Instantiate(cop, GameManager.instance.copSpawnPoint);
                    actualAmount++;
                    Instantiate(cop, GameManager.instance.copSpawnPointTwo);
                    actualAmount++;
                    Instantiate(cop, GameManager.instance.copSpawnPointThree);
                    actualAmount++;
                    canSpawn = false;
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        player = GameManager.instance.spawnedCar;

        bool shouldArrest = false;
        foreach (GameObject police in countOfCops)
        {
            float distance = Vector2.Distance(player.transform.position, police.transform.position);
            if (distance < 4)
            {
                shouldArrest = true;
                arrest();
                break;
            }
        }
        if (shouldArrest == false)
        {
            cancelArrest();
        }

        switch (currentLevel)
        {
            case 2:
                spawnableAmount = 6;
                canSpawn = true;
                spawnCops();
                break;
            case 3:
                spawnableAmount = 9;
                canSpawn = true;
                spawnCops();
                break;
            case 4:
                spawnableAmount = 12;
                canSpawn = true;
                spawnCops();
                break;
            case 5:
                spawnableAmount = 15;
                canSpawn = true;
                spawnCops();
                break;
        }

        if (currentLevel == 5)
        {
            levelCapped = true;
        }

    }


    private void FixedUpdate()
    {

    }

    public void arrest()
    {
        if (isBeingArrested == false)
        {
            Debug.Log("Starting Arrest");
            arrestCoroutine = StartCoroutine(initiateArrest());
            isBeingArrested = true;
        }
    }

    public void cancelArrest()
    {
        if (isBeingArrested == true)
        {
            Debug.Log("Caneling Arrest");
            StopCoroutine(arrestCoroutine);
            isBeingArrested = false;
        }
    }

    IEnumerator initiateArrest()
    {
        yield return new WaitForSeconds(arrestTime);
        Debug.Log("Suspect Arrested");
        GameManager.instance.gameOver();
    }

    public void destroyAllCruisers()
    {
        foreach(GameObject police in countOfCops)
        {
            Destroy(police);
            countOfCops.Clear();
        }
    }
}
