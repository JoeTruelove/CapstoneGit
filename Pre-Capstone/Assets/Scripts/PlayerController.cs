using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    The PlayerController takes care of most of the Player's mechanics.
    Takes care of movement and the Player's Variables
 */
public class PlayerController : MonoBehaviour
{
    private InputHandler _input;
    private Vector3 mousePos;
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private Camera camera;

    public GameObject bulletSpawnPoint;
    public GameObject bullet;
    public GameObject inventory;
    public PlayerGun pg;
    public float waitTime;
    public int health = 20;
    private bool dead = false;

    private void Awake()
    {
        _input = GetComponent<InputHandler>();
    }

    private void Update()
    {
        //Player Facing Mouse
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0f;

        if(playerPlane.Raycast(ray, out hitDist) && !dead)
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7f * Time.deltaTime);
        }

        //Keeps track if the Player Dies
        if(health <= 0)
        {
            dead = true;
        }

        //Shooting
        if(Input.GetMouseButtonDown(0))
        {
            //Debug.Log(inventoryOpen);
            Shoot();
            pg.Fire();
        }

        //Player Run
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = 7;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift)) moveSpeed = 5;
    }

    private void FixedUpdate()
    {
        //PlayerMovement
        if(!dead)
        {
            var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);

            var movementVector = MoveTowardTarget(targetVector);
        }
        
        //RotateTowardMovementVector(movementVector);
    }

    //This Code Moves Player toward Movement rather direction of mouse
    /*private void RotateTowardMovementVector(Vector3 movementVector)
    {
        var rotation = Quaternion.LookRotation(movementVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);
    }*/

    //Calculates the movement for the Player
    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = moveSpeed * Time.deltaTime;

        //transform.Translate(targetVector * speed);

        targetVector = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        
        transform.position = targetPosition;
        return targetVector;
    }

    //Instantiates a bullet with bullet script
    private void Shoot()
    {
        Instantiate(bullet.transform, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
    }
}
