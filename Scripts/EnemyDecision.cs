using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyDecision : MonoBehaviour {
    public Transform[] patrolPoints;
    private int destinationPoint = 0;
    public float speed;
    
	// Use this for initialization
	void Start () {
        GoToNextPoint();

        
	}



    void GoToNextPoint()
    {
        if(patrolPoints.Length == 0)
        {
            return;

        }

        
      
        destinationPoint = (destinationPoint + 1) % patrolPoints.Length;
        Debug.Log(Vector3.Distance(transform.position, patrolPoints[destinationPoint].position));
        Debug.Log(destinationPoint);
    }


   //Basic travel script for an enemy, moves back and forth between defined points.
    void Update () {
		if(Vector3.Distance(transform.position,patrolPoints[destinationPoint].position) < 1f)
        {

            GoToNextPoint();
            
        }
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[destinationPoint].position, step);
 
    }

   


}
