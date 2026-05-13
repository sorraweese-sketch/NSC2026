using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPCUI : MonoBehaviour
{
    public GameObject panel;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI hungerText;
    public TextMeshProUGUI thirstText;
    public TextMeshProUGUI ageText;
    public TextMeshProUGUI relationText;

    public RawImage npcImage;

    private NPCData data;

    void Start()
    {
        data = GetComponent<NPCData>();
    }

    void OnMouseOver()
    {   
        Debug.Log("Mouse On NPC");

        if (Input.GetMouseButtonDown(1))
        {
            panel.SetActive(true);

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

            npcImage.texture =
                data.profilePicture;
        }
    }
}