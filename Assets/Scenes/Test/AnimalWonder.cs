using UnityEngine;
using UnityEngine.AI; // จำเป็นต้องใช้สำหรับระบบ NavMesh

[RequireComponent(typeof(NavMeshAgent))]
public class AnimalWander : MonoBehaviour
{
    [Header("Wander Settings")]
    public float wanderRadius = 15f;  // ระยะรัศมีที่จะสุ่มเดินไป (ยิ่งเยอะยิ่งเดินไกล)
    public float minWaitTime = 2f;    // เวลารอน้อยที่สุดเมื่อเดินไปถึงจุดหมาย (วินาที)
    public float maxWaitTime = 6f;    // เวลารอนานที่สุดเมื่อเดินไปถึงจุดหมาย (วินาที)

    private NavMeshAgent agent;
    private float timer;
    private float currentWaitTime;
    private bool isWaiting = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // สุ่มหาจุดหมายแรกทันทีที่เริ่มเกม
        SetNewRandomDestination();
    }

    void Update()
    {
        // เช็คว่าสัตว์เดินไปใกล้ถึงจุดหมายหรือยัง
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!isWaiting)
            {
                // ถ้าถึงจุดหมายแล้ว ให้เริ่มรอนิ่งๆ
                isWaiting = true;
                currentWaitTime = Random.Range(minWaitTime, maxWaitTime);
                timer = 0f;
            }
        }

        // หากกำลังอยู่ในช่วงรอ ให้จับเวลา
        if (isWaiting)
        {
            timer += Time.deltaTime;
            if (timer >= currentWaitTime)
            {
                // เมื่อรอครบเวลา ให้สุ่มจุดหมายใหม่แล้วเดินต่อ
                isWaiting = false;
                SetNewRandomDestination();
            }
        }
    }

    // ฟังก์ชันสำหรับสุ่มหาตำแหน่งใหม่บน NavMesh
    void SetNewRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position; // อิงจากตำแหน่งปัจจุบันของสัตว์

        NavMeshHit navHit;

        // ค้นหาจุดบน NavMesh ที่ใกล้กับค่าสุ่มมากที่สุด
        if (NavMesh.SamplePosition(randomDirection, out navHit, wanderRadius, -1))
        {
            agent.SetDestination(navHit.position);
        }
    }
}