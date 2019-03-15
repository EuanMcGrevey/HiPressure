using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSpaceship : MonoBehaviour {
	public GameObject Target;
	public float Speed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		float Range = Vector3.Distance(transform.position, Target.transform.position);
		if(Range > 0f){
			transform.position = (Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(Target.transform.position.x, Target.transform.position.y), Speed));
			var angle = Mathf.PingPong(Time.time * 75.0f, 270.0f);
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}else {
			SceneManager.LoadScene(4);
		}
		
	}
}
