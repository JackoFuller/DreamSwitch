using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    public bool realWorldSwitch;
    public bool dreamWorldSwitch;

    //Reference between the worlds
    GameObject[] realWorldObjects;
    GameObject[] dreamWorldObjects;
    Collider[] dreamColliders;
    Collider[] realColliders;
    ParticleSystem[] dreamParticle;
    ParticleSystem[] realParticle;
    public Material dreamMat;
    public Material realMat;
    public Material worldMat;
    string tag1 = "Dream World";
    string tag2 = "Real World";

    EnemyDecision enemy;
    //All other references involved
    PlayerController player;
    float fadeOverTime = 0.1f;
    float switchSpeed = 0f;
    public float switchTime;
    float menuFade = 0f;
    float offSetPoint;
    Color colorPoint;
    public List<SpriteRenderer> lightColour;
    bool spritefade = false;
    public Texture closedEye;
    public Texture openEye;
    public Material playerTexture;
    public Material sky;
    public Sprite image2;
    public Image mainImage;
    public Canvas canvas;
    float timeHolder;
    public AudioSource switchSound;
    AudioSource music;
 

        void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        player.cannotSwitch = true;
        realWorldSwitch = true;

    }

    void Start()
    {

        timeHolder = Time.deltaTime;
        enemy = GameObject.Find("Enemy").GetComponent<EnemyDecision>();
        music = GameObject.Find("Music").GetComponent<AudioSource>();
       
        realMat.SetFloat("_Cutoff", 0);
        dreamMat.SetFloat("_Cutoff", 1);

        dreamWorldObjects = GameObject.FindGameObjectsWithTag(tag1);
        realWorldObjects = GameObject.FindGameObjectsWithTag(tag2);
        dreamColliders = new Collider[dreamWorldObjects.Length];
        realColliders = new Collider[realWorldObjects.Length];
        dreamParticle = new ParticleSystem[dreamWorldObjects.Length];
        realParticle = new ParticleSystem[realWorldObjects.Length];

        for (int i = 0; i < dreamWorldObjects.Length; i++)
        {
            dreamColliders[i] = dreamWorldObjects[i].GetComponent<Collider>();
            dreamColliders[i].isTrigger = true;
            dreamParticle[i] = dreamWorldObjects[i].GetComponentInChildren<ParticleSystem>();
        }
        for (int i = 0; i < realWorldObjects.Length; i++)
        {
            realColliders[i] = realWorldObjects[i].GetComponent<Collider>();
            realParticle[i] = realWorldObjects[i].GetComponentInChildren<ParticleSystem>();
        }
       



    }

    
    void FixedUpdate()
    {
        //Input for beginning the game.
            if (Input.GetKeyDown(KeyCode.P))
            {
          
            mainImage.sprite = image2;
            spritefade = true;

            
            }
        if(spritefade && mainImage.color.a > 0)
        {
            player.moveSpeed = 0;
            player.cannotSwitch = true;

            float t = menuFade += 0.01f;
            mainImage.color = new Color(mainImage.color.r, mainImage.color.g, mainImage.color.b, Mathf.Lerp(1, 0, t));
            mainImage.GetComponentInChildren<Text>().color = new Color(mainImage.color.r, mainImage.color.g, mainImage.color.b, Mathf.Lerp(1, 0, t));
          
        }
        if (mainImage.color.a <= 0)
        {
            player.moveSpeed = 6;
            player.cannotSwitch = false;
            spritefade = false;
            mainImage.gameObject.SetActive(false);
        }


        Fade();

        //The data to allow the player to switch between realms. This gradually changes the colour of most objects, changes the speed of the music, and disables and enables different blocks.
            if (!player.cannotSwitch)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                //Changes world and sky materials, closes or opens the players eye, and slows or speeds up the music.
                    if (realWorldSwitch)
                    {
                    realWorldSwitch = false;
                    dreamWorldSwitch = true;
                    switchSpeed = 0.1f;
                    offSetPoint = worldMat.mainTextureOffset.y;
                    colorPoint = sky.GetColor("_Tint");
                    playerTexture.mainTexture = closedEye;
                    switchSound.pitch = 0.5f;
                    switchSound.Play();
                    }
                    else if (dreamWorldSwitch)
                    {
                    dreamWorldSwitch = false;
                    realWorldSwitch = true;
                    switchSpeed = 0.1f;
                    offSetPoint = worldMat.mainTextureOffset.y;
                    colorPoint = sky.GetColor("_Tint");
                    playerTexture.mainTexture = openEye;
                    switchSound.pitch = 1f;
                    switchSound.Play();
                }

                }
                if (realWorldSwitch)
                {
                enemy.speed = 5;
                if (worldMat.mainTextureOffset.y != 0)
                    {
                    
                        float t = switchSpeed += switchTime * Time.deltaTime;
                        Color lightCol = new Color(Mathf.Lerp(colorPoint.r, 0.58f, t), Mathf.Lerp(colorPoint.g, 0.47f, t), Mathf.Lerp(colorPoint.b, 0.33f, t));
                        worldMat.mainTextureOffset = new Vector2(0, Mathf.Lerp(offSetPoint, 0, t));
                        sky.SetColor("_Tint", lightCol);
                    music.pitch = Mathf.Lerp(0.7f, 1, t);
                        foreach (SpriteRenderer renderer in lightColour)
                        {

                            renderer.color = new Color(Mathf.Lerp(renderer.color.r, 1, t), Mathf.Lerp(renderer.color.g, 1, t), Mathf.Lerp(renderer.color.b, 0.7f, t));
                        }
                    }

                }
                else if (dreamWorldSwitch)
                {
                enemy.speed = 0;
                if (worldMat.mainTextureOffset.y != -0.25f)
                    {
                   
                        float t = switchSpeed += switchTime * Time.deltaTime;
                        Color lightCol = new Color(Mathf.Lerp(colorPoint.r, 0.7f, t), Mathf.Lerp(colorPoint.g, 0.24f, t), Mathf.Lerp(colorPoint.b, 0.23f, t));
                        worldMat.mainTextureOffset = new Vector2(0, Mathf.Lerp(offSetPoint, -0.25f, t));
                        sky.SetColor("_Tint", lightCol);
                    music.pitch = Mathf.Lerp(1, 0.7f, t);
                    foreach (SpriteRenderer renderer in lightColour)
                        {

                            renderer.color = new Color(Mathf.Lerp(renderer.color.r, 1, t), Mathf.Lerp(renderer.color.g, 0.23f, t), Mathf.Lerp(renderer.color.b, 0.4f, t));
                        }

                    }
                }
            }

        
    }

    void Fade()
    {
        //This controls the amount of time it takes to switch from one realm to another.
        float t = switchSpeed += fadeOverTime * Time.deltaTime;

        if (realWorldSwitch && realMat.GetFloat("_Cutoff") > 0)
        {
            realMat.SetFloat("_Cutoff", Mathf.Lerp(1, 0, t) );
            for (int i = 0; i < dreamWorldObjects.Length; i++)
            {
                dreamColliders[i].isTrigger = true;
                dreamParticle[i].Play();
            }

            dreamMat.SetFloat("_Cutoff", Mathf.Lerp(0, 1, t) );
            for (int i = 0; i < realWorldObjects.Length; i++)
            {
                realColliders[i].isTrigger = false;
                realParticle[i].Stop();
            }

        }

        else if (dreamWorldSwitch && dreamMat.GetFloat("_Cutoff") > 0)
        {
            dreamMat.SetFloat("_Cutoff", Mathf.Lerp(1, 0, t) );
            for (int i = 0; i < realWorldObjects.Length; i++)
            {
                realColliders[i].isTrigger = true;
                realParticle[i].Play();
            }


            realMat.SetFloat("_Cutoff", Mathf.Lerp(0, 1, t));
            for (int i = 0; i < dreamWorldObjects.Length; i++)
            {
                dreamColliders[i].isTrigger = false;
                dreamParticle[i].Stop();
            }


        }
    }




}
