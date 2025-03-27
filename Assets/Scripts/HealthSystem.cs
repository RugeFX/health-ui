using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public Image background;
    public Image healthBar;
    public Gradient gradient;
    public Button healBtn;
    public Button attackBtn;
    public AnimationCurve damageCurve;
    public AnimationCurve healCurve;
    public float healthAmount = 100f;
    public float animationDuration = .3f;
    private Vector3 initialHealthScale;
    private Color initialBackgroundColor;

    // Start is called before the first frame update
    void Start()
    {
        initialHealthScale = healthBar.transform.localScale;
        initialBackgroundColor = background.color;

        healBtn.onClick.AddListener(OnHealBtnClick);
        attackBtn.onClick.AddListener(OnAttackBtnClick);
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        UpdatehealthBar();
    }

    private void UpdatehealthBar()
    {
        if (healthBar.fillAmount != NormalizedHealth)
        {
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, NormalizedHealth, .05f);
            healthBar.color = Color.Lerp(healthBar.color, gradient.Evaluate(NormalizedHealth), .05f);
        }
    }

    private void CheckHealth()
    {
        if (healthAmount <= 0)
            attackBtn.interactable = false;
        else if (healthAmount >= 100)
            healBtn.interactable = false;
        else
            attackBtn.interactable = healBtn.interactable = true;
    }

    private IEnumerator ShakeDamage()
    {
        Vector3 initialPosition = healthBar.transform.position;
        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float strength = damageCurve.Evaluate(elapsed / animationDuration) * 5f;
            healthBar.transform.position = initialPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        healthBar.transform.position = initialPosition;
    }

    private IEnumerator ScaleHeal()
    {
        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float scale = healCurve.Evaluate(elapsed / animationDuration) * .1f;
            healthBar.transform.localScale = initialHealthScale + Vector3.one * scale;
            yield return null;
        }

        healthBar.transform.localScale = initialHealthScale;
    }

    private IEnumerator OverlayHeal()
    {
        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float strength = healCurve.Evaluate(elapsed / animationDuration) * .2f;
            background.color = initialBackgroundColor + Color.green * strength;
            yield return null;
        }

        background.color = initialBackgroundColor;
    }

    private IEnumerator OverlayDamage()
    {
        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float strength = damageCurve.Evaluate(elapsed / animationDuration) * .2f;
            background.color = initialBackgroundColor + Color.red * strength;
            yield return null;
        }

        background.color = initialBackgroundColor;
    }

    private float NormalizedHealth
    {
        get
        {
            return healthAmount / 100f;
        }
    }


    public void TakeDamage(float amount)
    {
        if (healthAmount <= 0)
            return;

        StartCoroutine(ShakeDamage());
        StartCoroutine(OverlayDamage());
        healthAmount -= amount;
    }

    public void Heal(float amount)
    {
        if (healthAmount >= 100)
            return;

        StartCoroutine(ScaleHeal());
        StartCoroutine(OverlayHeal());
        healthAmount += amount;
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
