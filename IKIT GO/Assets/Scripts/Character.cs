﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Character : MonoBehaviour{

	float	Center_Screen_X;
	float	Center_Screen_Y;
	Vector2	StartVector2;
	Rigidbody2D	rbCharacter;
	bool	isCharacterUp ;
	bool	touchJump;
	public bool	isDead;
	float distanceOfJump;
	float heightOfJump;
	float step;

    GameObject[] background;
    GameObject[] objects;
    float speed;


	// Use this for initialization
	void Start () {
		Center_Screen_X = Screen.width;
		Center_Screen_Y = Screen.height;
		StartVector2 = transform.position;
		rbCharacter = GetComponent<Rigidbody2D>();
		distanceOfJump = heightOfJump = 1; // heightOfJump = distanceOfJump
		isCharacterUp = true;
		step = 0.09F;
		touchJump = false;
		isDead = false;

        background = GameObject.FindGameObjectsWithTag("Background");
        objects = GameObject.FindGameObjectsWithTag("Object");
        speed = -3;
	}
	
	//логика прыжка в методе FixedUpdate

	void FixedUpdate(){
		
		if (Input.touchCount > 0 && isDead == false) {
			foreach (Touch touch in Input.touches) {
				if (touch.position.y > Center_Screen_Y / 2 && isCharacterUp) { //jump
					rbCharacter.AddForce (Vector2.up * 3F, ForceMode2D.Impulse);
				}
			}
		}

	}
	void Update () {


		if( Input.touchCount > 0 && isDead == false)
		{
			foreach(Touch touch in Input.touches)
			{
				if( Center_Screen_Y/2 > touch.position.y	&&	Center_Screen_X/2 < touch.position.x /*&& isCharacterUp*/  && transform.position.x <= 8.32f) //move forvard
				{

					transform.position = Vector2.MoveTowards( (Vector2)transform.position, new Vector2( touch.position.x, 0), step );
                    Scroll();
				}
				else if( Center_Screen_Y/2 > touch.position.y &&  Center_Screen_X/2 > touch.position.x && isCharacterUp && transform.position.x >= -8.26f) //move back
				{

					transform.position = Vector2.MoveTowards( (Vector2)transform.position, new Vector2( -touch.position.x, 0), step );

				}
				/*else if(touch.position.y > Center_Screen_Y/2  && isCharacterUp) //jump
				{	
					rbCharacter.AddForce( Vector2.up * 8F, ForceMode2D.Impulse );
					//rbCharacter.AddForce( Vector2.up * 5F, ForceMode2D.Impulse);
				}	*/	

			}	


		}
	


	
	}



	//Проверка на местоположение для прыжка
	void OnCollisionExit2D()
	{
				isCharacterUp = false;	
	}



	void OnCollisionEnter2D( Collision2D c )
	{		

		isCharacterUp = true;	
        if(c.gameObject.name == "ice" || c.gameObject.name == "nut(Clone)")
		{
			isDead = true;
			GameControll.instance.CharacterDied();
		}
	}
	

    void Scroll()
    {

        foreach (var pic in background)
        {
            pic.transform.Translate(speed * Time.deltaTime, 0f, 0f);

            if (pic.transform.position.x <= -17.7)
            {
                pic.transform.Translate(35.4f, 0f, 0f);
            }
        }

        foreach (var obj in objects)
        {

            obj.transform.Translate(speed * Time.deltaTime, 0f, 0f);

            if (obj.transform.position.x <= -10f)
            {
                obj.transform.Translate(20f, 0f, 0f);
            }
        }
    }
}
