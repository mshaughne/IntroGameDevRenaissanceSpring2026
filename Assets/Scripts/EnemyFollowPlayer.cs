using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowPlayer : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }
}
