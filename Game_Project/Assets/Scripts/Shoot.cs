using UnityEngine;

/// <summary>
/// Author: Lee Taylor
/// 
/// Edited by: 
///     Jack Collins - Added sound effect
///     Adomas Anskaitis - Change weapon rotation
///     
/// This class contains the behaviour for the player to shoot.
/// This contains a calculated physics raycast from the plaer's point of view
/// to the centre of the screen, the crosshair. The collider the raycast hits 
/// if it's an enemy, the enemy loses health. 
/// </summary>s

public class Shoot : MonoBehaviour{

    // How much damage the player can output per shot
    public float playerDamage;

    // Used as origin point for player shooting
    public GameObject cam;
    private Transform camTransform;

    // Used as direction for player shooting
    public GameObject playerHead;
    private Transform headTransform;

    // Weapon position for rotation
    public Transform weaponPosition;
    public Transform Target;

    // Weapon shooting
    RaycastWeapon weapon;
    
    void Start() {
        // Grab transforms to prevent always doing extra operation -> gameObject.transform
        this.camTransform = cam.transform;
        this.headTransform = playerHead.transform;
        Cursor.lockState = CursorLockMode.Locked;
        weapon = GetComponentInChildren<RaycastWeapon>();
    }

    // Plays the shoot sound if the left mouse button is pressed
    void Update() {
            if (Input.GetMouseButtonDown(0))
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Laser", GetComponent<Transform>().position);
            }
        

        // Ray object a line from the player to where ever they're aiming 
        RaycastHit hit;

        // Rotate weapon towards aiming point
        Vector3 aimingPosition = Target.TransformPoint(Vector3.zero);
        weaponPosition.localRotation = Quaternion.Euler(0.0f, 95.0f, 90.0f - (aimingPosition.y * 2.5f));
        
        // Ray starts from camera, in the direction of player's head, has infinite range
        // Checks if ray collides with gameObject with tag "enemy"
        if (Physics.Raycast(this.camTransform.position, 
            this.headTransform.TransformDirection(Vector3.forward) * 1000, 
            out hit, 
            Mathf.Infinity)
            && hit.transform.tag == "Enemy") 
            {

            // If player clicks while aiming at an enemy gameObject then player can damage enemy
            if (Input.GetMouseButtonDown(0)) {
                hit.collider.gameObject.GetComponent<EnemyHealth>().damage(playerDamage);
            }

        }
        if (Input.GetButtonDown("Fire1"))
        {
            weapon.StartFiring();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            weapon.StopFiring();
        }

    }

}
