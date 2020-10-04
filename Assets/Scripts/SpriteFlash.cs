using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlash : MonoBehaviour
{

    public Color flashColor;
    public float flashDuration;
    public int flashTimesMax = 0;

    Material mat;

    private IEnumerator flashCoroutine;

    private void Awake()
    {
        mat = GetComponent<SpriteRenderer>().material;
    }

    private void Start()
    {
        mat.SetColor("_FlashColor", flashColor);
    }

    public void FlashSmooth()
    {
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = DoFlashSmooth();
        StartCoroutine(flashCoroutine);
    }

    public void Flash()
    {
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = DoFlash();
        StartCoroutine(flashCoroutine);
    }

    public void FlashMulitpleTimes()
    {
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = DoFlashTimes();
        StartCoroutine(flashCoroutine);
    }

    private IEnumerator DoFlashSmooth()
    {
        float lerpTime = 0;

        while (lerpTime < flashDuration)
        {
            lerpTime += Time.deltaTime;
            float perc = lerpTime / flashDuration;

            SetFlashAmount(1f - perc);
            yield return null;
        }
        SetFlashAmount(0);
    }

    private IEnumerator DoFlash()
    {
        SetFlashAmount(1);
        yield return new WaitForSeconds(flashDuration);
        SetFlashAmount(0);
    }

    private IEnumerator DoFlashTimes()
    {
        for(int i = 0; i < flashTimesMax; i++)
        {
            SetFlashAmount(1);
            yield return new WaitForSeconds(flashDuration);
            SetFlashAmount(0);
            yield return new WaitForSeconds(flashDuration);
        }
    }

    private void SetFlashAmount(float flashAmount)
    {
        mat.SetFloat("_FlashAmount", flashAmount);
    }

}