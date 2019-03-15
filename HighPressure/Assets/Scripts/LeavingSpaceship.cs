using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeavingSpaceship : MonoBehaviour {
	public GameObject Target;
	public float Speed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
			gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 10);
			transform.rotation = Quaternion.AngleAxis(0f, Vector3.forward);
	}

	void OnBecameInvisible () {
		SceneManager.LoadScene(6);
	}
}
