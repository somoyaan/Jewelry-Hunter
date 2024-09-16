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
    public static string gameState;

    // アニメーション
    Animator animator;
    public string oldAnime;
    public string nowAnime;
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime = "PlayerJump";
    public string clearAnime = "PlayerClear";
    public string deadAnime = "PlayerDead";


    // Start is called before the first frame update
    void Start()
    {
        gameState = "playing";
        rbody = GetComponent<Rigidbody2D>();
        axisH = 0.0f;
        onGround = false;
        animator = GetComponent<Animator>();
        oldAnime = stopAnime;
        nowAnime = stopAnime;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState != "playing") return;
        Move();
        Jump();
    }

    void FixedUpdate()
    {
        if (gameState != "playing") return;
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);
        if (onGround)
        {
            if (axisH == 0) nowAnime = stopAnime;
            else nowAnime = moveAnime;
        }
        else nowAnime = jumpAnime;
        if (oldAnime != nowAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);
        }
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
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Goal")
        {
            animator.Play(clearAnime);
            gameState = "gameClear";
            rbody.velocity = new Vector2(0, 0);
        }
        if (col.gameObject.tag == "Dead")
        {
            GameOver();
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Dead")
        {
            GameOver();
        }
    }

    // ゲームオーバーメソッド
    private void GameOver()
    {
        animator.Play(deadAnime);
        rbody.velocity = new Vector2(0, 0);
        rbody.AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
        GetComponent<CapsuleCollider2D>().enabled = false;
        gameState = "gameOver";
    }
}

