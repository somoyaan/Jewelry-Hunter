using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rbody;
    float axisH;
    [SerializeField, Header("移動スピード")]
    float moveSpeed;
    [SerializeField, Header("ジャンプ力")]
    float jumpPower;
    public LayerMask groundLayer;
    bool onGround;


    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        axisH = 0.0f;
        onGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void FixedUpdate()
    {
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);
    }

    //移動メソッド
    private void Move()
    {
        axisH = Input.GetAxisRaw("Horizontal");
        rbody.velocity = new Vector2(axisH * moveSpeed, rbody.velocity.y);
        if (axisH >= 1.0f) transform.localScale = new Vector2(1, 1);
        else if (axisH <= -1.0f) transform.localScale = new Vector2(-1, 1);
    }

    // ジャンプメソッド
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && onGround)
        {
            Vector2 setJumpPower = new Vector2(0, jumpPower);
            rbody.AddForce(setJumpPower, ForceMode2D.Impulse);
        }
    }
}
