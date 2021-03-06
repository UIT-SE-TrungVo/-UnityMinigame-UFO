using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    #region MOVEMENT
    public const float ACCELERATION = 1; //the object would get full speed after 1 second
    public const float SPEED = 30.0f; //the speed of object is 20*deltaTime;

    public float x_acceleration = 0;
    public float y_acceleration = 0;

    private void Move()
    {
        //MOVE THE PLAYER
        Vector3 movement = new Vector3(x_acceleration * SPEED * Time.deltaTime, y_acceleration * SPEED * Time.deltaTime);
        this.transform.Translate(movement);

        //swing the UFO to be more realistic
        //this.transform.Rotate(new Vector3(0, 0, Mathf.Min(1.0f, x_acceleration + y_acceleration) * 360 * Time.deltaTime));

        //MOVE CAMERA
        float distance = Vector3.Distance(this.transform.position, Camera.main.transform.position);
        if (distance >= 2)
        {
            Vector3 direction = this.transform.position - Camera.main.transform.position;
            direction.z = 0;
            Camera.main.transform.Translate(direction * SPEED * 0.1f * Time.deltaTime);
        }
        //this.transform.localPosition = new Vector3(x_acceleration * 4, y_acceleration * 4, 5.0f);
    }
    #endregion

    #region GET INPUT
    void GetInput()
    {
        ///ACCELERATIONS MUST BE IN [-1,1]

        #region check vertical direction
        //check vertical        
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)) { } //if both key are pressed, do nothing
        else if (Input.GetKey(KeyCode.S)) y_acceleration = Mathf.Max(-1, y_acceleration - Time.deltaTime);
        else if (Input.GetKey(KeyCode.W)) y_acceleration = Mathf.Min(1, y_acceleration + Time.deltaTime);
        else if (y_acceleration != 0)  //decrease acceleration by Time.deltaTime
        {
            float value = Mathf.Abs(y_acceleration);
            float i = (y_acceleration >= 0 ? 1 : -1);
            y_acceleration = i * Mathf.Max(0, value - Time.deltaTime * 2);
        }
        #endregion

        #region check horizontal direction
        //check horizontal
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) { } //if both key are pressed, do nothing
        else if (Input.GetKey(KeyCode.A)) x_acceleration = Mathf.Max(-1, x_acceleration - Time.deltaTime);
        else if (Input.GetKey(KeyCode.D)) x_acceleration = Mathf.Min(1, x_acceleration + Time.deltaTime);
        else if (x_acceleration != 0) //decrease acceleration by Time.deltaTime
        {
            float value = Mathf.Abs(x_acceleration);
            float i = (x_acceleration >= 0 ? 1 : -1);
            x_acceleration = i * Mathf.Max(0, value - Time.deltaTime);
        }
        #endregion
    }
    #endregion

    #region FIRING MECHANIC
    public GameObject bullet;
    private void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bullet.GetComponent<BulletBehaviour>().shooter = this.gameObject;
            Instantiate(bullet, this.transform.position, Quaternion.identity);
        }
    }
    #endregion

    #region COLLISION
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //this.GetComponent<DamageMechanicBehaviour>().GetDamage(1.0f);
        }
    }
    #endregion

    public void Explode()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    // Start is called before the first frame update
    void Start()
    {
        x_acceleration = 0;
        y_acceleration = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        GetInput();
        Move();
    }
}
