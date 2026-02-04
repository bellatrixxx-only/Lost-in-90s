using UnityEngine;

public class SafeWheelUI : MonoBehaviour
{
    [Header("UI refs")]
    [SerializeField] private RectTransform numbers; 

    [Header("Tuning")]
    [SerializeField] private float stepY = 88.5f;  
    [SerializeField] private float baseY = -359.2f; 
    [SerializeField, Range(0, 9)] private int currentDigit = 0;

    public int CurrentDigit => currentDigit;

    private void OnEnable()
    {
        ApplyPosition();
    }

    public void Step(int direction)
    {
        
        int d = (currentDigit + direction) % 10;
        if (d < 0) d += 10;
        SetDigit(d);
    }

    public void SetDigit(int digit)
    {
        currentDigit = Mathf.Clamp(digit, 0, 9);
        ApplyPosition();
    }

    private void ApplyPosition()
    {
        if (!numbers) return;

        var p = numbers.anchoredPosition;
        p.y = baseY + currentDigit * stepY;
        numbers.anchoredPosition = p;
    }
}
