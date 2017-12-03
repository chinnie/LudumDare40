using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;    //Singleton instance reference

    public Transform player;                    //Reference to the player's body target
    public GM_SpawnModule customerSpawner;      //Reference to the pengu spawner
    public GM_DanceModule dancerSpawner;        //Reference to the module that manages the player's advertiser
    public GM_LandlordModule landlordSpawner;   //Reference to the module that manages the landlord

    [Header("Currencies")]
    [SerializeField] int greatFishcount;
    [SerializeField] int goodFishcount;
    [SerializeField] int badFishcount;
    [SerializeField] int rottenFishcount;

    [Header("Fish State Prefabs")]
    [SerializeField] FishContainer[] Fish = new FishContainer[4];

    private int[] fishcounts = new int[4];
    private int[] oldfishcounts = new int[4];
        
    GameObject greatFishStack;

    int Fishpoints;
    bool fishchanged;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            InitializeReferences();
        }
        //If this is not the first Game Manager then we destroy this Game Manager
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        InitializeStacks();
    }

    // Use this for initialization
    void Start()
    {
        Fishpoints = 0;

        fishcounts[0] = greatFishcount;
        fishcounts[1] = goodFishcount;
        fishcounts[2] = badFishcount;
        fishcounts[3] = rottenFishcount;
        
        for (int i = 0; i < 4; i++)
        {           
            oldfishcounts[i] = 0;
        }

    }

    private void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (oldfishcounts[i] != fishcounts[i])
            {
                AdjustCounts(i);
            }
        }
    }

    void InitializeReferences()
    {
        if (greatFishStack == null)
        {
            greatFishStack = Instantiate(Fish[0].FishPrefab[0]) as GameObject;
            greatFishStack.transform.position = Fish[0].FishPos.position;
        }
    }

    void AdjustCounts(int index)
    {
        int prefabindex = 0;
        if (fishcounts[index] == 0) prefabindex = 0;
        if ((fishcounts[index] >= 1) && (fishcounts[index] <=3)) prefabindex = 1;
        if ((fishcounts[index] >=4) && (fishcounts[index] <= 7)) prefabindex = 2;
        if (fishcounts[index] >=8) prefabindex = 3;
        
        Destroy(greatFishStack);
        greatFishStack = Instantiate(Fish[index].FishPrefab[prefabindex]) as GameObject;
        greatFishStack.transform.position = Fish[index].FishPos.position;
    }

    void InitializeStacks()
    {
        Debug.Log("Inisializing Stacks of Fish");
        if (customerSpawner == null) customerSpawner = GetComponent<GM_SpawnModule>();

        if (dancerSpawner == null) dancerSpawner = GetComponent<GM_DanceModule>();

        if (landlordSpawner == null) landlordSpawner = GetComponent<GM_LandlordModule>();
    }
    
    public void AddPoints(CurrencyFish fish)
    {
        Fishpoints += fish.quality;

    } 
}