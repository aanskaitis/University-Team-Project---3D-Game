using UnityEngine;

/// <summary>
/// Author: Lee Taylor
/// 
/// This class contains the behaviour for a turret.
/// </summary>

public class Turret : MonoBehaviour
{

    public GameObject self; // Ref. to parent object
    public GameObject bullet; // Created when firing at enemy
    public int cost; // How much money the turret costs to place

    // The points from which the turret shoots from
    public Transform firePoint1;
    public Transform firePoint2;
    public Transform firePoint3;
    public Transform firePoint4;

    private GameObject sensorFollow; // Part of the turret to follow 
    private GameObject sensorPoint; // Point from which the turret shoots a physics raycast
    protected GameObject partToRotate; // Part of the turret to rotate for during shooting
    private Ray sensorRay; // Physics ray used to detect enemies to shoot at

    public float rotationSpeed = 5f; // Speed at which the turret rotates
    public float fireRate = 1f; // How often the turret shoots bullets
    protected float shotTime; // Last time the turret shot

    public float startX;
    private bool direction = true;
    protected bool combat = false;
    protected GameObject enemyRef;

    void Start() {

        partToRotate = self.transform.GetChild(1).gameObject;
        sensorPoint = self.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject;
        sensorFollow = self.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;

        sensorFollow.transform.parent = partToRotate.transform.parent;
        startX = sensorFollow.transform.position.x;

        shotTime = Time.time;

    }

    protected void LookMode() {

        // This block controls moving the point to follow left to right
        // Each if block moves the followPoint a small amount based on the current direction
        if (Mathf.Abs(startX - sensorFollow.transform.position.x) < 2.0f) {
            if (direction) {
                Vector3 nextPos = sensorFollow.transform.position;
                nextPos.x += 0.005f;
                nextPos.z += 0.005f;
                sensorFollow.transform.position = nextPos;
            }
            else {
                Vector3 nextPos = sensorFollow.transform.position;
                nextPos.x -= 0.005f;
                nextPos.z -= 0.005f;
                sensorFollow.transform.position = nextPos;
            }
        }
        else {
            if (direction) {
                Vector3 nextPos = sensorFollow.transform.position;
                nextPos.x -= 0.005f;
                nextPos.z -= 0.005f;
                sensorFollow.transform.position = nextPos;
            }
            else {
                Vector3 nextPos = sensorFollow.transform.position;
                nextPos.x += 0.005f;
                nextPos.z += 0.005f;
                sensorFollow.transform.position = nextPos;
            }
            direction = !direction;
        }

        // This sets the rotation of the barrel of the turret to face the point to follow
        Vector3 _direction = (sensorFollow.transform.position - partToRotate.transform.position).normalized;
        Quaternion _directionRotation = Quaternion.LookRotation(_direction);
        Quaternion newRotation = Quaternion.Slerp(partToRotate.transform.rotation, _directionRotation, Time.deltaTime * rotationSpeed);

        // This prevents the turret from rotating on it's x and z axis
        newRotation.x = 0f;
        newRotation.z = 0f;

        // Set the rotation of the turret to the new rotation
        partToRotate.transform.rotation = newRotation;

    }
    
    protected void SensorRay() {
        // A ray is casted out of the sensorPoint, in the direction of the barrel
        // if the ray collides with an enemy the turret enters combat mode

        // Origin point and direction of ray
        sensorRay.origin = sensorPoint.transform.position;
        sensorRay.direction = partToRotate.transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(sensorRay, out hit, 100f)) {

            // Checks if the ray hit an enemy
            if (hit.collider.tag == "Enemy") {
                combat = true;
                enemyRef = hit.collider.gameObject;
            } else {
                combat = false;
                enemyRef = null;
            }

        }

    }

    virtual public void CombatMode() {
        // The enemy is tracked and the partToRotate of the turret is adjusted to follow the enemy

        // This sets the rotation of the barrel of the turret to face the point to follow
        Vector3 _direction = (enemyRef.transform.position - partToRotate.transform.position).normalized;
        Quaternion _directionRotation = Quaternion.LookRotation(_direction);
        Quaternion newRotation = Quaternion.Slerp(partToRotate.transform.rotation, _directionRotation, Time.deltaTime * rotationSpeed);

        // This prevents the turret from rotating on it's x and z axis
        newRotation.x = 0f;
        newRotation.z = 0f;

        // Set the rotation of the turret to the new rotation
        partToRotate.transform.rotation = newRotation;

        if ((Time.time - shotTime) > fireRate) {

            // A bullet at each fire point is created and sent in a straight line
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Turret", GetComponent<Transform>().position);
            Instantiate(bullet, firePoint1.transform.position, firePoint1.transform.rotation);
            Instantiate(bullet, firePoint2.transform.position, firePoint2.transform.rotation);
            Instantiate(bullet, firePoint3.transform.position, firePoint3.transform.rotation);
            Instantiate(bullet, firePoint4.transform.position, firePoint4.transform.rotation);

            // Record time shot at to calculate next time to shoot
            shotTime = Time.time;

        }

    }

    virtual public void Update() {

        // Send out physics ray to check for enemies
        SensorRay();

        // No enemy and not in combat then carry on looking
        if (enemyRef == null && !combat) {
            LookMode();
        } // Otherwise enter combat mode 
        else {
            CombatMode();
        }


    }

}
