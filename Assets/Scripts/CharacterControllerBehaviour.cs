using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

//[RequireComponent(typeof(CharacterController))]
public class CharacterControllerBehaviour : MonoBehaviour {

    private CharacterController _characterController;
    private Vector3 _velocity = Vector3.zero; //[m/s]


    void Start ()
    {
        _characterController = GetComponent<CharacterController>(); //meestal deze gebruiken

#if DEBUG
        Assert.IsNotNull(_characterController, "DEPENCANDY ERROR: CharacterControllerBehaviour needs a CharacterController Component");

        //if (_characterController == null)
        //    Debug.LogError("DEPENDANCY ERROR: CharacterControllerBehaviour needs a CharacterController Component");
#endif
    }
	
	void Update ()
    {
        if (!_characterController.isGrounded)
        {
            //velocity needs to change overtime
            _velocity += Physics.gravity * Time.deltaTime;
        }
        
        Vector3 movement = _velocity * Time.deltaTime;

        _characterController.Move(movement);
	}
}
