using UnityEngine;
using System.Collections;
using AClockworkBerry;

public class BouncingBall : MonoBehaviour {
    public float Acceleration = -1;
    public float HorizVelocity = 1;
    public float Wall = 5;
    public float Radius = 1;
    public float RotationSpeed = 1;

    private Vector3 Velocity;
    private float MaxVelocity;

	void Start () {
        Velocity = new Vector3(HorizVelocity, 0, 0);

        MaxVelocity = Mathf.Sqrt(2 * -Acceleration * (transform.position.y - Radius));
        transform.position = new Vector3(0, transform.position.y, 0);

        Debug.LogWarning("Start...");
	}
	
	void Update () {
        if (Input.GetButtonDown("Fire1"))            
            ScreenLogger.Instance.ShowLog = !ScreenLogger.Instance.ShowLog;

        Velocity.y += Acceleration * Time.deltaTime;
        transform.position += Velocity * Time.deltaTime;

        if (transform.position.y - Radius < 0)
        {
            transform.position = new Vector3(transform.position.x, Radius, 0);
            Velocity.y = MaxVelocity;

            Debug.Log("Boing at " + transform.position.x + "...");
        }

        if (Mathf.Abs(transform.position.x) + Radius > Wall)
        {
            transform.position = new Vector3(
                transform.position.x > 0 ? Wall - Radius : -Wall + Radius,
                transform.position.y,
                0);

            Velocity.x *= -1;
        }

        transform.Rotate(new Vector3(0, RotationSpeed * Time.deltaTime, 0));
	}
}
