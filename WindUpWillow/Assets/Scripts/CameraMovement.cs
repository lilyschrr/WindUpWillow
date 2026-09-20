using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float speed = 10f;
    public Transform target;
    public GameObject targetObj;
    private PlayerMovement playerMove;
    void Start()
    {
        playerMove = targetObj.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMove.Alive)
        {
            Vector3 newPosition = new Vector3(target.position.x, target.position.y, -10f);
            transform.position = Vector3.Slerp(transform.position, newPosition, speed * Time.deltaTime);
        }
            
        
    }
}
