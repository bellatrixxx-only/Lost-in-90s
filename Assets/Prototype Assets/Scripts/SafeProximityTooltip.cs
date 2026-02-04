using UnityEngine;

public class SafeProximityTooltip : MonoBehaviour
{
    [Header("World UI")]
    [SerializeField] private GameObject tooltipF;
    [SerializeField] private GameObject controlsHint;

    [Header("Safe UI")]
    [SerializeField] private GameObject safeMiniGame;                 
    [SerializeField] private SafeMiniGameController miniGameController; 

    [Header("Player")]
    [SerializeField] private WalkScript playerMovement;

    private bool playerInRange;
    private bool inMinigame;

    private void Awake()
    {
        if (tooltipF != null) tooltipF.SetActive(false);
        if (controlsHint != null) controlsHint.SetActive(false);
        if (safeMiniGame != null) safeMiniGame.SetActive(false);

        if (miniGameController != null) miniGameController.enabled = false;
    }

    private void Update()
    {
      
        if (inMinigame)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                ResetMinigameState();
            return;
        }

        
        if (!playerInRange) return;

        if (Input.GetKeyDown(KeyCode.F))
            EnterMinigame();
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

        if (tooltipF != null) tooltipF.SetActive(false);
        if (controlsHint != null) controlsHint.SetActive(true);

        if (safeMiniGame != null) safeMiniGame.SetActive(true);

        if (miniGameController != null) miniGameController.enabled = true;

        if (playerMovement != null) playerMovement.SetBlocked(true);

        Debug.Log("Вход в мини-игру сейфа");
    }

    public void ResetMinigameState()
    {
        inMinigame = false;

        if (controlsHint != null) controlsHint.SetActive(false);

        if (safeMiniGame != null) safeMiniGame.SetActive(false);

        if (miniGameController != null) miniGameController.enabled = false;

        if (playerMovement != null) playerMovement.SetBlocked(false);

      
        if (playerInRange && tooltipF != null)
            tooltipF.SetActive(true);

        Debug.Log("Выход из мини-игры сейфа");
    }
}
