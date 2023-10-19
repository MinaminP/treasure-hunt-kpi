using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RandomSpawnTreasure : NetworkBehaviour
{
    public GameObject TreasureObject;

    public GameObject treasure1;
    public GameObject treasure2;
    public GameObject treasure3;
    public GameObject treasure4;
    public GameObject treasure5;
    public GameObject treasure6;
    public GameObject treasure7;   
    public GameObject treasure8;
    public GameObject treasure9;
    public GameObject treasure10;

    [SyncVar(hook = nameof(OnRandomChanged))]
    public int random = 0;

    [SyncVar(hook = nameof(OnTempRandomChanged))]
    public int tempRandom = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Command(requiresAuthority = false)]
    public void RandomSpawn()
    {
        random = Random.Range(1, 11);

        if (random == tempRandom)
        {
            //RandomSpawn();
            random = Random.Range(1, 11);
        }
        //random = 2;
        if(random == 1)
        {
            //Instantiate(TreasureObject, treasure1.position, treasure1.rotation);
            //NetworkServer.spa
            treasure1.GetComponent<interact>().isActive = true;
            treasure2.GetComponent<interact>().isActive = false;
            treasure3.GetComponent<interact>().isActive = false;
            treasure4.GetComponent<interact>().isActive = false;
            treasure5.GetComponent<interact>().isActive = false;
            treasure6.GetComponent<interact>().isActive = false;
            treasure7.GetComponent<interact>().isActive = false;
            treasure8.GetComponent<interact>().isActive = false;
            treasure9.GetComponent<interact>().isActive = false;
            treasure10.GetComponent<interact>().isActive = false;
        }
        else if (random == 2)
        {
            treasure1.GetComponent<interact>().isActive = false;
            treasure2.GetComponent<interact>().isActive = true;
            treasure3.GetComponent<interact>().isActive = false;
            treasure4.GetComponent<interact>().isActive = false;
            treasure5.GetComponent<interact>().isActive = false;
            treasure6.GetComponent<interact>().isActive = false;
            treasure7.GetComponent<interact>().isActive = false;
            treasure8.GetComponent<interact>().isActive = false;
            treasure9.GetComponent<interact>().isActive = false;
            treasure10.GetComponent<interact>().isActive = false;
        }
        else if (random == 3)
        {
            treasure1.GetComponent<interact>().isActive = false;
            treasure2.GetComponent<interact>().isActive = false;
            treasure3.GetComponent<interact>().isActive = true;
            treasure4.GetComponent<interact>().isActive = false;
            treasure5.GetComponent<interact>().isActive = false;
            treasure6.GetComponent<interact>().isActive = false;
            treasure7.GetComponent<interact>().isActive = false;
            treasure8.GetComponent<interact>().isActive = false;
            treasure9.GetComponent<interact>().isActive = false;
            treasure10.GetComponent<interact>().isActive = false;
        }
        else if (random == 4)
        {
            treasure1.GetComponent<interact>().isActive = false;
            treasure2.GetComponent<interact>().isActive = false;
            treasure3.GetComponent<interact>().isActive = false;
            treasure4.GetComponent<interact>().isActive = true;
            treasure5.GetComponent<interact>().isActive = false;
            treasure6.GetComponent<interact>().isActive = false;
            treasure7.GetComponent<interact>().isActive = false;
            treasure8.GetComponent<interact>().isActive = false;
            treasure9.GetComponent<interact>().isActive = false;
            treasure10.GetComponent<interact>().isActive = false;
        }
        else if (random == 5)
        {
            treasure1.GetComponent<interact>().isActive = false;
            treasure2.GetComponent<interact>().isActive = false;
            treasure3.GetComponent<interact>().isActive = false;
            treasure4.GetComponent<interact>().isActive = false;
            treasure5.GetComponent<interact>().isActive = true;
            treasure6.GetComponent<interact>().isActive = false;
            treasure7.GetComponent<interact>().isActive = false;
            treasure8.GetComponent<interact>().isActive = false;
            treasure9.GetComponent<interact>().isActive = false;
            treasure10.GetComponent<interact>().isActive = false;
        }
        else if (random == 6)
        {
            treasure1.GetComponent<interact>().isActive = false;
            treasure2.GetComponent<interact>().isActive = false;
            treasure3.GetComponent<interact>().isActive = false;
            treasure4.GetComponent<interact>().isActive = false;
            treasure5.GetComponent<interact>().isActive = false;
            treasure6.GetComponent<interact>().isActive = true;
            treasure7.GetComponent<interact>().isActive = false;
            treasure8.GetComponent<interact>().isActive = false;
            treasure9.GetComponent<interact>().isActive = false;
            treasure10.GetComponent<interact>().isActive = false;
        }
        else if (random == 7)
        {
            treasure1.GetComponent<interact>().isActive = false;
            treasure2.GetComponent<interact>().isActive = false;
            treasure3.GetComponent<interact>().isActive = false;
            treasure4.GetComponent<interact>().isActive = false;
            treasure5.GetComponent<interact>().isActive = false;
            treasure6.GetComponent<interact>().isActive = false;
            treasure7.GetComponent<interact>().isActive = true;
            treasure8.GetComponent<interact>().isActive = false;
            treasure9.GetComponent<interact>().isActive = false;
            treasure10.GetComponent<interact>().isActive = false;
        }
        else if (random == 8)
        {
            treasure1.GetComponent<interact>().isActive = false;
            treasure2.GetComponent<interact>().isActive = false;
            treasure3.GetComponent<interact>().isActive = false;
            treasure4.GetComponent<interact>().isActive = false;
            treasure5.GetComponent<interact>().isActive = false;
            treasure6.GetComponent<interact>().isActive = false;
            treasure7.GetComponent<interact>().isActive = false;
            treasure8.GetComponent<interact>().isActive = true;
            treasure9.GetComponent<interact>().isActive = false;
            treasure10.GetComponent<interact>().isActive = false;
        }
        else if (random == 9)
        {
            treasure1.GetComponent<interact>().isActive = false;
            treasure2.GetComponent<interact>().isActive = false;
            treasure3.GetComponent<interact>().isActive = false;
            treasure4.GetComponent<interact>().isActive = false;
            treasure5.GetComponent<interact>().isActive = false;
            treasure6.GetComponent<interact>().isActive = false;
            treasure7.GetComponent<interact>().isActive = false;
            treasure8.GetComponent<interact>().isActive = false;
            treasure9.GetComponent<interact>().isActive = true;
            treasure10.GetComponent<interact>().isActive = false;
        }
        else if (random == 10)
        {
            treasure1.GetComponent<interact>().isActive = false;
            treasure2.GetComponent<interact>().isActive = false;
            treasure3.GetComponent<interact>().isActive = false;
            treasure4.GetComponent<interact>().isActive = false;
            treasure5.GetComponent<interact>().isActive = false;
            treasure6.GetComponent<interact>().isActive = false;
            treasure7.GetComponent<interact>().isActive = false;
            treasure8.GetComponent<interact>().isActive = false;
            treasure9.GetComponent<interact>().isActive = false;
            treasure10.GetComponent<interact>().isActive = true;
        }

        tempRandom = random;
    }

    public void OnRandomChanged(int oldRandom, int newRandom)
    {

    }

    public void OnTempRandomChanged(int oldRandom, int newRandom)
    {

    }
}
