using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadPress : MonoBehaviour {
    public GameObject climb;
    
	
    //A check for the enemy to activate blocks if they hit a pressure pad.
    void OnCollisionStay(Collision other)
    {
        if(other.gameObject.name == "Enemy")
        {
            climb.SetActive(true);

        }

    }


    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name == "Enemy")
        {
            climb.SetActive(false);

        }

    }

}
