using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControls : MonoBehaviour
{

    private float thrustSpeed = 400;
    private float turnSpeed = 100;
    public float hoverPower;
    public float hoverHeight;

    private float thrustInput;
    private float turnInput;
    private Rigidbody shipRigidBody;
    public ParticleSystem speedLines;

    // Use this for initialization
    void Start()
    {
        shipRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        thrustInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
        // When spacebar is pressed, we add particles
        if (Input.GetKey(KeyCode.Space))
        {
            speedLines.Play();
        }
        else
        {
            speedLines.Stop();
        }
    }

    void FixedUpdate()
    {
        // Turning the ship
        shipRigidBody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);

        // Moving the ship
        shipRigidBody.AddRelativeForce(0f, 0f, thrustInput * thrustSpeed);

        // Hovering
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverPower;
            shipRigidBody.AddForce(appliedHoverForce, ForceMode.Acceleration);
        }

        
    }
}