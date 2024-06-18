using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float chaseDelay = 2f; // Adjust as needed
    [SerializeField] float attackDelay = 1f; // Adjust as needed

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    EnemyHealth health;
    Transform target;
    Animator animator;

    Coroutine currentActionCoroutine; // Track the current coroutine action

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        target = FindObjectOfType<HealthManager>().transform;
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (health.IsDead())
        {
            enabled = false;
            navMeshAgent.enabled = false;
            return;
        }

        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
            StartCoroutine(ChaseDelay());
        }
    }

    IEnumerator ChaseDelay()
    {
        yield return new WaitForSeconds(chaseDelay);
        if (isProvoked && distanceToTarget <= chaseRange)
        {
            EngageTarget();
        }
    }

    private void EngageTarget()
    {
        FaceTarget();

        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            StartActionCoroutine(ChaseTarget());
        }
        else if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            StartActionCoroutine(AttackTarget());
        }
    }

    void StartActionCoroutine(IEnumerator coroutine)
    {
        // Stop any existing coroutine
        if (currentActionCoroutine != null)
        {
            StopCoroutine(currentActionCoroutine);
        }

        // Start the new coroutine
        currentActionCoroutine = StartCoroutine(coroutine);
    }

    IEnumerator ChaseTarget()
    {
        Debug.Log("Chasing target");
        if (animator != null)
        {
            animator.SetBool("attack", false);
            animator.SetTrigger("move");
        }
        navMeshAgent.SetDestination(target.position);

        yield return new WaitForSeconds(chaseDelay); // Adjust delay as needed
        EngageTarget(); // Resume engagement after delay
    }

    IEnumerator AttackTarget()
    {
        Debug.Log("Attacking target");
        if (animator != null)
        {
            animator.SetBool("attack", true);
        }

        yield return new WaitForSeconds(attackDelay); // Adjust delay as needed
        EngageTarget(); // Resume engagement after delay
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
