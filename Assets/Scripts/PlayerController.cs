using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody rb;

    private InputAction moveAction;
    private InputAction smashAction;
    private InputAction breakAction;

    public Transform forcalPoint;

    public bool hasPowerUp;

    private Coroutine powerUpRoutine;

    public GameObject powerUpRing;
    private Animator powerUpAni;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
        smashAction = InputSystem.actions.FindAction("Smash");
        breakAction = InputSystem.actions.FindAction("Break");

        powerUpAni = GameObject.Find("SelectionRing_02").GetComponent<Animator>();



    }

    private void Start()
    {
        powerUpRing.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var move = moveAction.ReadValue<Vector2>();
        rb.AddForce(move.y * speed * forcalPoint.forward);

        if (breakAction.IsPressed())
        {
            rb.linearVelocity = Vector3.zero;
        }

        if (hasPowerUp) 
        {
        
        }

    }

    private void LateUpdate()
    {
        powerUpRing.transform.position = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (hasPowerUp)
            {
                var enemyRb = collision.gameObject.GetComponent<Rigidbody>();
                //var v = enemyRb.linearVelocity;
                //v.Normalize();

                var dir = enemyRb.transform.position - transform.position;
                dir.Normalize();
                enemyRb.AddForce(dir * 5, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);

            if (powerUpRoutine != null)
            {
                StopCoroutine(powerUpRoutine);
            }

         

            powerUpRoutine = StartCoroutine(PowerUpCooldown());
            
        }
    }

    IEnumerator PowerUpCooldown()
    {
        powerUpRing.SetActive(true);
        yield return null;
        powerUpAni.SetBool("hasPowerUp", true);


        yield return new WaitForSeconds(10f);
        hasPowerUp = false;
        powerUpAni.SetBool("hasPowerUp", false);

        powerUpRing.SetActive(false);

    }

}