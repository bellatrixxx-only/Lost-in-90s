using UnityEngine;

public class SafeMiniGameController : MonoBehaviour
{
    [Header("Wheels")]
    public SafeWheelUI[] wheels;

    [Header("UI")]
    [SerializeField] private GameObject safeMiniGameRoot;   
    [SerializeField] private GameObject controlsHint;       
    [Header("State")]
    public int activeWheelIndex = 0;

    [Header("Code")]
    [SerializeField] private int[] correctCode = new int[] { 1, 8, 4, 1 };

    [Header("Links")]
    [SerializeField] private SafeProximityTooltip proximity; 

    private bool isActive = false;

    private void OnEnable()
    {
        isActive = true;
        if (wheels != null && wheels.Length > 0)
            activeWheelIndex = Mathf.Clamp(activeWheelIndex, 0, wheels.Length - 1);
    }

    private void Update()
    {
        if (!isActive) return;
        if (wheels == null || wheels.Length == 0) return;

        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitMinigame();
            return;
        }

        // переключение колеса
        if (Input.GetKeyDown(KeyCode.LeftArrow)) SelectWheel(activeWheelIndex - 1);
        if (Input.GetKeyDown(KeyCode.RightArrow)) SelectWheel(activeWheelIndex + 1);

        // прокрутка цифры на активном колесе
        if (Input.GetKeyDown(KeyCode.UpArrow)) wheels[activeWheelIndex].Step(-1);
        if (Input.GetKeyDown(KeyCode.DownArrow)) wheels[activeWheelIndex].Step(+1);

        // ENTER — проверка кода
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CheckCode();
        }
    }

    private void SelectWheel(int idx)
    {
        if (idx < 0) idx = wheels.Length - 1;
        if (idx >= wheels.Length) idx = 0;
        activeWheelIndex = idx;
    }

    private void CheckCode()
    {
        
        if (wheels.Length < correctCode.Length) return;

        for (int i = 0; i < correctCode.Length; i++)
        {
            if (wheels[i].CurrentDigit != correctCode[i])
                return;
        }

        Debug.Log("Сейф открыт");
        
    }

    public void ExitMinigame()
    {
        isActive = false;

        if (safeMiniGameRoot != null) safeMiniGameRoot.SetActive(false);
        if (controlsHint != null) controlsHint.SetActive(false);

        
        if (proximity != null) proximity.ResetMinigameState();
    }
}
