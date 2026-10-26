using System.Collections;
using System.Collections.Generic;
using Unity.Tutorials.Editor;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

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

    // Update is called once per frame
    void Update()
    {
        if (chase == true)
        {
            canSpawn = true;
            if (actualAmount <= spawnableAmount)
            {
                GameManager.instance.spawnCop();
            }
            else if (actualAmount >= spawnableAmount)
            {
                canSpawn = false;
            }
        }

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

    }

    public void startChase()
    {
        chase = true;
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
}
