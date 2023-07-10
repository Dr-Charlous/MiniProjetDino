using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    [SerializeField] float run = 8f;
    [SerializeField] float jump = 500f;
    [SerializeField] float climb = 5f;
    [SerializeField] float glide = 800f;

    [SerializeField] bool isJumping;
    [SerializeField] bool isClimbing;
    [SerializeField] bool isGliding;

    [SerializeField] Collider2D climbCheckCollider;
    [SerializeField] Collider2D groundCheckCollider;
    [SerializeField] LayerMask groundCheck;

    [SerializeField] Animator character;

    void Update()
    {
        //Déplacements
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x != 0 && !isClimbing)
        {
            if (!isJumping)
            {
                character.SetBool("Running", true);
                character.SetBool("Jumping", false);
            }
            

            if (x < 0)
            {
                character.transform.localScale = new Vector3(-1, 1, 1);
                character.transform.localPosition = new Vector3(0.25f, -0.6f, 1);

            }
            if (x > 0)
            {
                character.transform.localScale = new Vector3(1, 1, 1);
                character.transform.localPosition = new Vector3(-0.25f, -0.6f, 1);
            }


            if (Input.GetKey(KeyCode.LeftShift) && !isGliding)
            {
                transform.position += new Vector3(x, 0, 0) * run * Time.deltaTime;
            }
            else
            {
                transform.position += new Vector3(x, 0, 0) * speed * Time.deltaTime;
            }
        }
        else
            character.SetBool("Running", false);


        //Saute
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (groundCheckCollider.IsTouchingLayers(groundCheck))
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jump));
                character.SetBool("Running", false);
                character.SetBool("Jumping", true);
                character.SetTrigger("JumpingT");
                isJumping = true;
            }
            else if (climbCheckCollider.IsTouchingLayers(groundCheck))
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(x * jump/2, jump));
                character.SetBool("Running", false);
                character.SetBool("Jumping", true);
                character.SetTrigger("JumpingT");
                isJumping = true;
            }
        }

        //Chute
        if (gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            isJumping = false;
            character.SetBool("Jumping", false);
        }
        if (groundCheckCollider.IsTouchingLayers(groundCheck) && isJumping == false)
        {
            character.SetBool("Gliding", false);
        }
        





        //Escalade
        if (climbCheckCollider.IsTouchingLayers(groundCheck))
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.position += new Vector3(0, y, 0) * climb * Time.deltaTime;
            isClimbing = true;
            character.SetBool("Climbing", true);
        }
        else 
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 2;
            isClimbing = false;
            character.SetBool("Climbing", false);
        }



        //Planne
        if (Input.GetKey(KeyCode.Space) && !groundCheckCollider.IsTouchingLayers(groundCheck) && !isJumping)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, glide * Time.deltaTime));
            isGliding = true;
            character.SetBool("Gliding", true);
        }
        else
        {
            isGliding = false;
        }
    }
}
