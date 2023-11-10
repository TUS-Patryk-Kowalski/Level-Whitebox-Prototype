using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{

    public RawImage image1, image2;
    public Texture2D[] images;
    public float timeForImages;
    public float transitionSpeed;

    private int index;
    private IEnumerator imageSequence;

    private void Start()
    {
        imageSequence = CycleImages();
        StartCoroutine(imageSequence);

        if(timeForImages == 0)
        {
            timeForImages = 10;
        }
    }

    private IEnumerator CycleImages()
    {
        while (true)
        {
            // Set the top-level image to the current image
            image1.texture = images[index];

            // Increment the index with wrapping to avoid going out of bounds
            index = (index + 1) % images.Length;

            // Set the underlying image to the next image
            image2.texture = images[index];

            // Wait for the specified time
            yield return new WaitForSeconds(timeForImages);

            // Slowly transition the images by making the top image transparent over time
            while (image1.color.a > 0)
            {
                // Decrease the alpha value
                image1.color = new Color(image1.color.r, image1.color.g, image1.color.b, image1.color.a - transitionSpeed * Time.deltaTime);
                yield return null; // Wait for the next frame
            }

            // Once the top image is completely transparent, reset the alpha and prepare for the next image
            image1.color = new Color(image1.color.r, image1.color.g, image1.color.b, 1);
        }
    }
}
