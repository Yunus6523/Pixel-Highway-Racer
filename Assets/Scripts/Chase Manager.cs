using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseManager : MonoBehaviour
{
    [Header("Police")]
    public bool chase;
    public bool canSpawn;
    public float spawnableAmount;
    public float actualAmount;
    public List<GameObject> countOfCops;

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
        spawnableAmount = 2;
        actualAmount = 0;

        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameManager.instance.spawnedCar;

        bool shouldArrest = false;
        foreach (GameObject police in countOfCops)
        {
            float distance = Vector2.Distance(player.transform.position, police.transform.position);
            if (distance < 1)
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

        //switch (currentLevel)
        //{
        //    case 2:
        //        spawnableAmount = 5;
        //        break;
        //    case 3:
        //        spawnableAmount = 8;
        //        break;
        //    case 4:
        //        spawnableAmount = 10;
        //        break;
        //    case 5:
        //        spawnableAmount = 15;
        //        break;
        //}

        if(currentLevel == 5)
        {
            levelCapped = true;
        }

    }

    public void startChase()
    {
        chase = true;
        if(actualAmount <= spawnableAmount)
        {
            //Instantiate(GameManager.instance.copCar, GameManager.instance.copSpawnPoint);
            //Instantiate(GameManager.instance.copCar, GameManager.instance.copSpawnPointTwo);
            //Instantiate(GameManager.instance.copCar, GameManager.instance.copSpawnPointThree);
            actualAmount += 3 ;
            spawnableAmount = 6;
            levelCapped = false;
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
