using UnityEngine;

/// <summary>
/// 
/// Author: Adomas Anskaitis 
/// Edited by: Lee Taylor (Added enemy hits player and point to defend)
/// 
/// This class contains the behaviour for enemy movement. 
/// From one point to another, and hitting players.
/// 
/// </summary>

public class enemyMovement : MonoBehaviour
{

    public PointToDefend pointToDestroy;
    public int damage = 20;

    public GameObject[] waypoints;
    int currentWP = 0;
    Animator animator;
    CharacterController controller;
    public Transform player;
    private PlayerStats playerStats;
    private playerDefense playerDefense;

    public float speed = 2.0f;
    public float rotSpeed = 10.0f;
    public float gravity = 10.0f;

    private float timeStartAttack;
    private float ptdHitTime;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        playerStats = player.GetComponent<PlayerStats>();
        playerDefense = player.GetComponent<playerDefense>();
        ptdHitTime = Time.time;
    }

    // Update is called once per frame
    void Update() {
        
        Vector3 direction = player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);
        float distance = Vector3.Distance(this.transform.position, player.position);

        // Damage player check
        if (distance < 1.5f && angle < 50.0f &&
            animator.GetCurrentAnimatorStateInfo(0).IsName("AI Punch") &&
            !playerDefense.immortal &&
            (Time.time - timeStartAttack) >= 0.8f) {

                Debug.Log("Enemy hit player");
                playerStats.damagePlayer(damage);
                timeStartAttack = Time.time;

        }

        // Waypoint increase check
        if (currentWP < waypoints.Length) {
            if (Vector3.Distance(this.transform.position, waypoints[currentWP].transform.position) < 1) {
                currentWP++;
                animator.SetInteger("waypoint", currentWP);
            }
        }

        // Enemy walking animation check and set
        if (currentWP < waypoints.Length && !(distance < 1.5f && angle < 50.0f)) {
            
            animator.ResetTrigger("isAttacking");
            animator.SetBool("isWalking", true);

            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("AI Punch")) {
                
                timeStartAttack = Time.time;
                Quaternion lookatWP = Quaternion.LookRotation(waypoints[currentWP].transform.position - this.transform.position);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookatWP, rotSpeed * Time.deltaTime);
                Vector3 move = Vector3.forward;
                move = transform.TransformDirection(move);
                move.y -= gravity * Time.deltaTime;
                controller.Move(move * Time.deltaTime * speed);
            }
           
        }

        // Enemy hitting animation check and set
        else if (currentWP < waypoints.Length) {
            
            animator.SetBool("isWalking", false);
            animator.SetTrigger("isAttacking");
            
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
                Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);

            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("AI Punch")) {
                timeStartAttack = Time.time;
            }

        }

        // Enemy at PTD check and react
        else if (Vector3.Distance(this.transform.position, pointToDestroy.transform.position) < 6f &&
            (Time.time - ptdHitTime) > 1.6f) {

            animator.SetBool("isWalking", false);
            animator.SetTrigger("isAttacking");
            pointToDestroy.Damage(6);
            ptdHitTime = Time.time;

        }

    }
}
