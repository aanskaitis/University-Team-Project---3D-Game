using UnityEngine;

/// <summary>
/// 
/// Author: Lee Taylor
/// 
/// This class contains the behaviour for a bullet projectile object. 
/// Bullets are created from turrets, and travel in a straight line. 
/// When they collide with an enemy, the enemy's health is decreased. 
/// This simulates being shot.
/// 
/// </summary>

public class Bullet : MonoBehaviour {

    public GameObject self; // Used to destroy this object 
    public float Speed = 1f; // Speed of the bullet

    void Update() {
        /*The bullet is moved forward, after 5 seconds the bullet is garbage collected as by this time 
        the bullet has travelled out of the level*/

        self.gameObject.transform.position += self.gameObject.transform.forward * Speed * Time.deltaTime;
        Destroy(self, 5f);

    }

    private void OnTriggerEnter(Collider col) {
        /* When the bullet has collided with another object, col, it's tag attribute is checked
         * if the tag is "enemy" then the enemy's health is decreased and the bullet is destroyed */

        if (col.gameObject.GetComponent<Collider>().tag == "Enemy") {
            col.gameObject.GetComponent<EnemyHealth>().damage(10);
            Destroy(self, 0.5f);
        }

    }

}
