using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

//[RequireComponent(typeof(CharacterController))]
public class CharacterControllerBehaviour : MonoBehaviour {

    [SerializeField]
    private Transform _absoluteTransform;

    [SerializeField]
    private float _acceleration = 3; //[m/s^2]

    private CharacterController _characterController;
    private Vector3 _velocity = Vector3.zero; //[m/s]

    private Vector3 _inputMovement;
    
    void Start ()
    {
        _characterController = GetComponent<CharacterController>();

#if DEBUG
        Assert.IsNotNull(_characterController, "DEPENCANDY ERROR: CharacterControllerBehaviour needs a CharacterController Component");
        //if (_characterController == null)
        //    Debug.LogError("DEPENDANCY ERROR: CharacterControllerBehaviour needs a CharacterController Component");
#endif
    }

    private void Update()
    {
        _inputMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    void FixedUpdate () //maakt gebruik van vaste timestamps en is dus smoother voor physics
    {
        ApplyGround();

        ApplyGravity();

        ApplyMovement();

        DoMovement();
    }

    private void ApplyMovement()
    {
        if(_characterController.isGrounded)
        {
            //Vector3 xzForward = _absoluteTransform.forward; //returns kopie
            //xzForward.y = 0;

            //Vector3 xzForward = new Vector3(_absoluteTransform.forward.x, 0, _absoluteTransform.forward.z);

            Vector3 xzForward = Vector3.Scale(_absoluteTransform.forward, new Vector3(1, 0, 1));

            Quaternion relativeRotation = Quaternion.LookRotation(xzForward);

            Vector3 relativeMovement = relativeRotation * _inputMovement;

            _velocity += relativeMovement * _acceleration * Time.fixedDeltaTime;
        }
    }

    private void DoMovement()
    {
        Vector3 displacement = _velocity * Time.deltaTime;

        _characterController.Move(displacement);
    }

    private void ApplyGravity()
    {
        if (!_characterController.isGrounded)
        {
            //velocity needs to change overtime
            _velocity += Physics.gravity * Time.fixedDeltaTime;
        }
    }

    private void ApplyGround()
    {
        if (_characterController.isGrounded)
        {
            _velocity -= Vector3.Project(_velocity, Physics.gravity); //component in velocity die veroorzaakt is door gravity
        }
    }
}
