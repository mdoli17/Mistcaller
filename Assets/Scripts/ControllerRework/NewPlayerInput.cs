using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NewPlayer))]
public class NewPlayerInput : MonoBehaviour {

	NewPlayer player;

	void Start () {
		player = GetComponent<NewPlayer> ();
	}

	void Update () {
		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		player.SetDirectionalInput (directionalInput);

		if (Input.GetKeyDown (KeyCode.Space)) {
			player.OnJumpInputDown ();
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			player.OnJumpInputUp ();
		}
	}
}
