using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Adomas Anskaitis
/// 
/// This class contains behaviour for the enemy 
/// to follow their waypoints.
/// </summary>

public class FollowWP : MonoBehaviour
{
    public GameObject[] waypoints;
    int currentWP = 0;
    Animator animator;

    public float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the enemy is close enough to the waypoint,
        // it can start walking towards next waypoint
        if (Vector3.Distance(this.transform.position, waypoints[currentWP].transform.position) < 1)
        {
            currentWP++;
            animator.SetInteger("waypoint", currentWP);
        }

        if (currentWP < waypoints.Length)
        {
            this.transform.LookAt(waypoints[currentWP].transform);
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }

    }
}
