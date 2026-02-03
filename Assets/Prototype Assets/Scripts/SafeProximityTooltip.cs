using UnityEngine;

public class SafeProximityTooltip : MonoBehaviour
{
    [Header("World UI")]
    [SerializeField] private GameObject tooltipF;
    [SerializeField] private GameObject controlsHint;

    private bool playerInRange;
    private bool inMinigame;

    private void Awake()
    {
        if (tooltipF != null) tooltipF.SetActive(false);
        if (controlsHint != null) controlsHint.SetActive(false);
    }

    private void Update()
    {
        if (!playerInRange) return;
        if (inMinigame) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            EnterMinigame();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;
        if (!inMinigame && tooltipF != null)
            tooltipF.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;
        if (tooltipF != null) tooltipF.SetActive(false);
    }

    private void EnterMinigame()
    {
        inMinigame = true;

        if (tooltipF != null)
            tooltipF.SetActive(false);

        if (controlsHint != null)
            controlsHint.SetActive(true);

        Debug.Log("¬ход в мини-игру сейфа");
    }
}
