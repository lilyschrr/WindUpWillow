
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChomperMovement : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody2D rb;
    private Vector2 movement;

    public float xDir;
    public float yDir;
    private float x;
    private float y;
    private Animator animator;
    public Sprite[] sprites;
    public int sprIndex = 0;

    public Transform movePoint;
    public bool Stationary;

    public LayerMask Path;
    private float timeVal = 0;
    public float timeDelay = 1;

    [SerializeField] private GameObject windingKey;
    private WindUpController windingController;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
          animator = GetComponent<Animator>();
        //  sprite = GetComponent<Sprite>();
        windingController = windingKey.GetComponent<WindUpController>();
        
    }
    private void Start()
    {
        movePoint.parent = null;
        x = xDir;
        y = yDir;
        movePoint.position += new Vector3(0.0f, 0.0f, 0.0f);
        animator.SetBool("Stationary", Stationary);
        animator.SetFloat("Vertical", y);
        animator.SetFloat("Horizontal", x);
        animator.SetBool("Motion", false);
        GetComponent<SpriteRenderer>().sprite = sprites[sprIndex];
        timeDelay = windingController.SecondsPerBeat;
    }

    private void Update()
    {
        if (windingController.AbleToWind)
        {

        }
        else if (windingController.MoveAmount > 0)
        {

            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);
            timeVal += Time.deltaTime;

            if (Vector3.Distance(transform.position, movePoint.position) <= .01f)
            {
                //animator.SetBool("Motion", false);
                
                movement = new Vector2(x, y);
                if (timeVal > timeDelay)
                {
                    animator.SetBool("Motion", false);
                    if (x != 0f && !Stationary)
                    {
                        animator.SetBool("HDir", true);
                        if (CheckPathHorizontal(x))
                        {
                            movePoint.position += new Vector3(x, 0.0f, 0.0f);
                           
                        }
                        else if (CheckPathVertical(-1)){
                            movePoint.position += new Vector3(0.0f, -1f, 0.0f);
                            sprIndex = 2;
                            animator.SetBool("HDir", false);
                        }
                        else if (CheckPathVertical(1)){
                            movePoint.position += new Vector3(0.0f, 1f, 0.0f);
                            sprIndex = 3;
                            animator.SetBool("HDir", false);
                        }
                        else
                        {
                            if (CheckPathHorizontal(-x))
                            {
                                movePoint.position += new Vector3(-x, 0.0f, 0.0f);
                            }
                            if (x > 0f) sprIndex = 0;
                            else sprIndex = 1;
                            
                        }
                        

                    }
                    if (y != 0f && !Stationary)
                    {
                        animator.SetBool("HDir", false);
                        if (CheckPathVertical(y))
                        {
                            movePoint.position += new Vector3(0.0f, y, 0.0f);
                        }
                        else if (CheckPathHorizontal(1))
                        {
                            movePoint.position += new Vector3(1f, 0.0f, 0.0f);
                            sprIndex = 1;
                            animator.SetBool("HDir", true);
                        }
                        else if (CheckPathHorizontal(-1))
                        {
                            movePoint.position += new Vector3(-1f, 0.0f, 0.0f);
                            sprIndex = 0;
                            animator.SetBool("HDir", true);
                        }
                        else
                        {
                            if (CheckPathVertical(-y))
                            {
                                movePoint.position += new Vector3(0.0f, -y, 0.0f);
                            }
                            if (y > 0f) sprIndex = 2;
                            else sprIndex = 3;
                            
                        }
                        
                    }
                    if (sprIndex == 0) //moving left
                    {
                        x = -1f;
                        y = 0f;
                    }
                    else if (sprIndex == 1) //moving right
                    {
                        x = 1f;
                        y = 0f;
                    }
                    else if (sprIndex == 2) //moving down
                    {
                        y = -1f;
                        x = 0f;
                    }
                    else if (sprIndex == 3) //moving up
                    {
                        y = 1f;
                        x = 0f;
                    }
                    else
                    {

                    }
                    animator.SetBool("Motion", true);
                    animator.SetFloat("Horizontal", x);
                    animator.SetFloat("Vertical", y);

                    timeVal = 0;
                    GetComponent<SpriteRenderer>().sprite = sprites[sprIndex];
                }
                else
                {
                    // animator.SetFloat("Horizontal", 0);
                    // animator.SetFloat("Vertical", 0);
                }


            }
            else
            {
                
            }
           

        }
        else
        {
            animator.SetBool("Motion", false);
        }
    }


    private bool CheckPathHorizontal(float nextX)
    {
        return Physics2D.OverlapCircle(movePoint.position + new Vector3(nextX, 0.0f, 0.0f), 0.2f, Path);
    }

    private bool CheckPathVertical(float nextY)
    {
        return Physics2D.OverlapCircle(movePoint.position + new Vector3(0.0f, nextY, 0.0f), 0.2f, Path);
    }

    private void FixedUpdate()
    {
        // rb.MovePosition(
        //    rb.position + movement * speed * Time.fixedDeltaTime
        //);
    }
}