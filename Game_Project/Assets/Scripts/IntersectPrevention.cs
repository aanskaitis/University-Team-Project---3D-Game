using UnityEngine;

/// <summary>
/// Author: Lee Taylor
/// 
/// This class prevents the player from placing a turret that
/// intersects with the level, which would form an unnatural appearance.
/// 
/// The level designer places invisible cube objects which are added to this
/// class' array field, solidObjects. When the player is placing a turret, each
/// object's bound is checked to determine whether it is intersecting with the turret.
/// </summary>

public class IntersectPrevention : MonoBehaviour {

    [Header("Clip objects")]
    public GameObject[] solidObjects; // Storage for invisible cubes
    public BoxCollider[] colliders; // Colliders from the cubes, public as another class must access these

    // Start is called before the first frame update
    void Start() {
        /* The collider from each cube is extracted and stored 
         * in colliders for later use */

        colliders = new BoxCollider[solidObjects.Length];

        for (int i = 0; i < solidObjects.Length; i++) {
            colliders.SetValue(((GameObject)solidObjects.GetValue(i)).GetComponent<BoxCollider>(), i);
        }

    }

    public BoxCollider[] getBoxColliders() {
        return this.colliders;
    }

    public void Append(BoxCollider boxCollider) {
        /* When the player places a turret, the turret is also added to the array
           of colliders to prevent intersection between turret and another turret.
           The array is expanded and another turret object collider is added. */

        int endPointer = colliders.Length;
        BoxCollider[] copy = new BoxCollider[endPointer + 1];

        int i = 0;
        foreach (BoxCollider bc in colliders) {
            copy.SetValue(bc, i);
            i++;
        }

        copy.SetValue(boxCollider, endPointer);
        colliders = (BoxCollider[])copy.Clone();

    }

}
