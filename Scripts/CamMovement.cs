using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CamMovement : MonoBehaviour {

    PlayerController player;
    GameObject controller;
    GameObject camFollow;
    new GameObject camera;
    float camSpeed = 0.1f;
    public List<GameObject> cameraPositions;
    public int nextPosition = 0;
    float startTime;
    
    //Identify player and camera point locations.
    void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        cameraPositions.Add(GameObject.Find("CamPos"));
        cameraPositions.Add(GameObject.Find("CamPos2"));
        cameraPositions.Add(GameObject.Find("CamPos3"));
        cameraPositions.Add(GameObject.Find("CamPos4"));
        startTime = Time.time;
    }
	

    //When the player enters a new area, the camera will travel to a new camera position.
    public void MoveTo()
    {
        float travelDist = Vector3.Distance(transform.position,cameraPositions[nextPosition].transform.position);
        float distCovered = (Time.time - startTime) * camSpeed;
        float t = distCovered / travelDist;
        Debug.Log("YES");

        transform.position = Vector3.Lerp(transform.position, cameraPositions[nextPosition].transform.position,t);
        if(transform.position == cameraPositions[nextPosition].transform.position)
        {
            player.nextLevel = false;
            nextPosition++;

        }
    }
 


}
