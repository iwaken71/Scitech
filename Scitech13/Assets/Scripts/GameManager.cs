using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject[] cubes;
	Renderer[] _renderer;
	int[] number;
	Material[] mats;
	bool game = false;
	int index = 0;
	public  Material[] sozai;
	int matnum;
	public GameObject panel;
	Animator anim;
	public Text scoreLabel;
	int score = 0; 
	int goalScore = 0;
	AudioSource source;
	public AudioClip hazure;

	// Use this for initialization
	void Start () {
		index = 0;
		_renderer = new Renderer[3];
		number = new int[3];
		matnum = sozai.Length;

		mats = new Material [matnum]; 
		source = GetComponent<AudioSource> ();


		//sozai = Resources.LoadAll ("Images") as Texture[];
		for (int i = 0; i < matnum; i++) {
			mats[i] = sozai[i];
		}

		for (int i = 0; i < 3; i++) {
			_renderer[i] = cubes[i].GetComponent<Renderer>();
			number[i] = i;
		}

		anim = panel.GetComponent<Animator> ();
		scoreLabel.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
		bool _space = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
		if (goalScore > score) {
			score++;
		}

		if(game){
			if(index == 0){
				number[0] ++;
				number[1] ++;
				number[2] ++;
				if(number[0] >= matnum){
					number[0] = 0;
				}if(number[1] >= matnum){
					number[1] = 0;
				}if(number[2] >= matnum){
					number[2] = 0;
				}
				if(_space){
					index = 1;
				}



			}else if(index == 1){
				number[1] ++;
				number[2] ++;
				if(number[1] >= matnum){
					number[1] = 0;
				}if(number[2] >= matnum){
					number[2] = 0;
				}
				if(_space){
					index = 2;
					if(Random.value < 0.5f){
						number[1] = 0;
					}
				}
			}else if(index == 2){
				number[2] += 1;
				while(number[2] >= matnum){
					number[2] -= matnum;
				}
				if(_space){
					index = 0;        
					game = false;
					if(Random.value < 0.5f){
						number[2] = 0;
					}
					if(Check() >= 0){
						//Debug.Log("ok");
						goalScore += 10;
						if(number[0] == 0){
							anim.SetTrigger("Clear");
						}
					}else{
						//Debug.Log("no");
					}
				}
			}

		}else{
			if(_space){
				game = true;
				number[0] = 0;
				number[1] = 0;
				number[2] = 0;
			}
		}
		scoreLabel.text = score.ToString ();
		Draw ();

	}

	void Draw(){
		for (int i = 0; i < 3; i++) {
			_renderer[i].material = mats[number[i]];
		}

	}
	int Check(){
		int ans = number[0];
		if (ans != number [1]) {
			source.clip = hazure;
			source.Play();
			return -1;
		}
		if (ans != number [2]) {
			source.clip = hazure;
			source.Play();
			return -1;
		}
		return ans;
	}
}
