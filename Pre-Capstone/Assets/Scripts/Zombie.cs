using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    private NavMeshAgent agent;
    public Animator animator;
    public Transform target;
    public GameController gc;

    public float distanceThreshold = 10f;
    public float viewDistance = 10f;
    public float fov = 120f;
    public float wanderRadius = 7f;
    public int health = 10;
    public enum AIState { idle, chasingPlayer, chasingCar, patrolling, attacking, dead};

    public AIState aiState = AIState.idle;
    static private List<GameObject> patrolPoints = null;
    
    private bool isAware;
    private GameObject currentTarget;
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

        if (health <= 0)
        {
            gc.ZombieDied(this.gameObject);
            //Destroy(this.gameObject);
            aiState = AIState.dead;
            isDead = true;
        }
    }

    public void CarAlarm(GameObject go)
    {
        currentTarget = go;
        
        if(!chasingPlayer)
        {
            aiState = AIState.chasingCar;
            chasingCar = true;
        }

    }
    IEnumerator Think()
    {
        while(true)
        {
            switch (aiState)
            {
                case AIState.idle:

                    

                    if(isAware)
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
                        Debug.Log("Undid Car Chase Successfully");
                        ChangeToChase();
                    }
                    else
                    {

                        Debug.Log("Inside Car Chase");
                        agent.SetDestination(currentTarget.transform.localPosition);
                        chasingCar = true;

                        if (isAware)
                        {
                            ChangeToChase();
                            chasingPlayer = true;
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
                    animator.SetBool("Dead", true);
                    break;
                default:
                    break;
            }
            
            
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void IsAware()
    {
        isAware = true;
        Debug.Log("is aware");
    }
    
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
                        Debug.Log("Player Found");
                    }
                }
            }
        }
        if(dist < distanceThreshold)
        {
            IsAware();
        }
    }

    public Vector3 RandomWanderPoint()
    {
        Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);
        return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);
    }

    

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

    void ChangeToAttack()
    {
        aiState = AIState.attacking;
    }

    IEnumerator Wait(float duration, string ai)
    {
        float time = 0;
        //Debug.Log("we are waiting");
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;

        }
        //Debug.Log("we are done waiting");
    }

    public void Attack()
    {
        RaycastHit objectHit;
        Vector3 fwd = this.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(this.transform.position, fwd * 50, Color.green);
        if (Physics.Raycast(this.transform.position, fwd, out objectHit, 50))
            target.GetComponent<PlayerController>().health--;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            ChangeToAttack();
        }
    }
}
