using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation.Samples;


public class EnemyAI : MonoBehaviour
{

    public NavigationLoop navigationLoop;
    public AgentSpeedController agentSpeedController;

    public NavMeshAgent m_agent;
    public Transform playerTransform;
    public Transform castleTransform;
    public LayerMask whatIsGround, whatIsPlayer;
    public float chaseDistance = 7f;
    public float unchaseDistance = 13f;
    public float attackDistance = 0.5f;
    public bool isChase=false;

    //vị trí điểm đến khi Patrolling
    public Transform[] goals = new Transform[3];
    private int m_NextGoal = 0;

    public EnemyState enemyState;
    public Animator anim;
    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Patrolling);
    }
    void Update()
{
    float distanceEnemyToPlayer = Vector3.Distance(transform.position, playerTransform.position);
    float distanceCastleToPlayer=Vector3.Distance(castleTransform.position, playerTransform.position);

    // Ưu tiên Attack nếu đang ở gần player
        if (distanceEnemyToPlayer <= attackDistance)
        {
            if (enemyState != EnemyState.Attack)
                ChangeState(EnemyState.Attack);
            // Không return ở đây nếu muốn enemy tiếp tục kiểm tra chase sau khi attack xong
            return;
        }
        // Nếu đang attack mà player đã ra khỏi vùng attack thì chuyển về Chase hoặc Patrolling
        else if (isChase || (enemyState == EnemyState.Attack && distanceEnemyToPlayer <= chaseDistance))
        {
            m_agent.SetDestination(playerTransform.position);
            if (enemyState != EnemyState.Chase)
                ChangeState(EnemyState.Chase);
        }
        else
        {
            if (enemyState != EnemyState.Patrolling)
                ChangeState(EnemyState.Patrolling);
            PatrollingLoop();
        }

    // Cập nhật trạng thái chase theo khoảng cách
    if (distanceCastleToPlayer < chaseDistance) isChase = true;
    else if (distanceCastleToPlayer > unchaseDistance) isChase = false;
}

    public void PatrollingLoop()
    {
        float distance = Vector3.Distance(m_agent.transform.position, goals[m_NextGoal].position);
            if (distance < 0.5f)
            {
                m_NextGoal = m_NextGoal != goals.Length-1 ? m_NextGoal + 1 : 0;
            }
            m_agent.destination = goals[m_NextGoal].position;
    }

public void ChangeState(EnemyState newState)
    {
        if (enemyState == newState) return;
        if (enemyState == EnemyState.Patrolling)
        {
            anim.SetBool("isWalk", false);
        }
        else if (enemyState == EnemyState.Chase) anim.SetBool("isChase", false);
        else if (enemyState == EnemyState.Attack) anim.SetBool("isAttack", false);
        enemyState = newState;
        if (enemyState == EnemyState.Patrolling)
        {
            anim.SetBool("isWalk", true);
        }
        else if (enemyState == EnemyState.Chase) anim.SetBool("isChase", true);
        else if (enemyState == EnemyState.Attack) anim.SetBool("isAttack", true);

    }
    public enum EnemyState
    {
        Patrolling,
        Chase,
        Attack
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(castleTransform.position, chaseDistance);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(castleTransform.position, unchaseDistance);
    }
}
