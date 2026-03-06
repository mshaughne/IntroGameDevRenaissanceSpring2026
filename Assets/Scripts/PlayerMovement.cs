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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

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
            */

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
}