using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    //[SerializeField] private string dialogueNode = "NPC_Talk";
    [SerializeField] private string dialogueNode;
    [SerializeField] private Key interactionKey = Key.E;

    private bool playerInRange = false;
    public bool isCurrentlyTalking = false;
    private PlayerInput playerInput;
    public PlayerMovement playerMovement;

    [SerializeField] private GameObject interactionIndicator;

    private void Start()
    {
        if (interactionIndicator != null)
        {
            interactionIndicator.SetActive(false);
        }

        if (dialogueRunner == null)
        {
            dialogueRunner = FindFirstObjectByType<DialogueRunner>();
            if (dialogueRunner == null)
            {
                Debug.Log($"Dialogue runner not found");
            }
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerInput = player.GetComponent<PlayerInput>();

            if (dialogueRunner == null)
            {
                Debug.Log($"No dialogue runner found");
            }
        }

        if (dialogueRunner != null)
        {
            dialogueRunner.onDialogueComplete.AddListener(OnDialogueComplete);
        }
    }

    private void OnDestroy()
    {
        if (dialogueRunner != null)
        {
            dialogueRunner.onDialogueComplete.RemoveListener(OnDialogueComplete);
        }
    }

    private void Update()
    {
        if (playerInRange && !isCurrentlyTalking && !dialogueRunner.IsDialogueRunning)
        {
            if (Keyboard.current[interactionKey].wasPressedThisFrame)
            {
                Debug.Log($"Dialogue attempted.");
                StartDialogue();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            //Get rid of this bit if it doesn't work
            if (!dialogueRunner.IsDialogueRunning && interactionIndicator != null)
            {
                interactionIndicator.SetActive(true);
            }
        }

        /*if (playerInput != null)
        {

        }*/

        Debug.Log($"Player entered range");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            //Get rid of this bit if it doesn't work
            if (interactionIndicator != null)
            {
                interactionIndicator.SetActive(false);
            }
        }

        Debug.Log($"Player left range");
    }
    private void StartDialogue()
    {
        if (dialogueRunner == null) return;

        isCurrentlyTalking = true;
        Debug.Log($"Now is talking");

        //Get rid of this bit below if it doesn't work
        if (interactionIndicator != null)
        {
            interactionIndicator.SetActive(false);
        }

        dialogueRunner.StartDialogue(dialogueNode);

        Debug.Log($"Started dialogue");

        playerMovement.DisablePlayerMovement();
    }

    private void OnDialogueComplete()
    {
        Debug.Log($"Dialogue Complete");
        isCurrentlyTalking = false;

        //Get rid of this bit below if it doesn't work
        if (playerInRange && interactionIndicator != null)
        {
            interactionIndicator.SetActive(true);
        }

        playerMovement.EnablePlayerMovement();
    }
}