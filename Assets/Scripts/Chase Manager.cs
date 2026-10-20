using System.Collections;
using Unity.Tutorials.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class ChaseManager : MonoBehaviour
{
    [Header("Police")]
    public Police police;
    public bool chase;
    public bool canSpawn;
    public float spawnableAmount;
    public float actualAmount;

    [Header("Arrest")]
    [SerializeField]public float arrestTime;

    [Header("Wanted Level")]
    [SerializeField] public int currentLevel;
    [SerializeField] public int maxLevel;
    [SerializeField] public bool levelCapped;

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
       if(chase == true)
        {
            canSpawn = true;
            if (actualAmount <= spawnableAmount)
            {
                canSpawn = true;
                if(canSpawn == true)
                {
                    Debug.Log("is spawning");
                    GameManager.instance.spawnCop();
                }
            }
            else if(actualAmount > spawnableAmount)
            {
                Debug.Log("is not spawning");
                canSpawn = false;
            }
        }
    }

    public void startChase()
    {
        chase = true;
    }

    private void FixedUpdate()
    {
        if (police.distance.x <= -1)
        {
            StartCoroutine(initiateArrest());
            Debug.Log("Starting Arrest!");
        }
    }

    IEnumerator initiateArrest()
    {
        yield return new WaitForSeconds(arrestTime);
        GameManager.instance.gameOver();
    }
}
