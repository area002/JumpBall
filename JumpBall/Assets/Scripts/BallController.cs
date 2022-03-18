using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody rb;
    public float impulseForce = 3;
    private bool ingnoreNextCollision;
    private Vector3 starPosition;
    public int perfectPass;
    public float superSpeed = 8;
    private bool IsSuperSpeedActive;
    private int perfectPassCount = 3;

    private void Start()
    {
        starPosition = transform.position;
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (ingnoreNextCollision)
        {
            return;
        }
        //Destruir plataforma con supervelocidad 
        if (IsSuperSpeedActive && !collision.transform.GetComponent<GoalController>())
        {
            Destroy(collision.transform.parent.gameObject, 0.2f);
        }
        else
        {
            DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
            if (deathPart)
            {
                GameManager.singleton.RestartLevel();
            }
        }


        //GameManager.singleton.AddScore(1);
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);

        ingnoreNextCollision = true;
        Invoke("AllownextCollision", 0.2f);

        perfectPass = 0;
        IsSuperSpeedActive = false;
    }

    private void AllownextCollision()
    {
        ingnoreNextCollision = false;
    }

    public void ResetBall()
    {
        transform.position = starPosition;
    }

    private void Update()
    {
        if(perfectPass>=perfectPassCount && !IsSuperSpeedActive)
        {
            IsSuperSpeedActive = true;
            rb.AddForce(Vector3.down * superSpeed, ForceMode.Impulse);
        }
    }
}
