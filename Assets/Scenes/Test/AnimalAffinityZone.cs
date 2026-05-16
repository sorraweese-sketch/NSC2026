using System.Collections.Generic;
using UnityEngine;

public class AnimalAffinityZone : MonoBehaviour
{
    private Animal myAnimal;
    private HashSet<Animal> sameSpeciesInZone = new HashSet<Animal>();

    void Start()
    {
        // ค้นหาสคริปต์ Animal จากวัตถุพ่อแม่
        myAnimal = GetComponentInParent<Animal>();
        if (myAnimal == null)
        {
            Debug.LogError($"[AffinityZone] ไม่พบคอมโพเนนต์ Animal บนวัตถุพ่อแม่ของ {gameObject.name}");
        }
    }

    void Update()
    {
        // ถ้ามีสัตว์ชนิดเดียวกันอยู่ในรัศมี ให้เพิ่มค่าความสัมพันธ์
        if (sameSpeciesInZone.Count > 0)
        {
            float amountToIncrease = myAnimal.affinityGainRate * Time.deltaTime;
            myAnimal.IncreaseAffinity(amountToIncrease);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Animal otherAnimal = other.GetComponent<Animal>();

        if (otherAnimal != null && otherAnimal != myAnimal)
        {
            // เช็คว่าเป็นสัตว์ชนิดเดียวกันไหม (เสือ-เสือ, กวาง-กวาง, นกเงือก-นกเงือก)
            if (otherAnimal.animalType == myAnimal.animalType)
            {
                sameSpeciesInZone.Add(otherAnimal);
                Debug.Log($"<color=green>[เข้าโซน]</color> {myAnimal.animalName} เจอ {otherAnimal.animalName}");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        Animal otherAnimal = other.GetComponent<Animal>();

        if (otherAnimal != null)
        {
            if (sameSpeciesInZone.Contains(otherAnimal))
            {
                sameSpeciesInZone.Remove(otherAnimal);
                Debug.Log($"<color=red>[ออกโซน]</color> {otherAnimal.animalName} เดินแยกจาก {myAnimal.animalName}");
            }
        }
    }
}