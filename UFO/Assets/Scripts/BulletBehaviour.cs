using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public const float MAX_DISTANCE = 200;
    public float SPEED;

    Vector3 direction;
    GameObject player;

    //to avoid self-shooting, the bullet will not explode by the one that shot it
    public GameObject shooter;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GetDirection();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(direction * Time.deltaTime * SPEED);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != shooter) 
        {
            other.GetComponent<DamageMechanicBehaviour>().GetDamage(1.0f);
            Explode();
        }
    }

    private void GetDirection()
    {
        if (shooter.CompareTag("Player"))
        {
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePoint - this.transform.position).normalized;
        }
        else
        {
            direction = (player.transform.position - this.transform.position).normalized;
        }
    }

    void Explode()
    {
        Destroy(this.gameObject);
    }
}
