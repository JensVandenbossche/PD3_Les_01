using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

//require components, best ook een klasse met alle scripts die op een component moeten
//[RequireComponent(typeof(CharacterController))]

public class CharacterControllerBehaviour : MonoBehaviour {

    private CharacterController _characterController;

	void Start ()
    {
        _characterController = GetComponent<CharacterController>(); //meestal deze gebruiken

#if DEBUG
        Assert.IsNotNull(_characterController, "DEPENCANDY ERROR: CharacterControllerBehaviour needs a CharacterController Component");

        //if (_characterController == null)
        //{
        //    Debug.LogError("DEPENDANCY ERROR: CharacterControllerBehaviour needs a CharacterController Component");
        //}
#endif
    }
	
	void Update ()
    {
		
	}
}
