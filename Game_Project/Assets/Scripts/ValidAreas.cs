using UnityEngine;

/// <summary>
/// Author: Lee Taylor
/// 
/// This class contains behaviour to control the green/valid zones for the turrets.
/// The zones are stored here, and there is a method to set the active state of those objects.
/// This is to make them visible/invisible corresponding to the current mode the user is in.
/// </summary>

public class ValidAreas : MonoBehaviour {

    // Stores objects which can contain a turret atop
    public GameObject[] validZones;
    public IntersectPrevention intersectPrevention;

    // Method used to change visibility of turret zones
    public void setValidZones(bool state) {

        foreach (GameObject obj in validZones) {
            obj.SetActive(state);
        }

        foreach (Collider obj in intersectPrevention.colliders) {
            obj.enabled = state;
        }

    }

    // Start is called before the first frame update
    void Start() {

        setValidZones(false);

    }

    
}
