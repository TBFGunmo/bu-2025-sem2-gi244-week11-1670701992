using UnityEngine;

public class StunPowerUp : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    public void ActiveStun() 
    {
        playerController.activeStun = true;   // sent activeStun to playercontroller
        Destroy(this.gameObject);
    }

    
}
