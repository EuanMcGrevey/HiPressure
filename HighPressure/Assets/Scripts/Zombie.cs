using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {

	public GameObject Target;
	private GameObject Enemy;
	private float Range;
	public float Speed;
	private int interval = 1;
	private float nextTime = 0;
	private bool alive = true;


	 // Use this for initialization
	void Start () {
		Enemy = gameObject;
	}


	 // Update is called once per frame
	void Update () {
		if(!Target)
			return;

		Range = Vector3.Distance(Enemy.transform.position, Target.transform.position);
		if (Range <= 15f) {
			transform.position = (Vector2.MoveTowards(new Vector2(Enemy.transform.position.x, Enemy.transform.position.y), new Vector2(Target.transform.position.x, Target.transform.position.y), Speed));
		}

		if(Range <= 1f && Time.time >= nextTime) {
			Target.GetComponent<PlayerHeath>().TakeDamage(10);
			nextTime = Time.time + interval;
		}
	}

	public bool GetAlive() {
		return alive;
	}

	public void SetAlive(bool newalive)
	{
		alive = newalive;
	}
}
