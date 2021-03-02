using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOBehaviour : MonoBehaviour
{

    #region MOVEMENT
    public const float ACCELERATION = 1; //the object would get full speed after 1 second
    public const float SPEED = 10.0f; //the speed of object is 20*deltaTime;

    public float x_acceleration = 0;
    public float y_acceleration = 0;

    private void Move()
    {
        this.transform.Translate(new Vector2(x_acceleration * SPEED * Time.deltaTime, y_acceleration * SPEED * Time.deltaTime));
    }
    #endregion

    #region HEALTH 
    public const float MAX_HP = 1;
    private float healthPoint = MAX_HP;
    public bool isDead()
    {
        if (healthPoint <= 0) return true;
        else return false;
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
        else if (y_acceleration != 0)  //decrease acceleration by half
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
        else if (x_acceleration != 0) //decrease acceleration by half
        {
            float value = Mathf.Abs(x_acceleration);
            float i = (x_acceleration >= 0 ? 1 : -1);
            x_acceleration = i*Mathf.Max(0, value - Time.deltaTime);
        }
        #endregion
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        x_acceleration = 0;
        y_acceleration = 0;
        healthPoint = MAX_HP;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
    }
}
