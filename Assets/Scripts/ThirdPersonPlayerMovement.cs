using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonPlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 6f, jumpForce = 100f;
    [SerializeField] Transform cameraPivot, groundCheckPos;
    [SerializeField] Rigidbody rb;
    [SerializeField] LayerMask groundLayers;

    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if(Input.GetButtonDown("Jump"))
        {
            RaycastHit hit;
            if (Physics.Raycast(groundCheckPos.position, Vector3.down, out hit, 0.1f, groundLayers))
            {
                rb.AddForce(0, jumpForce, 0);
            }
        }

        // if player hits escape, go to main menu
        if(Input.GetButtonDown("Cancel"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }

    private void FixedUpdate()
    {
        // get the input direction and the camera direction
        Vector3 input = new Vector3(horizontal, 0, vertical);
        Vector3 camForward = cameraPivot.forward;
        Vector3 camRight = cameraPivot.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        if (input.magnitude > 0.1f)
        {
            Vector3 moveDirection = camForward * vertical + camRight * horizontal;

            rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
}
