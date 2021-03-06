using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    const float MAX_DISTANCE = 50;
    const float SPEED = 20f;
    const float SHOOT_RATE = 0.01f;

    Vector3 direction = new Vector3(0, 0, 0);
    GameObject player;
    public GameObject bullet;

    #region COLLISION
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<DamageMechanicBehaviour>().GetDamage(1.0f);
            Explode();
        }
    }
    #endregion

    private Vector3 GetTargetPointOnScreen()
    {
        //get a random point on screen, this make sure the UFO will not go out
        float x_ViewPort = Random.Range(0.3f, 0.7f);
        float y_ViewPort = Random.Range(0.2f, 0.8f);

        Vector3 newPosition_ViewPort = new Vector3(x_ViewPort, y_ViewPort, 5);
        Vector3 newPosition_WorldPoint = Camera.main.ViewportToWorldPoint(newPosition_ViewPort);

        return newPosition_WorldPoint;
    }


    void FireAtPlayer()
    {
        //to avoid self-shooting, the bullet will not explode by the one that shot it
        bullet.GetComponent<BulletBehaviour>().shooter = this.gameObject;
        Instantiate(bullet, this.transform.position, Quaternion.identity);
    }

    void Explode()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 randomPointOnScreen = GetTargetPointOnScreen();
        direction = (randomPointOnScreen - this.transform.position).normalized;
        direction.z = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float shootRandom = Random.value;
        if (shootRandom <= SHOOT_RATE)
        {
            FireAtPlayer();
        }

        //make random shooting
        this.transform.Translate(direction * SPEED * Time.deltaTime);
    }
}
