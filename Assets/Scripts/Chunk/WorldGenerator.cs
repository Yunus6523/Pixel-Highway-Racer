using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform player;
    [SerializeField] GameObject[] chunkPrefabs;

    [Header("Spawning")]
    [SerializeField] float chunkWidth = 10f;
    [SerializeField] float spawnAheadDistance = 20f;
    [SerializeField] float despawnDistance = 20f;

    [Header("Starting Chunks")]
    [SerializeField] GameObject[] startingChunks;
    [SerializeField] float firstChunkOffset = 10f;

    float nextSpawnX;
    readonly List<GameObject> spawnedChunks = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextSpawnX = transform.position.x + firstChunkOffset;
        spawnedChunks.AddRange(startingChunks);
    }

    // Update is called once per frame
    void Update()
    {
        player = GameManager.instance.spawnedCar.transform;
        if(nextSpawnX - player.position.x < spawnAheadDistance)
        {
            spawn();
        }
        despawnChunksBehindPlayer();
    }

    void spawn()
    {
        GameObject prefab = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
        GameObject chunk = Instantiate(prefab, new Vector3(nextSpawnX, 0f, 0f), Quaternion.identity);
        chunk.transform.SetParent(transform);
        spawnedChunks.Add(chunk);

        nextSpawnX += chunkWidth;
    }

    void despawnChunksBehindPlayer()
    {
        for(int i = spawnedChunks.Count - 1; i >= 0; --i)
        {
            GameObject chunk = spawnedChunks[i];

            if(player.position.x - chunk.transform.position.x > despawnDistance)
            {
                spawnedChunks.RemoveAt(i);
                Destroy(chunk);
            }
        }
    }
}
