using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] float mouseSensitivity = 200f;
    //[SerializeField] float distance = 4f;

    float xRot;
    float yRot;

    // Start is called before the first frame update
    void Start()
    {
        // lock the cursor to the center of the game screen
        Cursor.lockState = CursorLockMode.Locked;
        // make the cursor invisible
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRot -= mouseY;
        yRot = yRot + mouseX;

        xRot = Mathf.Clamp(xRot, -80f, 80f);

        transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        player.rotation = Quaternion.Euler(0, yRot, 0);
    }
}
