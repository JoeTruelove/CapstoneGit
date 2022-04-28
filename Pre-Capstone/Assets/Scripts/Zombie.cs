using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This is the Heart script for the Zombie AI and controls everything for the AI to think.
public class Zombie : MonoBehaviour
{
    private NavMeshAgent agent;
    public Animator animator;
    public Transform target;
    public GameController gc;

    public enum AIState { idle, chasingPlayer, chasingCar, chasingPipe, patrolling, attacking, dead };

    public float distanceThreshold = 10f;
    public float viewDistance = 10f;
    public float fov = 120f;
    public float wanderRadius = 7f;
    public int health = 10;
    public int damage = 1;

    public AIState aiState = AIState.idle;
    static private List<GameObject> patrolPoints = null;
    
    private bool isAware;
    private Transform currentTarget;
    private bool chasingCar;
    private bool chasingPlayer;
    private Vector3 wanderPoint;
    private bool isDead = false;
    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Think());
        target = GameObject.FindGameObjectWithTag("Player").transform;
        wanderPoint = RandomWanderPoint();

        //Adds patrol points into Zombies brain
        /*if(patrolPoints == null)
        {
            patrolPoints = new List<GameObject>();
            foreach(GameObject go in GameObject.FindGameObjectsWithTag("PatrolPoints"))
            {
                Debug.Log("Patrol Point: " + go.transform.position);
                patrolPoints.Add(go);
            }
        }*/
    }

    void Update()
    {
        SearchForPlayer();

        if (health <= 0 && !isDead)
        {
            aiState = AIState.dead;
            gc.ZombieDied(this.gameObject);
            agent.isStopped = true;
            //Destroy(this.gameObject);
            
            isDead = true;
        }
    }

    //Used by the Car script to change Zombie to chase the Car
    public void CarAlarm(GameObject go)
    {
        currentTarget = go.transform;
        
        if(!chasingPlayer)
        {
            aiState = AIState.chasingCar;
            chasingCar = true;
        }

    }

    //Used by PipeBomb Script to change Zombie to chase the Pipebomb
    public void PipeBomb(Transform go)
    {
        currentTarget = go.transform;

        ChangeToPipeBomb();

    }

    //This method is what controls what the Zombie is currently doing when they are in a certain "state"
    IEnumerator Think()
    {
        while(true)
        {
            switch (aiState)
            {
                case AIState.idle:

                    if (isAware)
                    {
                        ChangeToChase();
                        chasingPlayer = true;
                    }
                    agent.SetDestination(transform.position);
                    break;
                case AIState.chasingPlayer:
                    animator.SetBool("IsRunning", true);
                    if (!isAware)
                    {
                        ChangeToIdle();
                    }
                    agent.SetDestination(target.position);
                    break;
                case AIState.chasingCar:

                    if (chasingPlayer)
                    {
                        //Debug.Log("Undid Car Chase Successfully");
                        ChangeToChase();
                    }
                    else
                    {
                        //Debug.Log("Inside Car Chase");
                        agent.SetDestination(currentTarget.localPosition);
                        chasingCar = true;

                        if (isAware)
                        {
                            ChangeToChase();
                            chasingPlayer = true;
                        }
                    }
                    break;
                case AIState.chasingPipe:
                    {
                        if (currentTarget != null)
                        {
                            agent.SetDestination(currentTarget.localPosition);
                        }
                        else if (isAware)
                        {
                            ChangeToChase();
                        }
                        else
                        {
                            ChangeToPatrolling();
                        }
                    }
                    break;
                case AIState.patrolling:
                    animator.SetBool("IsWalking", true);
                    
                    
                    if (isAware)
                    {
                        ChangeToChase();
                        chasingPlayer = true;
                    }
                    Wander();
                    break;
                case AIState.attacking:
                    animator.SetTrigger("Attack");
                    ChangeToChase();
                    break;
                case AIState.dead:
                    //Destroy(this.gameObject);
                    animator.SetBool("Dead", true);
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    //A check to see if the Zombie is aware of the Player's presence
    public void IsAware()
    {
        isAware = true;
        //Debug.Log("is aware");
    }
    
    //Method to change the Zombie's current destination to a random wander point
    public void Wander()
    {
        if(Vector3.Distance(transform.position, wanderPoint) < 2f)
        {
            wanderPoint = RandomWanderPoint();
        }else
        {
            agent.SetDestination(wanderPoint);
        }
    }

    //A continuous check to see if the Player is within the Zombie's sight
    public void SearchForPlayer()
    {
        float dist = Vector3.Distance(target.position, transform.position);
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(target.transform.position)) < fov / 2f)
        {
            //Debug.Log("We are in 1");
            if (Vector3.Distance(target.position, transform.position) < viewDistance)
            {
                //Debug.Log("We are in 2");
                RaycastHit hit;
                if(Physics.Linecast(transform.position, target.transform.position, out hit, -1))
                {
                    //Debug.Log("We are in 3");
                    if (hit.transform.CompareTag("Player"))
                    {
                        IsAware();
                        //Debug.Log("Player Found");
                    }
                }
            }
        }
        if(dist < distanceThreshold)
        {
            IsAware();
        }
    }

    //Method to create a random point within a distance for the Zombie to "wander" to
    public Vector3 RandomWanderPoint()
    {
        Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);
        return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);
    }

    /*
        These multiple "ChangeTo..." methods are used by other methods to change the Zombie's current thinking state.
     */
    void ChangeToPatrolling()
    {
        aiState = AIState.patrolling;
        animator.SetBool("IsWalking", true);

    }

    void ChangeToChase()
    {
        aiState = AIState.chasingPlayer;
        animator.SetBool("IsRunning", true);
    }

    void ChangeToIdle()
    {
        aiState = AIState.idle;
        animator.SetBool("IsWalking", false);
    }

    void ChangeToCar()
    {
        aiState = AIState.chasingCar;
        animator.SetBool("IsWalking", true);
    }

    void ChangeToPipeBomb()
    {
        aiState = AIState.chasingPipe;
        animator.SetBool("IsWalking", true);
    }

    void ChangeToAttack()
    {
        aiState = AIState.attacking;
    }

    //This method is used by the Zombie's attack animation to determine if the Player is currently in front of the Zombie
    //while it is attacking.
    public void Attack()
    {
        RaycastHit objectHit;
        Vector3 fwd = this.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(this.transform.position, fwd * 50, Color.green);
        if (Physics.Raycast(this.transform.position, fwd, out objectHit, 50))
            target.GetComponent<PlayerController>().health = target.GetComponent<PlayerController>().health - damage;
    }

    //The Zombie's OnTrigger to attack the Player.
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            ChangeToAttack();
        }
    }
}
