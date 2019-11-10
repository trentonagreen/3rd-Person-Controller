using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Transform cam;
	private Animator anim;

	private Vector3 direction;
	private Vector3 rotationDirection;
    private float inputMagnitude;

    public float moveSpeed;
	public float rotationSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        #region Walking
        Vector3 forward = cam.forward;
        Vector3 right = cam.right;
        forward.y = 0;
        right.y = 0;

        direction = horizontal * right + vertical * forward;
        direction = direction * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(transform.position + direction);
        #endregion

        #region Rotation
        rotationDirection = horizontal * right + forward * vertical;

        if(!Mathf.Approximately(horizontal, 0.0f) || !Mathf.Approximately(vertical, 0.0f))
        {
            float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(rotationDirection));

            if(!Mathf.Approximately(angle, 0.0f))
            {
                Quaternion deltaRotation = Quaternion.RotateTowards(rb.rotation, Quaternion.LookRotation(rotationDirection), rotationSpeed * Time.fixedDeltaTime);
                rb.MoveRotation(deltaRotation);
            }
        }
        #endregion

        #region Animation
        inputMagnitude = new Vector2(horizontal, vertical).sqrMagnitude;
        anim.SetFloat("InputMagnitude", inputMagnitude, .25f, Time.fixedDeltaTime);
        #endregion
    }
}
