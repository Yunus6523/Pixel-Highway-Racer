using Unity.VisualScripting;
using UnityEngine;

public class WorldChunk : MonoBehaviour
{
    [Header("NPC Car Prefab")]
    [SerializeField] GameObject carPrefab;

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
        randomOne = Random.Range(0, 5);
        randomTwo = Random.Range(0, 5);
        randomThree = Random.Range(0, 5);

        spawnCarAtOne();
        spawnCarAtTwo();
        spawnCarAtThree();
    }

    void spawnCarAtOne()
    {
        if(randomOne == 1)
        {
            Instantiate(carPrefab, spawnPointOne);
        }
    }

    void spawnCarAtTwo()
    {
        if(randomTwo == 1)
        {
            Instantiate(carPrefab, spawnPointTwo);
        } 
    }

    void spawnCarAtThree()
    {
        if(randomThree == 1)
        {
            Instantiate(carPrefab, spawnPointThree);
        }
    }
}
