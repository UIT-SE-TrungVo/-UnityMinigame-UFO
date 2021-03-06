using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMechanicBehaviour : MonoBehaviour
{
    #region HEALTH & DAMAGE
    public float START_HEALTH;
    float healthPoint;

    public bool isDead()
    {
        if (healthPoint <= 0) return true;
        else return false;
    }
    
    public float GetHealthPoint()
    {
        return healthPoint;
    }

    public void GetDamage(float value)
    {
        healthPoint -= value;
        Debug.Log(this.gameObject.ToString() + "got damage !");
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        healthPoint = START_HEALTH;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead())
        {
            this.gameObject.SendMessage("Explode");
            Destroy(this.gameObject);
        }
    }
}
