using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageDisplayText : MonoBehaviour
{
    public float speed;
    public TextMeshProUGUI damageTMP;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveUp = new Vector3(0, speed, 0);
        this.transform.Translate(moveUp);
    }

    public void SetDamageText(float damage)
    {
        damageTMP.text = "-" + (int)damage;
    }
}
