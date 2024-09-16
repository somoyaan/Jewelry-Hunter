using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBlock : MonoBehaviour
{
    [SerializeField, Header("自動落下検知距離")]
    public float length;
    public bool isDelete;
    bool isFall;
    [SerializeField, Header("フェードアウト時間")]
    private float fadeTime;
    Rigidbody2D rbody;
    GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        isDelete = false;
        isFall = false;
        rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // float d = Vector2.Distance(transform.position, player.transform.position);
            // if (length >= d)
            if (length > (transform.position.x - player.transform.position.x))
            {
                rbody.bodyType = RigidbodyType2D.Dynamic;
            }
        }
        if (isFall)
        {
            //落下した
            fadeTime -= Time.deltaTime;
            Color color = GetComponent<SpriteRenderer>().color;
            color.a = fadeTime;
            GetComponent<SpriteRenderer>().color = color;
            if (fadeTime < 0.0f) Destroy(gameObject);
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        isFall = true;
    }
}
