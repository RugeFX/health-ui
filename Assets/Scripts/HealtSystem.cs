using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealtSystem : MonoBehaviour
{
    public Image HealthBar;
    public Gradient gradient;
    public Button HealBtn;
    public Button AttackBtn;
    public float HealthAmount = 100f;
    // Start is called before the first frame update
    void Start()
    {
        HealBtn.onClick.AddListener(OnHealBtnClick);
        AttackBtn.onClick.AddListener(OnAttackBtnClick);
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (HealthBar.fillAmount != NormalizedHealth)
        {
            HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, NormalizedHealth, .05f);
            HealthBar.color = Color.Lerp(HealthBar.color, gradient.Evaluate(NormalizedHealth), .05f);
        }
    }

    private void CheckHealth()
    {
        if (HealthAmount <= 0)
            AttackBtn.interactable = false;
        else if (HealthAmount >= 100)
            HealBtn.interactable = false;
        else
            AttackBtn.interactable = HealBtn.interactable = true;
    }

    private float NormalizedHealth
    {
        get
        {
            return HealthAmount / 100f;
        }
    }


    public void TakeDamage(float amount)
    {
        if (HealthAmount <= 0)
            return;

        HealthAmount -= amount;
    }

    public void Heal(float amount)
    {
        if (HealthAmount >= 100)
            return;

        HealthAmount += amount;
    }

    public void OnAttackBtnClick()
    {
        TakeDamage(10f);
    }
    public void OnHealBtnClick()
    {
        Heal(10f);
    }
}
