using UnityEngine;

/// <summary>
/// Author: Mark Nicholson
/// 
/// This class contains the behaviour to control the player's aim.
/// This moves the player's shoulder camera.
/// 
/// Credit original work: https://www.youtube.com/watch?v=hb9FoFEFR3M&ab_channel=TheKiwiCoder
/// Author YouTube Username: TheKiwiCoder
/// </summary>

public class playerAim : MonoBehaviour
{
    public float turnspeed = 15;
    public float aimDuration = 0.3f;
    public Transform cameraLookAt;
    public Transform playerTurn;
    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;

    void Update()
    {
   
        xAxis.Update(Time.fixedDeltaTime);
        yAxis.Update(Time.fixedDeltaTime);
        playerTurn.eulerAngles = new Vector3(0, xAxis.Value, 0);
        cameraLookAt.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);
    
    }
}
