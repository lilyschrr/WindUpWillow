using System.Timers;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody2D rb;
    private Vector2 movement;

    private float x;
    private float y;
   // private Animator animator;
    public Sprite[] sprites;
    private int sprIndex = 0;

    public Transform movePoint;

    public LayerMask WhatStopsMovement;
    public float timeVal = 0;
    private float timeDelay = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
      //  animator = GetComponent<Animator>();
        //  sprite = GetComponent<Sprite>();

    }
    private void Start()
    {
        movePoint.parent = null;
        y = -1f;
        movePoint.position += new Vector3(x, 0.0f, 0.0f);
       // animator.SetFloat("Horizontal", movement.x);
        GetComponent<SpriteRenderer>().sprite = sprites[sprIndex];
    }

    private void Update()
    {


        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);
        timeVal += Time.deltaTime;

        if (Vector3.Distance(transform.position, movePoint.position) <= .00000001f)
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
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(x, 0.0f, 0.0f), 0.2f, WhatStopsMovement))
                {
                    movePoint.position += new Vector3(x, 0.0f, 0.0f);
                    // animator.SetFloat("Horizontal", movement.x);
                   // animator.SetFloat("Speed", movement.sqrMagnitude);
                }
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0.0f, y, 0.0f), 0.2f, WhatStopsMovement))
                {
                    movePoint.position += new Vector3(0.0f, y, 0.0f);
                    // animator.SetFloat("Vertical", movement.y);
                   // animator.SetFloat("Speed", movement.sqrMagnitude);
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



    }

    private void FixedUpdate()
    {
        // rb.MovePosition(
        //    rb.position + movement * speed * Time.fixedDeltaTime
        //);
    }
}