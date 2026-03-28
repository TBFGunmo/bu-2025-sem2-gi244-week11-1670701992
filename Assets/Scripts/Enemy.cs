using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody rb;
    private GameObject player;
    private PlayerController playerController;

    private bool activeStun = false; // สำหรับให้หยุดทันทีหลังสตั้น เเต่ผู้เล่นยังคงผลักตัวศัตรุได้อยู่

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>(); 

    }

    // Update is called once per frame
    void Update()
    {
        if (!(playerController.activeStun))
        {
            Vector3 dir = (player.transform.position - transform.position);
            dir.Normalize();
            rb.AddForce(dir * speed);

            if (activeStun) 
            {
                activeStun = false;
            }

        }
        else if (!activeStun) 
        {
            rb.linearVelocity = Vector3.zero;
            activeStun = true;
        }
        

    }
}