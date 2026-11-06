using Unity.VisualScripting;
using UnityEngine;

public class WorldChunk : MonoBehaviour
{
    [Header("NPC Car Prefab")]
    [SerializeField] GameObject[] carList;
    [SerializeField] int carIndex;

    [Header("Spawn Transform")]
    [SerializeField] Transform spawnPointOne;
    [SerializeField] Transform spawnPointTwo;
    [SerializeField] Transform spawnPointThree;

    [Header("Randomizer")]
    [SerializeField] int randomOne;
    [SerializeField] int randomTwo;
    [SerializeField] int randomThree;

    private void Start()
    {
        randomOne = Random.Range(0, 2);
        randomTwo = Random.Range(0, 11);
        randomThree = Random.Range(0, 11);

        spawnCarAtOne();
        spawnCarAtTwo();
        spawnCarAtThree();
    }

    void spawnCarAtOne()
    {
        if(randomOne == 1)
        {
            carIndex = Random.Range(0, 2);
            Instantiate(carList[carIndex], spawnPointOne);
        }
    }

    void spawnCarAtTwo()
    {
        if(randomTwo == 1)
        {
            carIndex = Random.Range(0, 2);
            Instantiate(carList[carIndex], spawnPointTwo);
        } 
    }

    void spawnCarAtThree()
    {
        if(randomThree == 1)
        {
            carIndex = Random.Range(0, 2);
            Instantiate(carList[carIndex], spawnPointThree);
        }
    }
}
