using UnityEngine;
using UnityEngine.Events;

public enum AnimalType
{
    Tiger,    // เสือ
    Deer,     // กวาง
    Hornbill  // นกเงือก
}

public class Animal : MonoBehaviour
{
    [Header("Animal Profiles")]
    public AnimalType animalType;
    public string animalName;

    [Header("Relationship Values")]
    public float currentAffinity = 0f;
    public float maxAffinity = 100f;
    public float affinityGainRate = 5f;

    [Header("Events")]
    public UnityEvent onMaxAffinityReached;

    private bool isMaxed = false;

    public void IncreaseAffinity(float amount)
    {
        if (isMaxed) return;

        currentAffinity += amount;
        currentAffinity = Mathf.Clamp(currentAffinity, 0f, maxAffinity);

        Debug.Log($"[{animalName}] ค่าความสัมพันธ์: {currentAffinity:F1}/{maxAffinity}");

        if (currentAffinity >= maxAffinity && !isMaxed)
        {
            isMaxed = true;
            OnAffinityMax();
        }
    }
    private void OnAffinityMax()
    {
        Debug.LogWarning($"🎉 [{animalName}] มีความสัมพันธ์เต็ม 100 กับเพื่อนในสปีชีส์แล้ว!");
        if (onMaxAffinityReached != null)
        {
            onMaxAffinityReached.Invoke();
        }
    }
}