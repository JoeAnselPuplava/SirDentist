using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowFade : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject shadow;
    private Renderer objectRenderer;
    private Color originalColor;

    public float fadeDuration = 1.5f;
    bool fading = false;

    void Start()
    {
        objectRenderer = shadow.GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
    }


    public void shadowvanish(){
        Debug.Log("Shadow hit");
        StartCoroutine(FadeAndDestroy());
    }

    IEnumerator FadeAndDestroy()
    {
        float currentFadeTime = 0f;

        while (currentFadeTime < fadeDuration)
        {
            float alpha = 1 - (currentFadeTime / fadeDuration);
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            objectRenderer.material.color = newColor;

            yield return null;
            currentFadeTime += Time.deltaTime;
        }

        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
