using UnityEngine;

/// <summary>
/// Author: Lee Taylor
/// 
/// This class handles the turret placing mode. Specifically: 
///     - Converting the mouse pointer to a world point in the level
///     - Rendering a turret under the mouse
///     - Changing the material of the turret to indicate if the zone is valid or not
///     - Left-Clicking a valid point places the turret at that point
///     - Once a turret is placed, the player may rotate the turret
///     - A player may cancel rotating or placing a turret by right-clicking
///     
/// Note: Each turret has an invisible cube object in which two rays (lines) are casted
/// downwards and collides with the level. The objects the rays collide with are stored 
/// and their 'tag' attribute is checked for a certain string. If both ray collsions return 
/// a certain string then that zone is valid.
/// 
/// </summary>

public class MouseInput : MonoBehaviour
{

    Ray ray;

    public EnemyManager enemyManager;  // This manager is disabled when MouseInput is enabled and vice versa
    public GameObject camera; // Used to acquire the cameraPoint
    public GameObject playerObject; // Used to acquire playerStats
    private PlayerStats playerStats; // Used to affect the amount of money the player has

    private GameObject turret; // Stores the object under the mouse

    private Vector3 clickPosition; // Stores the world point the player clicked on
    private GameObject[] placedTurrets = new GameObject[32]; // Stores turrets
    private int placedTurretsPointer = 0; // Pointer for placedTurrets

    // Rays for validity function
    private Transform zoneObject; // Invisible rectangle in the turret
    private Ray BLRay; // BottomLeft downwards raycast from the zoneObject
    private Ray TRRay; // TopRight downwards raycast from the zoneObject

    private bool validPlace = false; // Indicates whether the player has found a valid place for the turret
    public Material valid; // The material to set the turret to when valid
    public Material invalid; // The material to set the turret to when invalid

    // Material component 
    public Material baseMaterial; // material to set the turret once fully placed 
    public Material barrelMaterial; // material to set the turret once fully placed
    private GameObject baseObject; // child object 'base' of turret
    private MeshRenderer baseMesh; // material of child object 'base'
    private GameObject barrelObject; // child object 'barrel' of turret
    private MeshRenderer barrelMesh; // material of child object 'barrel'

    public IntersectPrevention intersectPrevention; // list of colliders used from here
    private bool rotatingTurret = false; // indicates the current status 
    private GameObject turretToRotate; // turret placed that is to be rotated is stored here
    private GameObject turretToRotateBarrel; // barrel of the turret to rotate is stored here
    private BoxCollider[] tColliders = new BoxCollider[2]; // colliders of the current turret to place/rotate 

    public ValidAreas validZones; // object stores green zones, with method to enable/disable them
    public GameObject self; // ref. to self to disable when necessary

    public GameObject moneyPrompt;

    public bool getRotatingTurret() {
        return rotatingTurret;
    }

    public GameObject getTurret() {
        return turret;
    }

    public void setTurret(GameObject _turret) {
        turret = _turret;
    }

    public void InitTurretUnderMouse() {

        // zoneObject is the invisible platform inside the turret
        zoneObject = turret.transform.Find("ValidZone");

        // Get child objects of the turret
        baseObject = turret.transform.GetChild(0).gameObject;
        barrelObject = turret.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;

        // Get material components of the above child objects
        baseMesh = baseObject.GetComponent<MeshRenderer>();
        barrelMesh = barrelObject.GetComponent<MeshRenderer>();

        // Init. ray's used for validating placement of the turret
        TRRay = new Ray(new Vector3(0, 0, 0), Vector3.down);
        BLRay = new Ray(new Vector3(0, 0, 0), Vector3.down);

    }

    void Start() {

        playerStats = playerObject.GetComponent<PlayerStats>();

    }

