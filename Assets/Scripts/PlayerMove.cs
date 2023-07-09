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

    [SerializeField] Collider2D climbCheckCollider;
    [SerializeField] Collider2D groundCheckCollider;
    [SerializeField] LayerMask groundCheck;

    [SerializeField] Animator character;

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x != 0)
        {
            if (!isJumping)
            {
                character.SetBool("Running", true);
                character.SetBool("Jumping", false);
                character.SetBool("Idling", false);
            }
            

            if (x < 0)
            {
                character.transform.localScale = new Vector3(-0.15f, 0.15f, 0.15f);
                character.transform.localPosition = new Vector3(0.23f, -0.575f, 0.15f);

            }
            if (x > 0)
            {
                character.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                character.transform.localPosition = new Vector3(-0.23f, -0.575f, 0.15f);
            }
        }
        else
            character.SetBool("Running", false);



        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (groundCheckCollider.IsTouchingLayers(groundCheck))
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jump));
                character.SetBool("Running", false);
                character.SetBool("Jumping", true);
                character.SetBool("Idling", false);
                isJumping = true;
            }
            else if (climbCheckCollider.IsTouchingLayers(groundCheck))
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(x * jump, jump));
                isJumping = true;
            }
        }

        if (groundCheckCollider.IsTouchingLayers(groundCheck) && isJumping == false)
        {
            print("touch floor");
            character.SetBool("Jumping", false);
        }



        if (gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
            isJumping = false;



        if (climbCheckCollider.IsTouchingLayers(groundCheck))
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            transform.position += new Vector3(0, y, 0) * climb * Time.deltaTime;
        }
        else 
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 2;
        }




        if (Input.GetKey(KeyCode.Space) && !groundCheckCollider.IsTouchingLayers(groundCheck) && !isJumping)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, glide * Time.deltaTime));
        }




        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += new Vector3(x, 0, 0) * run * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(x, 0, 0) * speed * Time.deltaTime;
        }
    }
}
