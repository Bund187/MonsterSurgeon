using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public static class Utils
{
    public static IEnumerator FadeOut(Image image, float speed, float limit)
    {
        Color alpha = image.color;
        while (alpha.a > limit)
        {
            alpha.a -= speed;
            image.color = alpha;
            yield return null;
        }
    }

    public static IEnumerator FadeIn(Image image, float speed, float limit)
    {
        Color alpha = image.color;
        while (alpha.a < limit)
        {
            alpha.a += speed;
            image.color = alpha;
            yield return null;
        }
    }
    
    public static IEnumerator FadeIn(SpriteRenderer sr, float speed, float limit)
    {
        Color alpha = sr.color;
        while (alpha.a < limit)
        {
            alpha.a += speed;
            sr.color = alpha;
            yield return null;
        }
    }

    public static IEnumerator FadeOut(SpriteRenderer sr, float speed, float limit)
    {
        Color alpha = sr.color;
        while (alpha.a > limit)
        {
            alpha.a -= speed;
            sr.color = alpha;
            yield return null;
        }
    }

    public static IEnumerator FadeOutVolume(AudioSource audioS, float speed, float limit)
    {
        while (audioS.volume > limit)
        {
            audioS.volume -= speed;
            yield return null;
        }
    }

    public static IEnumerator FadeInVolume(AudioSource audioS, float speed, float limit)
    {
        while (audioS.volume < limit)
        {
            audioS.volume += speed;
            yield return null;
        }
    }

    public static  IEnumerator IncreaseVolume(AudioSource audioS, float step, float maxVolume)
    {
        while (audioS.volume < maxVolume)
        {
            audioS.volume += step;
            yield return null;
        }
    }

    //Returns true when anim has finished
    public static bool AnimationIsFinished(Animator anim)
    {
        //return anim.isActiveAndEnabled && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && !anim.IsInTransition(0);

        if (anim.isActiveAndEnabled)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && !anim.IsInTransition(0))
                return true;
            else
                return false;
        }
        else
            return false;

    }

    public static void OnOFFGameObject(bool activate, GameObject[] gos)
    {
        foreach(GameObject go in gos) go.SetActive(activate);
    }

    public static IEnumerator ShakeGameObject(GameObject shake, float shakeValue1, float shakeValue2)
    {
        float randomShake = Random.Range(shakeValue1, shakeValue2);
        Vector2 startPositionBack = shake.transform.position;
        shake.transform.position = new Vector2(startPositionBack.x + randomShake, shake.transform.position.y - randomShake);
        yield return new WaitForSeconds(0.05f);
        randomShake = Random.Range(shakeValue1, shakeValue2);
        shake.transform.position = new Vector2(startPositionBack.x - randomShake, shake.transform.position.y + randomShake);
        yield return new WaitForSeconds(0.1f);
        randomShake = Random.Range(shakeValue1, shakeValue2);
        shake.transform.position = new Vector2(startPositionBack.x + randomShake, shake.transform.position.y + randomShake);
        yield return new WaitForSeconds(0.07f);
        shake.transform.position = startPositionBack;
    }
    //Hace que vibre un elemento UI con Recttransform
    public static IEnumerator ShakeUI(RectTransform rect, float min, float max)
    {
        Vector2 startPos = rect.anchoredPosition;

        for (int i = 0; i < 3; i++)
        {
            float x = Random.Range(min, max);
            float y = Random.Range(min, max);
            rect.anchoredPosition = startPos + new Vector2(x, y);
            yield return new WaitForSeconds(0.05f);

            x = Random.Range(min, max);
            y = Random.Range(min, max);
            rect.anchoredPosition = startPos + new Vector2(-x, y);
            yield return new WaitForSeconds(0.05f);
        }

        rect.anchoredPosition = startPos;
    }
    public static IEnumerator FlashColor(Outline outline)
    {
        float intensity = 1;
        while (intensity > 0)
        {
            outline.effectColor = new Color(intensity, intensity, intensity);
            yield return new WaitForSeconds(0.05f);
            intensity -= 0.1f;
        }
        outline.effectColor = new Color(intensity, intensity, intensity);
    }

    public static IEnumerator LowLightIntensity(Light light, float step, float finalIntensity)
    {
        while (light.intensity > finalIntensity)
        {
            light.intensity -= step;
            yield return null;
        }
    }

    public static IEnumerator TimeAction(float waitTime, System.Action<bool> returnResult)
    {
        float timeNow = Time.time;
        float totalTime = timeNow + waitTime;
        while (timeNow < totalTime)
        {
            timeNow = Time.time;
            yield return null;
        }
        if (timeNow >= totalTime)
        {
            returnResult(true);
        }

        /* EJEMPLO DE COMO USAR ESTA COROUTINE
         * 
         *  StartCoroutine(Utils.TimeAction(5, ActionReady => { 
                    if (ActionReady)
                    {
                        print("Accion lista");
                    }
            }));
         * */
    }

    //Hace que la parte derecha de un sprite 2D mire hacia un target.
    public static void LookAt2D(Transform target, Transform thisTransform)
    {
        // Si tenemos un target
        if (target != null)
        {
            // Calcula la dirección desde el sprite hacia el target
            Vector3 direction = target.position - thisTransform.position;

            // Calcula el ángulo en radianes
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Aplica la rotación al sprite
            thisTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

}
