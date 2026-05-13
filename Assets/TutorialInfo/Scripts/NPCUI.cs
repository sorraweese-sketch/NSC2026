using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class NPCUI : MonoBehaviour
{
    public GameObject panel;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI hungerText;
    public TextMeshProUGUI thirstText;
    public TextMeshProUGUI ageText;
    public TextMeshProUGUI relationText;

    public RawImage npcImage;

    // ระยะที่กด E ได้
    public float interactDistance = 10f;

    private NPCData data;

    private NavMeshAgent agent;

    private bool isOpen = false;

    void Start()
    {
        data = GetComponent<NPCData>();

        agent = GetComponent<NavMeshAgent>();

        // เริ่มเกมให้ UI ปิด
        panel.SetActive(false);
    }

    void Update()
    {
        // ระยะจากกล้องถึง NPC
        float distance =
            Vector3.Distance(
                Camera.main.transform.position,
                transform.position
            );

        // ถ้าอยู่ใกล้พอ
        if (distance <= interactDistance)
        {
            // กด E
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                // เปิด / ปิด UI
                isOpen = !isOpen;

                panel.SetActive(isOpen);

                // หยุดเดินเมื่อเปิด UI
                agent.isStopped = isOpen;

                // ถ้า UI เปิด
                if (isOpen)
                {
                    // อัปเดตข้อความ
                    healthText.text =
                        "Health : " + data.health;

                    hungerText.text =
                        "Hunger : " + data.hunger;

                    thirstText.text =
                        "Thirst : " + data.thirst;

                    ageText.text =
                        "Age : " + data.age;

                    relationText.text =
                        "Relation : " + data.relationship;

                    // อัปเดตรูป
                    npcImage.texture =
                        data.profilePicture;
                }
            }
        }
    }
}