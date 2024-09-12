using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField, Header("左リミット")]
    public float leftLimit;
    [SerializeField, Header("上リミット")]
    public float TopLimit;
    [SerializeField, Header("右リミット")]
    public float rightLimit;
    [SerializeField, Header("下リミット")]
    public float bottomLimit;
    GameObject player;
    public GameObject subScreen;
    float playerPosX;
    float playerPosY;
    float playerPosZ;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            playerPosX = player.transform.position.x;
            playerPosY = player.transform.position.y;
            playerPosZ = transform.position.z;

            if (playerPosX < leftLimit) playerPosX = leftLimit;
            else if (playerPosX > rightLimit) playerPosX = rightLimit;
            if (playerPosY < bottomLimit) playerPosY = bottomLimit;
            else if (playerPosY > TopLimit) playerPosY = TopLimit;

            Vector3 v3 = new Vector3(playerPosX, playerPosY, playerPosZ);
            transform.position = v3;
            if (subScreen != null)
            {
                playerPosY = subScreen.transform.position.y;
                playerPosZ = subScreen.transform.position.z;
                Vector3 subV = new Vector3(playerPosX / 2.0f, playerPosY, playerPosZ);
                subScreen.transform.position = subV;
            }
        }
    }
}
