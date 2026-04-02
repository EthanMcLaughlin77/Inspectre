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

    private void Start()
    {
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
        }

        Debug.Log($"Player left range");
    }
    private void StartDialogue()
    {
        if (dialogueRunner == null) return;

        isCurrentlyTalking = true;
        Debug.Log($"Now is talking");

        dialogueRunner.StartDialogue(dialogueNode);

        Debug.Log($"Started dialogue");

        playerMovement.DisablePlayerMovement();
    }

    private void OnDialogueComplete()
    {
        Debug.Log($"Dialogue Complete");
        isCurrentlyTalking = false;

        playerMovement.EnablePlayerMovement();
    }
}