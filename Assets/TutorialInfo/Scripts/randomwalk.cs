using UnityEngine;
using UnityEngine.AI;

public class RandomWalk : MonoBehaviour
{
    public float walkRadius = 10f;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        SetRandomDestination();
    }

    void Update()
    {
        // ถ้าเดินถึงจุดแล้ว
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetRandomDestination();
        }
    }

    void SetRandomDestination()
    {
        // สุ่มตำแหน่ง
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;

        randomDirection += transform.position;

        NavMeshHit hit;

        // หาจุดบน NavMesh
        if (NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1))
        {
            agent.SetDestination(hit.position);
        }
    }
}