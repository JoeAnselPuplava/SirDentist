using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shift_collider : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D myCollider;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<PolygonCollider2D>();
    }

void LateUpdate()
{
    // Get the current animation state
    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

    // Check if the animation is playing (you can adjust the condition as needed)
    if (stateInfo.normalizedTime < 1.0f)
    {
        // Get the sprite for the current frame
        Sprite currentSprite = spriteRenderer.sprite;

        // Get the sprite's texture and extract pixel data
        Texture2D tex = currentSprite.texture;
        Color[] pixels = tex.GetPixels((int)currentSprite.textureRect.x,
                                       (int)currentSprite.textureRect.y,
                                       (int)currentSprite.textureRect.width,
                                       (int)currentSprite.textureRect.height);

        // Process pixel data to generate collider points
        Vector2[] colliderPath = GenerateColliderPoints(pixels, tex.width, tex.height);

        // Set the collider path
        myCollider.SetPath(0, colliderPath);
    }
}

Vector2[] GenerateColliderPoints(Color[] pixels, int width, int height)
{
    List<Vector2> colliderPoints = new List<Vector2>();

    if (pixels.Length == 0)
    {
        Debug.LogError("Pixels array is empty.");
        return new Vector2[0];
    }
    else
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = y * width + x;

                if (index < pixels.Length)
                {
                    if (pixels[index].a > 0.5f)
                    {
                        colliderPoints.Add(new Vector2(x, y));
                    }
                }
                else
                {
                    Debug.LogError("Index out of bounds: " + index);
                }
            }
        }
    }

    return colliderPoints.ToArray();
}

}
