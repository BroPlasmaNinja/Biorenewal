using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private GameObject HeroSprite;
    [SerializeField] private float speed = 1.0f;
    private Vector2 CenterOfScreen { get { return new Vector2(Screen.width/2, Screen.height/2); } }
    private Vector2 CursorRelativeOfHero { get { return new Vector2 (Input.mousePosition.x, Input.mousePosition.y)-CenterOfScreen; } }

    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        Debug.Log(Screen.width + " " + Screen.height);
    }

    private void FixedUpdate()
    {
        HeroRotate();
        Step();
    }

    //Метод ходьбы куда-то на случай если управлять персом будут эффекты. 
    public void Step()
    {
        rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized*speed;
    }
    public void Step(Vector2 where)
    {
        rigidbody.velocity = where.normalized*speed;
    }
    //Математика
    public void HeroRotate()
    {
        HeroSprite.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(CursorRelativeOfHero.y, CursorRelativeOfHero.x) *180/Mathf.PI+90);
    }
}
