using UnityEngine;

/// <summary>
/// Author: Lee Taylor
/// 
/// This class contains behaviour to enable the player to move the camera
/// when placing turrets.
/// </summary>

public class OverheadController : MonoBehaviour {

    public GameObject self; // Ref. to self to get transform position

    // Distance travelled by camera in x,z directions
    public float zChange = 0.2f;
    public float xChange = 0.2f;

    // Positional z bounds for the camera not to travel beyond
    public float zUpperLimit = -19.65f;
    public float zLowerLimit = -1.9f;

    // Positional x bounds for the camera not to travel beyond
    public float xUpperLimit = -2.65f;
    public float xLowerLimit = -9.22f;

    void Update() {
        /* This method listens for key strokes, and moves the camera depending on key pressed
         * and if the camera is within the specified bounds */
        if (Input.GetKey("w") && self.transform.position.z > zUpperLimit) {
            Vector3 newPos = self.transform.position;
            newPos.z -= zChange;
            self.transform.position = newPos;
        }
        else if (Input.GetKey("s") && zLowerLimit > self.transform.position.z) {
            Vector3 newPos = self.transform.position;
            newPos.z += zChange;
            self.transform.position = newPos;
        }
        else if (Input.GetKey("a") && self.transform.position.x < xUpperLimit) {
            Vector3 newPos = self.transform.position;
            newPos.x += xChange;
            self.transform.position = newPos;
        }
        else if (Input.GetKey("d") && self.transform.position.x > xLowerLimit) {
            Vector3 newPos = self.transform.position;
            newPos.x -= xChange;
            self.transform.position = newPos;
        }
    }

}
