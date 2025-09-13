using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation.Samples;




public class EnemyMove : MonoBehaviour
{
    public NavigationLoop navigationLoop;
    public AgentSpeedController agentSpeedController;

    public NavMeshAgent agent;
    public Transform playerTransform;
    public Transform castleTransform;
    public float chaseDistance = 7f;
    public float unchaseDistance = 15f;
    public float attackDistance = 0.5f;
    
    public EnemyState enemyState;
    public Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agentSpeedController = GetComponent<AgentSpeedController>();
        navigationLoop = GetComponent<NavigationLoop>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Walk);
            agentSpeedController.enabled = false;
            navigationLoop.enabled = true;
            agent.speed = 3.5f;

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < attackDistance)
        {
            ChangeState(EnemyState.Attack);
            agentSpeedController.enabled = false;
            navigationLoop.enabled = false;

        }
        else
        {
            if (Vector3.Distance(castleTransform.position, playerTransform.position) > unchaseDistance)
            {
                ChangeState(EnemyState.Walk);
                agentSpeedController.enabled = false;
                navigationLoop.enabled = true;
                agent.speed = 3.5f;

            }
            else if (Vector3.Distance(castleTransform.position, playerTransform.position) < unchaseDistance && enemyState==EnemyState.Attack)
            {
                ChangeState(EnemyState.Chase);
                agentSpeedController.enabled = true;
                navigationLoop.enabled = false;
                agent.speed = 6f;

            }
            else if (Vector3.Distance(castleTransform.position, playerTransform.position) < chaseDistance)
            {
                ChangeState(EnemyState.Chase);
                agentSpeedController.enabled = true;
                navigationLoop.enabled = false;
                agent.speed = 6f;
            }
        
        }
        
        OnDrawGizmos();
    }
    public void ChangeState(EnemyState newState)
    {
        if (enemyState == EnemyState.Walk) anim.SetBool("isWalk", false);
        else if (enemyState == EnemyState.Chase) anim.SetBool("isChase", false);
        else if (enemyState == EnemyState.Attack) anim.SetBool("isAttack", false);
        enemyState = newState;
        if (enemyState == EnemyState.Walk)anim.SetBool("isWalk", true);
        else if (enemyState == EnemyState.Chase)anim.SetBool("isChase", true);
        else if (enemyState == EnemyState.Attack)anim.SetBool("isAttack", true);
    
    }
    public enum EnemyState
    {
        Walk,
        Chase,
        Attack
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
