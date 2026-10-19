using System.Collections;
using UnityEngine;

public class ChaseManager : MonoBehaviour
{
    public Police police;
    public float arrestTime;

    //public float amountOfCops;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(police.distance.x < 1)
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
