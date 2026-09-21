using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overlay_Controller : MonoBehaviour
{
    [SerializeField]
    private GameObject blackScreen;
    [SerializeField]
    private GameObject crackOverlay;
    [SerializeField]
    private List<GameObject> crackSubImages;
    
    private int activeCoroutines = 0;

    private void Awake()
    {
        if (GameManager.instance.fx.Overlay != null)
        {
            Destroy(gameObject);
            return;
        }
        GameManager.instance.fx.Overlay = this;
        DontDestroyOnLoad(this);
    }

    public IEnumerator PlayScreenCrack()
    {
        yield return new WaitForSeconds(0.25f);
        crackOverlay.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < crackSubImages.Count; i++)
        {
            Image image = crackSubImages[i].GetComponent<Image>();
            Color color = image.color;
            color.a = 0;
            image.color = color;
            crackSubImages[i].SetActive(true);
            StartCoroutine(FadeInImage(image));
            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitUntil(() => activeCoroutines == 0);

        Image blackImage = blackScreen.GetComponent<Image>();

        Color col = blackImage.color;
        col.a = 1;
        blackImage.color = col;

        blackScreen.SetActive(true);
        crackOverlay.SetActive(false);
        for (int i = 0; i < crackSubImages.Count; i++)
        {
            crackSubImages[i].SetActive(false);
        }
    }

    public IEnumerator FadeInScreen()
    {
        activeCoroutines++;
        Image image = blackScreen.GetComponent<Image>();

        yield return StartCoroutine(FadeOutImage(image));
        
        blackScreen.SetActive(false);
        activeCoroutines--;
    }

    public IEnumerator FadeOutScreen()
    {
        activeCoroutines++;
        blackScreen.SetActive(true);
        Image image = blackScreen.GetComponent<Image>();

        Color col = image.color;
        col.a = 0;
        image.color = col;

        yield return StartCoroutine(FadeInImage(image));

        activeCoroutines--;
    }

    private IEnumerator FadeInImage(Image image, float time = 0.5f)
    {
        activeCoroutines++;
        yield return new WaitUntil(() =>
        {
            Color color = image.color;
            float inc = Time.deltaTime / time;
            color.a += inc;
            image.color = color;

            return color.a >= 1;
        });
        activeCoroutines--;
    }

    private IEnumerator FadeOutImage(Image image, float time = 0.5f)
    {
        activeCoroutines++;
        yield return new WaitUntil(() =>
        {
            Color color = image.color;
            float inc = Time.deltaTime / time;
            color.a -= inc;
            image.color = color;

            return color.a <= 0;
        });
        activeCoroutines--;
    }
}
