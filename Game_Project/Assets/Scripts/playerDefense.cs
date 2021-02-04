using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDefense : MonoBehaviour
{
    // camera effects
    public GameObject effects;

    public bool immortal;

    private bool cooldown = false;

    // Update is called once per frame
    void Update()
    {
        bool rightMouseClick = Input.GetMouseButtonDown(1);

        if (rightMouseClick)
        {
            if (!cooldown)
            {
                Debug.Log("hello");
                immortal = true;
                effects.SetActive(true);
                cooldown = true;
                StartCoroutine(Immortal());
                StartCoroutine(Cooldown());
            }
        }
        
    }

    IEnumerator Immortal()
    {
        //yield on a new YieldInstruction that waits for 4 seconds.
        yield return new WaitForSecondsRealtime(4);

        effects.SetActive(false);
        immortal = false;
    }

    IEnumerator Cooldown()
    {
        //yield on a new YieldInstruction that waits for 10 seconds.
        // the functions will run simultanuously, thus real cooldown is (14 - 4) = 10 seconds
        yield return new WaitForSecondsRealtime(14);

        cooldown = false;
    }
}