    private bool IsValidTZone() {

        // Update bottom-left BL ray pos
        Vector3 zoneRayCastBL = zoneObject.position;
        zoneRayCastBL.z -= zoneObject.transform.localScale.z / 2;
        zoneRayCastBL.x -= zoneObject.transform.localScale.x / 2;

        // Update top-right TR ray pos
        Vector3 zoneRayCastTR = zoneObject.position;
        zoneRayCastTR.z += zoneObject.transform.localScale.z / 2;
        zoneRayCastTR.x += zoneObject.transform.localScale.x / 2;

        // Bottom left ray
        BLRay.origin = zoneRayCastBL;
        RaycastHit hitBL;

        // Top right ray
        TRRay.origin = zoneRayCastTR;
        RaycastHit hitTR;

        /* 
         * Checks if both rays are colliding with the level
         * Checks tag of object of bottom left ray
         * Checks tag of object of top right ray
         * Sets the material of the floating turret to green
         */
        if (Physics.Raycast(BLRay, out hitBL) && Physics.Raycast(TRRay, out hitTR)) {
            if ("TurretZone".Equals(hitBL.collider.tag)) {
                if ("TurretZone".Equals(hitTR.collider.tag)) {
                    Debug.Log(hitTR.collider.tag);
                    baseMesh.material = valid;
                    barrelMesh.material = valid;
                    return true;
                }
            } 
        } 
        
        // Failing checks sets the materials to red
        baseMesh.material = invalid;
        barrelMesh.material = invalid;
        return false;

    }

    private bool checkIntersect() {
        /* The player may rotate a placed turret - this method
         * prevents rotating the turret into a position intersecting 
         * another object. 
         * If intersection return false otherwise return true. */

        // Change it so the GetChild methods are called only once
        GameObject obj0 = turretToRotate.transform.GetChild(0).gameObject;
        GameObject obj1 = turretToRotate.transform.GetChild(1).gameObject;

        // Checks colliders of turret and solids for intersection
        foreach (BoxCollider tBox in tColliders) {
            foreach (BoxCollider solid in intersectPrevention.colliders) {
                if (tBox.bounds.Intersects(solid.bounds)) {

                    // Change it so the GetChild methods are called only once
                    obj0.GetComponent<MeshRenderer>().material = invalid;
                    obj1.GetComponent<MeshRenderer>().material = invalid;

                    return false;
                }
            }
        }

        // Change it so the GetChild methods are called only once
        obj0.GetComponent<MeshRenderer>().material = valid;
        obj1.GetComponent<MeshRenderer>().material = valid;

        return true;

    }

    private void cancelTurretRotation() {
        placedTurretsPointer--;
        placedTurrets.SetValue(null, placedTurretsPointer);
        Destroy(turretToRotate);
        rotatingTurret = false;
    }

    private void cancelTurretPlacement() {

        if (turret != null) {
            // Remove turret under mouse - Set mouse under turret to null
            Destroy(turret.gameObject);
            turret = null;
        }

    }

