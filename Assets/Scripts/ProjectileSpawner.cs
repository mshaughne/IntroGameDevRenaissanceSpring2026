using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileForce = 100f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            GameObject proj = Instantiate(projectilePrefab,
                transform.position, transform.rotation);

            proj.GetComponent<Rigidbody>()
                .AddRelativeForce(Vector3.up * projectileForce, ForceMode.Impulse);
        }
    }
}
