using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FamilyMember : MonoBehaviour {

	public GameObject Target;
	private GameObject Person;
	private float Range;
	public float Speed;
	private int interval = 1;
	private float nextTime = 0;
	private bool following = false;
    public static int saved = 0;


    // Use this for initialization
    void Start () {
		Person = gameObject;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {

			following = false;
			Target.GetComponent<PlayerController>().setBeingFollowed(false);
            gameObject.SetActive(false);
            Destroy(gameObject);
            UIManager.AddToScore(200 / 2); // score gets added twice due to icons
            saved++;
            var door = GameObject.Find(Person.name) as GameObject;
            if(door)
	            door.SetActive(false);

            GameObject f = GameObject.FindWithTag("Family");
            if(!f) {
            	Debug.Log("You win!");
	            SceneManager.LoadScene(8);
            }

        }
    }

	 // Update is called once per frame
	void Update () {
		if(!Target)
			return;

		Range = Vector2.Distance(Person.transform.position, Target.transform.position);
		bool canFollow = !Target.GetComponent<PlayerController>().getBeingFollowed() || following;
		if (canFollow && Range <= 2f && Range > 1f) {
			if(!following) {
				Target.GetComponent<PlayerController>().setBeingFollowed(true);
				following = true;
			}

			transform.position = (Vector2.MoveTowards(new Vector2(Person.transform.position.x, Person.transform.position.y), new Vector2(Target.transform.position.x, Target.transform.position.y), Speed));
		} else if (Range > 5f && following) {
			following = false;
			Target.GetComponent<PlayerController>().setBeingFollowed(false);
		}
	}
}