    private void RotatePlacedTurret() {
        /* After clicking to place a turret the player
         * rotate the turret - this method enables this feature */

        // Amount of rotation applied, direction to face, angle to change by, calculated angle
        float RotationSpeed = 100f;  
        Vector3 _direction = (clickPosition - turretToRotateBarrel.transform.position).normalized;
        Quaternion _lookRotation = Quaternion.LookRotation(_direction);
        Quaternion newR = Quaternion.Slerp(turretToRotateBarrel.transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
        
        // X and Z rotation set to 0 to prevent floating and incorrect rotations 
        newR.x = 0f;
        newR.z = 0f;

        // New rotation applied
        // turretToRotate.transform.rotation = newR;
        turretToRotateBarrel.transform.rotation = newR;

        // Check if turret is inside another object i.e a wall
        if (checkIntersect()) {
            if (Input.GetMouseButtonDown(0) == true) {
                
                rotatingTurret = false;
                GameObject validZone = turretToRotate.transform.GetChild(2).gameObject;
                intersectPrevention.Append(validZone.GetComponent<BoxCollider>());
                turretToRotate.GetComponent<Turret>().enabled = true;

                turretToRotate.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = baseMaterial;
                turretToRotate.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = barrelMaterial;

                playerStats.money -= turretToRotate.GetComponent<Turret>().cost;
                playerStats.updateMoneyText();

                return;
        
            } else if (Input.GetMouseButtonDown(1) == true) {
                Debug.Log("Cancel Rotation");
                cancelTurretRotation();
                return;
            } 
        } else if (Input.GetMouseButtonDown(1) == true) {
            Debug.Log("Cancel Rotation");
            cancelTurretRotation();
            return;
        }

        return;
    }

    private void switchModeToCombat() {
        Debug.Log("Round ended");
        self.SetActive(false);
        enemyManager.playerObject.SetActive(true);
        validZones.setValidZones(false);
        enemyManager.setPressedStart(true);
        return;
    }

    private void handleStartNextRound() {

        // Disables any turret that was under the mouse
        if (turret != null) {
            turret.SetActive(false);
        }

        // Deletes any turret that was not finished rotating
        if (rotatingTurret) {
            cancelTurretRotation();
        }

        switchModeToCombat();
    }

    private void handlePlaceTurret() {

        // Init. new turret object at mouse position
        turretToRotate = Instantiate(turret);

        // Get turret objects
        GameObject baseObject = turretToRotate.transform.GetChild(0).gameObject;
        GameObject barrelObject = turretToRotate.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
        GameObject validZone = turretToRotate.transform.GetChild(2).gameObject;

        // Enable collisions on each object
        validZone.GetComponent<BoxCollider>().enabled = true;
        baseObject.GetComponent<BoxCollider>().enabled = true;
        barrelObject.GetComponent<BoxCollider>().enabled = true;

        // Increase size of invisible zone around placed turret
        Vector3 newSca = validZone.transform.localScale;
        newSca.x = 3f;
        newSca.z = 3f;
        validZone.transform.localScale = newSca;

        // Store turret box colliders for prevention of rotation intersection
        tColliders.SetValue(baseObject.GetComponent<BoxCollider>(), 0);
        tColliders.SetValue(barrelObject.GetComponent<BoxCollider>(), 1);

        // Set the position of the turret placed
        turretToRotate.transform.position = clickPosition;

        // Store placed turret, point to next empty index
        placedTurrets.SetValue(turretToRotate, placedTurretsPointer);
        placedTurretsPointer++;

        // Begin rotating turret
        rotatingTurret = true;
        turretToRotateBarrel = turretToRotate.transform.GetChild(1).gameObject;

        // Remove turret under mouse - focus on new stuck in place turret to rotate
        Destroy(turret.gameObject);

        // Set mouse under turret to null
        turret = null;

    }

    void Update() {

        // Player may press G to start next round during build phase
        if (Input.GetKeyDown((KeyCode)103)) {
            moneyPrompt.SetActive(false);
            handleStartNextRound();
            return;
        }

        if (Input.GetMouseButtonDown(1) == true && !rotatingTurret) {
            cancelTurretPlacement();
            return;
        }

        // Ray object created from mouse position
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Checks if ray object collides with the level 
        if (Physics.Raycast (ray, out hit)) {

            // Render turret under mouse
            clickPosition = hit.point;
            // clickPosition.y += 0.6f;

            // Enables the user to rotate the turret
            if (rotatingTurret) {
                RotatePlacedTurret();
                return;
            }

            // If there is no turret under the mouse then exit
            if (turret == null) {
                return;
            }

            turret.transform.position = clickPosition;

            if (!IsValidTZone()) {
                return;
            }

            // Checks mouse is clicked and in valid zone
            if (Input.GetMouseButtonDown(0) == true) {

                handlePlaceTurret();

            }

        }

    }

}
