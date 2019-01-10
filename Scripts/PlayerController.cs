using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Vector3 moveDir;
    public float gravity = 20f;
    public float moveSpeed = 2.0f;
    public float jumpSpeed = 5f;
    CharacterController controller;
    public CamMovement cameraMove;
    public bool nextLevel = false;
    public bool cannotSwitch = false;
    bool fadeOut = false;
    public Animator anim;
    public List<GameObject> wallOff;
    int wallnumber = 0;
    public Camera ccam;
    public Transform camTrack;
    public Canvas canvas;
    float switchSpeed = 0f;
    public Image panelCol;
    Text textCol;
    public ParticleSystem dustparticle;
    

    void Start()
    {
        controller = GetComponent<CharacterController>();
        panelCol = GameObject.Find("Panel").GetComponent<Image>();
        textCol = GameObject.Find("Panel").GetComponentInChildren<Text>();
    }

    
    void Update()
    {
        Animations();
        Movement();
        //Makes the camera move to the next level.
        if(nextLevel)
        {
            cameraMove.MoveTo();

        }
        //When the player starts the game, fade out the title panel.
        if(fadeOut)
        {
            float t = switchSpeed += 0.1f * Time.deltaTime;
            panelCol.color = new Color(panelCol.color.r, panelCol.color.g, panelCol.color.b, Mathf.Lerp(0, 1, t));
            textCol.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));

        }
        if(panelCol.color.a >=1)
        {
            SceneManager.LoadScene(0);

        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();

        }
    }

    void Movement()
    {
        //Checks if player in on the ground to display movement.
        if (controller.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= moveSpeed;
           if(moveDir.x !=0f)
            {
                dustparticle.Play();

            }

            //Input for letting the player jump.


            if (Input.GetButton("Jump"))
            {
                moveDir.y = jumpSpeed;
            }
        }
       

        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }
    //Collision checks on the player to see if the camera needs to move to another level.
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Next Level")
        {
            Destroy(other.gameObject);
            wallOff[wallnumber].SetActive(true);
            wallnumber++;
            nextLevel = true;
            
            
        }
        if(other.gameObject.name == "Final Move")
        {
            ccam.transform.parent = camTrack;
            ccam.transform.position = camTrack.position;
        }

        if(other.gameObject.tag == "Real World" || other.gameObject.tag == "Dream World")
        {
            cannotSwitch = true;


        }
        if (other.gameObject.name == "FadeoutCheck")
        {
            fadeOut = true;
            
        }


    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Real World" || other.gameObject.tag == "Dream World")
        {
            cannotSwitch = false;


        }

    }





    void Animations()
    {
        anim.SetFloat("Movement", moveDir.x);
        anim.SetBool("isGrounded", controller.isGrounded);

    }


}
