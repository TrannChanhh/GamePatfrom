using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TapController : MonoBehaviour {

	public delegate void PlayerDelegate ();
	public static event PlayerDelegate OnPlayerDied;
	public static event PlayerDelegate OnPlayerScored;
	public GameObject fireFX;

	public float tapForce=10;
	public float tiltSmooth=5;
	public Vector3 startPos;

	public AudioSource tapAudio;
	public AudioSource scoreAudio;
	public AudioSource dieAudio;



	Rigidbody2D rigidbody;
	Quaternion downrotation;
	Quaternion forwardrotation;

	GameManager game;


	void Start(){
		rigidbody = GetComponent<Rigidbody2D> ();
		downrotation = Quaternion.Euler (0, 0, -90);
		forwardrotation = Quaternion.Euler (0, 0, 35);
		game = GameManager.Instance;
		rigidbody.simulated = false;
	}

	void OnEnable(){
		GameManager.OnGameStarted += OnGameStarted;
		GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
	}

	void OnDisable(){
		GameManager.OnGameStarted -= OnGameStarted;
		GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
	
	}

	void OnGameStarted(){
		rigidbody.velocity = Vector3.zero;
		rigidbody.simulated = true;

	}
	void OnGameOverConfirmed(){
		transform.localPosition = startPos;
		transform.rotation = Quaternion.identity;
	}
	void Update(){
		if (game.GameOver) {
			rigidbody.simulated = false;
			return;
		}
			
		if(Input.GetMouseButtonDown(0))
			{
			tapAudio.Play ();
			transform.rotation = forwardrotation;
			rigidbody.velocity = Vector3.zero;
			rigidbody.AddForce (Vector2.up * tapForce, ForceMode2D.Force);
			StartCoroutine(Fire() );

            }

		transform.rotation = Quaternion.Lerp (transform.rotation, downrotation, tiltSmooth * Time.deltaTime);
	
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        // If the object has the "Coin" tag
        if (col.CompareTag("Coin"))
        {
            OnPlayerScored?.Invoke();
            scoreAudio.Play();
			if (col != null)
			{
				if(col.CompareTag("Coin"))
                {
                    Destroy(col.gameObject);
                }
			}
			return;
        }

        if (col.CompareTag("DeadZone"))
        {
            rigidbody.simulated = false;
            OnPlayerDied?.Invoke();
            dieAudio.Play();
        }
    }
    IEnumerator Fire()
    {

        fireFX.SetActive(true);
        yield return new WaitForSeconds(1f);
        fireFX.SetActive(false);
    }


}
