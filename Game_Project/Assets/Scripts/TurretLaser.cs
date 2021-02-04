using UnityEngine;

/// <summary>
/// Author: Lee Taylor
/// 
/// This inherits the behaviour from the turret class.
/// But the method to shoot is over written with functionality 
/// for a laser.
/// </summary>

public class TurretLaser : Turret {

    [Header("Laser Turret")]
    public LineRenderer laser; // Laser line effect
    public float damage = 1f; // Damage dealt to enemy

    void Awake() {
        // Set laser to line rendered object
        laser = gameObject.GetComponent<LineRenderer>();
    }

    override public void CombatMode() {

        // This sets the rotation of the barrel of the turret to face the point to follow
        Vector3 _direction = (enemyRef.transform.position - partToRotate.transform.position).normalized;
        Quaternion _directionRotation = Quaternion.LookRotation(_direction);
        Quaternion newRotation = Quaternion.Slerp(partToRotate.transform.rotation, _directionRotation, Time.deltaTime * rotationSpeed);

        // This prevents the turret from rotating on it's x and z axis
        newRotation.x = 0f;
        newRotation.z = 0f;

        // Set the rotation of the turret to the new rotation
        partToRotate.transform.rotation = newRotation;

        // Enable line renderer if not enabled
        if (!laser.enabled) {
            laser.enabled = true;
        }

        // If line renderer enabled, play sound effect
        if (laser.enabled = true)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Laser_Turret", GetComponent<Transform>().position);
        }

        // Set origin point for laser (lineRenderer)
        laser.SetPosition(0, firePoint1.transform.position);

        // Calculate and set end point for laser (lineRenderer)
        Vector3 endPoint = enemyRef.transform.position;
        endPoint.y = firePoint1.transform.position.y;
        laser.SetPosition(1, endPoint);

        // If enough time has elapsed the turret may shoot and damage an enemy
        if ((Time.time - shotTime) > fireRate) {
            enemyRef.GetComponent<EnemyHealth>().damage(damage);
            shotTime = Time.time;
        }

    }

    override public void Update() {
        // Send out physics ray to check for enemies
        SensorRay();

        // No enemy and not in combat then carry on looking
        if (enemyRef == null && !combat) {
            laser.enabled = false;
            LookMode();
        }
        // Otherwise enter combat mode 
        else {
            CombatMode();
        }

    }

}
