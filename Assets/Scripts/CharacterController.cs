using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private GameObject HeroSprite;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float runMod { get { return Input.GetKey(KeyCode.LeftShift) ? _runMod : 0f; } }
    private float _runMod = 0.2f;
    private Vector2 CenterOfScreen { get { return new Vector2(Screen.width/2, Screen.height/2); } }
    private Vector2 CursorRelativeOfHero { get { return new Vector2 (Input.mousePosition.x, Input.mousePosition.y)-CenterOfScreen; } }

    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HeroRotate();
        Step();
        UpdateControllerValue();
    }

    //Метод ходьбы куда-то на случай если управлять персом будут эффекты. 
    public void Step()
    {
        rigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed * (1+runMod);
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
    public void UpdateControllerValue()
    {
        GetComponent<Animator>().SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
        GetComponent<Animator>().SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        GetComponent<Animator>().SetBool("Run",Input.GetKey(KeyCode.LeftShift));
        //Это временное решение, тк у нас нет нормальных анимаций
        GetComponent<Animator>().SetBool("Move", (Input.GetAxisRaw("Vertical")!=0 || Input.GetAxisRaw("Horizontal")!=0) ? true : false); ;

    }
}
