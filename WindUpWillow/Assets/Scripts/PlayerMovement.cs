using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody2D rb;
    private Vector2 movement;

    public float xDir;
    public float yDir;
    private float x;
    private float y;
   // private Animator animator;
    public Sprite[] sprites;
    public int sprIndex = 0;

    public Transform movePoint;

    public LayerMask WhatStopsMovement;
    public LayerMask Killers;
    private float timeVal = 0;
    public float timeDelay = 1;

    [SerializeField] private GameObject windingKey;
    private WindUpController windingController;

    public AudioSource DeathNoise;

    private bool Alive = true;
    private bool DeathNotSet = true;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //  animator = GetComponent<Animator>();
        //  sprite = GetComponent<Sprite>();
        windingController = windingKey.GetComponent<WindUpController>();
        
    }
    private void Start()
    {
        movePoint.parent = null;
        x = xDir;
        y = yDir;
        movePoint.position += new Vector3(x, 0.0f, 0.0f);
       // animator.SetFloat("Horizontal", movement.x);
        GetComponent<SpriteRenderer>().sprite = sprites[sprIndex];
        timeDelay = windingController.SecondsPerBeat;
    }

    private void Update()
    {
        if (windingController.AbleToWind)
        {
            //empty so nothing happens at this point
        }
        else if (windingController.MoveAmount > 0)
        {

            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);
            timeVal += Time.deltaTime;
            if (Physics2D.OverlapCircle(movePoint.position, 0.2f, Killers)) Alive = false;

            if (Vector3.Distance(transform.position, movePoint.position) <= .00000001f && Alive)
            {
                if (Keyboard.current.aKey.isPressed)
                {
                    x = -1f;
                    y = 0f;
                }
                else if (Keyboard.current.dKey.isPressed)
                {
                    x = 1f;
                    y = 0f;
                }
                else if (Keyboard.current.sKey.isPressed)
                {
                    y = -1f;
                    x = 0f;
                }
                else if (Keyboard.current.wKey.isPressed)
                {
                    y = 1f;
                    x = 0f;
                }
                else
                {

                }
                movement = new Vector2(x, y);
                if (timeVal > timeDelay)
                {
                    if (!CheckCollisionHorizontal(x) && x != 0f)
                    {
                        movePoint.position += new Vector3(x, 0.0f, 0.0f);
                        // animator.SetFloat("Horizontal", movement.x);
                        // animator.SetFloat("Speed", movement.sqrMagnitude);
                    }
                    else if (!CheckCollisionVertical(y) && y != 0f)
                    {
                        movePoint.position += new Vector3(0.0f, y, 0.0f);
                        // animator.SetFloat("Vertical", movement.y);
                        // animator.SetFloat("Speed", movement.sqrMagnitude);
                    }
                    else if (x != 0f)
                    {
                        x = -x;
                        if (!CheckCollisionHorizontal(x)) movePoint.position += new Vector3(x, 0.0f, 0.0f);

                    }
                    else if (y != 0f)
                    {
                        y = -y;
                        if (!CheckCollisionVertical(y)) movePoint.position += new Vector3(0f, y, 0.0f);
                    }

                  
                    timeVal = 0;
                    sprIndex++;
                    if (sprIndex >= sprites.Length) sprIndex = 0;
                    GetComponent<SpriteRenderer>().sprite = sprites[sprIndex];
                }
                else
                {
                    // animator.SetFloat("Horizontal", 0);
                    // animator.SetFloat("Vertical", 0);

                }


            }
            else if (!Alive)
            {
                windingController.CeaseAll();
                
            }


        }
        else if (DeathNotSet)
        {
            //death
            if (!DeathNoise.isPlaying) DeathNoise.Play();
            movePoint.position += new Vector3(0f, -100f, 0);
            DeathNotSet = false;
        }
        else
        { 
            timeVal += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);
            //if (!DeathNoise.isPlaying) SceneManager.LoadScene("Lose");
        }
    }
   

    private bool CheckCollisionHorizontal(float nextX)
    {
        return Physics2D.OverlapCircle(movePoint.position + new Vector3(nextX, 0.0f, 0.0f), 0.2f, WhatStopsMovement);
    }

    private bool CheckCollisionVertical(float nextY)
    {
        return Physics2D.OverlapCircle(movePoint.position + new Vector3(0.0f, nextY, 0.0f), 0.2f, WhatStopsMovement);
    }

    private void FixedUpdate()
    {
        // rb.MovePosition(
        //    rb.position + movement * speed * Time.fixedDeltaTime
        //);
    }
}