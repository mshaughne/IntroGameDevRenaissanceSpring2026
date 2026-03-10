using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Physics and Movement")]
    [SerializeField] Rigidbody rb;
    Vector3 playerInput;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 10f;

    [Header("Ground Detection")]
    [SerializeField] Transform checkPos;
    [SerializeField] float checkRadius = 1f;
    [SerializeField] LayerMask groundLayers;

    [Header("Camera")]
    [SerializeField] Transform cameraTransform;
    [SerializeField] float rotSpeed;
    Vector3 lastMoveDir;
    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // if we get the jump button, add force up
        if (Input.GetButtonDown("Jump"))
        {
            // where all of the raycast collision data is stored
            RaycastHit hit;
            if(Physics.Raycast(checkPos.position, Vector3.down, out hit, checkRadius, groundLayers))
            {
                rb.AddForce(0, jumpForce, 0);
            }
        }
    }

    private void FixedUpdate()
    {
        // get the rotation vectors of the camera
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        // remove y axis rotation
        camForward.y = 0;
        camRight.y = 0;

        // renormalize the magnitude
        camForward.Normalize();
        camRight.Normalize();

        // get move direction based on camera direction and inputs
        Vector3 moveDir = camForward * vertical + camRight * horizontal;

        // if we're moving at all
        if (moveDir.magnitude > 0.1f)
        {
            lastMoveDir = moveDir;
            Quaternion targetRot = Quaternion.LookRotation(moveDir);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed);
        }

        Vector3 newVelocity = moveDir.normalized * moveSpeed;

        rb.velocity = new Vector3(newVelocity.x, rb.velocity.y, newVelocity.z);
    }

    /*
    // Update is called once per frame
    void Update()
    {
        //rb.AddForce(Vector3.left * 5f * Time.deltaTime);

        //playerInput = new Vector3(Input.GetAxis("Horizontal"),
        //    0, Input.GetAxis("Vertical"));

        // setting the velocity manually
        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed,
            rb.velocity.y, Input.GetAxis("Vertical") * moveSpeed);

        // if we get the jump button, add force up
        if (Input.GetButtonDown("Jump"))
        {
            /*
            if (Physics.CheckSphere(checkPos.position, checkRadius, groundLayers))
            {
                rb.AddForce(0, jumpForce, 0);
            }
            *

            // where all of the raycast collision data is stored
            RaycastHit hit;
            if(Physics.Raycast(checkPos.position, Vector3.down, out hit, checkRadius, groundLayers))
            {
                rb.AddForce(0, jumpForce, 0);
            }
        }
    }

    private void FixedUpdate()
    {
        //rb.AddForce(Vector3.left * 5f);

        //rb.AddForce(playerInput * moveSpeed);
    }
    */
        }