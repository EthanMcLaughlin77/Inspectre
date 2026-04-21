using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float mouseSensitivity = 2f;

    [SerializeField] private Vector3 moveDirection;
    private float rotationY;

    private PlayerInput playerInput;

    private bool wasMovementDisabled = true;

    // Update is called once per frame

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerInput = player.GetComponent<PlayerInput>();
        }

        /*Camera.main.transform.rotation = Quaternion.Euler(0,0,0);
        transform.rotation = Quaternion.Euler(0,0,0);*/

        //player.transform.rotation = Camera.main.transform.rotation;
    }
    void Update()
    {
        HandleMovement();
        HandleRotation();

        if (moveDirection != Vector3.zero)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void HandleMovement()
    {
        if (wasMovementDisabled) return;
        /*Debug.Log($"Movement disabled");
        if (wasMovementDisabled) return;
        wasMovementDisabled = true;
        Debug.Log($"Movement enabled");*/

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        if (wasMovementDisabled) return;
        float mouseX = Input.GetAxis("Mouse X");
        rotationY += mouseX * mouseSensitivity;

        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
    }

    public void DisablePlayerMovement()
    {
        if (wasMovementDisabled) return;

        wasMovementDisabled = true;

        if (playerInput != null)
        {
            playerInput.enabled = false;
        }

        Debug.Log($"Player movement disabled for dialogue");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Debug.Log($"Interacted with wall, movement disabled.");
            speed = 0.1f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Debug.Log($"Movement reenabled.");
            speed = 4f;
        }
    }

    public void EnablePlayerMovement()
    {
        if (!wasMovementDisabled) return;

        wasMovementDisabled = false;

        if (playerInput != null)
        {
            playerInput.enabled = true;
        }

        Debug.Log($"Player movement enabled after dialogue");
    }
}
