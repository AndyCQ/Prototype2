using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldowns : MonoBehaviour
{

    [Header("Ability 1")]
    public Image abilityImage1;
    public float cooldown1;
    public bool isCooldown1 = false;
    public KeyCode ability1;
    public Image realImage1;
    bool expand1;

    [Header("Ability 2")]
    public Image abilityImage2;
    public float cooldown2;
    public bool isCooldown2 = false;
    public KeyCode ability2;
    public Image realImage2;
    bool expand2;

    [Header("Ability 3")]
    public Image abilityImage3;
    public float cooldown3;
    public bool isCooldown3 = false;
    public KeyCode ability3;
    public Image realImage3;
    bool expand3;


    // Start is called before the first frame update
    void Start()
    {
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
        abilityImage3.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Ability1();
        Ability2();
        Ability3();
        if(expand1)
            realImage1.transform.localScale += new Vector3(0.005f, 0.005f, 0.005f);
        else if(realImage1.transform.localScale.x > 0.34) 
            realImage1.transform.localScale -= new Vector3(0.005f, 0.005f, 0.005f);

        if(expand2)
            realImage2.transform.localScale += new Vector3(0.005f, 0.005f, 0.005f);
        else if(realImage2.transform.localScale.x > 0.34) 
            realImage2.transform.localScale -= new Vector3(0.005f, 0.005f, 0.005f);

        if(expand3)
            realImage3.transform.localScale += new Vector3(0.005f, 0.005f, 0.005f);
        else if(realImage3.transform.localScale.x > 0.34) 
            realImage3.transform.localScale -= new Vector3(0.005f, 0.005f, 0.005f);
    }

    void Ability1()
    {
        if(isCooldown1)
        {
            abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;

            if(abilityImage1.fillAmount <= 0)
            {
                abilityImage1.fillAmount = 0;
                isCooldown1 = false;
                StartCoroutine(Bump(realImage1));
            }
        }
        if(Input.GetKey(ability1) && isCooldown1 == false)
        {
            isCooldown1 = true;
            abilityImage1.fillAmount = 1;
        }
    }

    void Ability2()
    {
        if(isCooldown2)
        {
            abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;

            if(abilityImage2.fillAmount <= 0)
            {
                abilityImage2.fillAmount = 0;
                isCooldown2 = false;
                StartCoroutine(Bump(realImage2));
            }
        }
        if(Input.GetKey(ability2) && isCooldown2 == false)
        {
            isCooldown2 = true;
            abilityImage2.fillAmount = 1;
        }
    }

    void Ability3()
    {
        if(isCooldown3)
        {
            abilityImage3.fillAmount -= 1 / cooldown3 * Time.deltaTime;

            if(abilityImage3.fillAmount <= 0)
            {
                abilityImage3.fillAmount = 0;
                isCooldown3 = false;
                StartCoroutine(Bump(realImage3));
            }
        }
        if(Input.GetKey(ability3) && isCooldown3 == false)
        {
            isCooldown3 = true;
            abilityImage3.fillAmount = 1;
        }
    }


    private IEnumerator Bump(Image im)
    {
        if(im == realImage1)
            expand1 = true;
        if(im == realImage2)
            expand2 = true;
        if(im == realImage3)
            expand3 = true;

        yield return new WaitForSeconds(0.05f);
        expand1 = false;
        expand2 = false;
        expand3 = false;
    }
}
