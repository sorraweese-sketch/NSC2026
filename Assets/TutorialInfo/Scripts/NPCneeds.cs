using UnityEngine;
using UnityEngine.AI;

public class NPCNeeds : MonoBehaviour
{
    private NPCData data;

    private NavMeshAgent agent;

    // กำลังไปกินน้ำไหม
    private bool goingToDrink = false;

    void Start()
    {
        data = GetComponent<NPCData>();

        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // ถ้าค่าน้ำต่ำกว่า 80
        // และยังไม่ได้ไปกินน้ำ
        if (data.thirst <= 80 && !goingToDrink)
        {
            goingToDrink = true;

            // เดินไปหาน้ำ
            agent.SetDestination(
                data.waterPoint.position
            );
        }

        // ถ้ากำลังไปกินน้ำ
        if (goingToDrink)
        {
            // ถึงจุดน้ำแล้ว
            if (!agent.pathPending &&
                agent.remainingDistance <= 2f)
            {
                // เติมน้ำ
                data.thirst = 100;

                // กลับไปสถานะปกติ
                goingToDrink = false;
            }
        }
    }
}