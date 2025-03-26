using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealtSystem : MonoBehaviour
{
    public Image HealthBar;
    public float HealthAmount = 100f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(HealthAmount <= 0)
        {
            Debug.Log("Player is Dead");
        }
    }

    public void takeDamage(float damage)
    {
        HealthAmount -= damage;
        HealthBar.fillAmount = HealthAmount / 100f;
    }

    public void healDamage(float heal)
    {
        HealthAmount += heal;
        HealthBar.fillAmount = HealthAmount / 100f;
    }

    public void btnTakeDamage()
    {
        takeDamage(10f);
    }
    public void btnHealDamage()
    {
        healDamage(10f);
    }
}
