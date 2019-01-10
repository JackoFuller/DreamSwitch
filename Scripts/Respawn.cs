using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {
    public GameObject checkpoint;
	


    //Checkpoints are set in each level so the player doesn't have to start from the beginning.
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            other.transform.position = checkpoint.transform.position;        
                
        }
        

    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Player")
        {
            other.transform.position = checkpoint.transform.position;

        }
    }


}
