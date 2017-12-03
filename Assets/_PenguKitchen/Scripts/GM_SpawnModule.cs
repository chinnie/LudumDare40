using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_SpawnModule : MonoBehaviour
{

    [SerializeField] bool isEnabled = true;             //Should enemies spawn?
    [SerializeField] float checkRate = .5f;             //The amount of time in between checks to determine if a wave is over

    [Header("Customers")]
    [SerializeField] GameObject customerPrefab;         //The ranged drone prefab
    [SerializeField] Transform[] rangedSpawnPoints;     //The collection of spawning points for ranged drones

    List<Customer> customers;                             //The collection of melee drones in a wave
    WaitForSeconds checkDelay;                          //Delay container

    void Update()
    {
        Debug.Log("Update from spawner");
    }
    void Start()
    {
        customers = new List<Customer>();
        checkDelay = new WaitForSeconds(checkRate);
        StartCoroutine(SpawnCycle());
    }

    IEnumerator SpawnCycle()
    {
        //Initially wait before spawning to give the player time to prepare
        yield return checkDelay;

        while (isEnabled)
        {
            SpawnNewWave();

            do
            {
                yield return checkDelay;
            }
            while (!CheckForEndOfWave());
        }
    }

    void SpawnNewWave()
    {
        //Tell the Game Manager how many waves have spawned so it can update the UI
        //GameStateManager.instance.NewWaveSpawned(++totalWavesSpawned);

        customers.Clear();
                
        //Iterate through the number of customers
        //for (int i = 0; i < waves[currentWave].numberOfMeleeEnemies; i++)
        //{
        //    //Pick a random spawn point
        //    int index = Random.Range(0, meleeSpawnPoints.Length);
        //    //Instantiate the enemy
        //    GameObject obj = Instantiate(meleeEnemy) as GameObject;
        //    //Place the enemy at the spawn point
        //    obj.transform.position = meleeSpawnPoints[index].position;
        //    //Add the enemy to the collection
        //    melee.Add(obj.GetComponent<MeleeDrone>());
        //}
    }

    //This method is called to determine if a wave has been defeated. A value of false will be returned if the wave
    //is still active, while true means that it is complete
    bool CheckForEndOfWave()
    {
        //Loop through the melee enemies, and if a living one is found, return false
        //for (int i = 0; i < melee.Count; i++)
        //    if (melee[i] != null)
        //        return false;

        //If no ranged or melee enemies were found, then the wave is over and return true
        return true;
    }

    public void Stop()
    {
        //Stops the spawning cycle
        isEnabled = false;
    }
}