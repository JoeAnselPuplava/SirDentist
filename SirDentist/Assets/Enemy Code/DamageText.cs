using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public float floatSpeed = 1.0f;
    public float fadeSpeed = 1.0f;

    void Update()
    {
        // Move the text upwards
        transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);
        // Fade out the text alpha
        Color textColor = GetComponent<TMP_Text>().color;
        textColor.a -= fadeSpeed * Time.deltaTime;
        GetComponent<TMP_Text>().color = textColor;

        // Destroy the text when alpha reaches zero
        if (textColor.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}

