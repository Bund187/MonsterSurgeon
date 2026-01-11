using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public AnimationCurve curve; //Esto hace que se cree en el inspector una curva. El eje vertical será la fuerza del shake y la horizontal el tiempo que dura el shake
    public float duration = 1f;

    public void StartShake(float duration_)
    {
        duration= duration_;
        StartCoroutine("Shaking");
    }

    public IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;                                     //Almacenamos la posicion inicial de la camara para resetearla más tarde
        float elapsedTime = 0f;                                                         

        while (elapsedTime < duration)                                                   
        {
            elapsedTime += Time.deltaTime;                                              //Se va incrementando con el tiempo
            float strength = curve.Evaluate(elapsedTime / duration);                    //Se evalua la parte del tiempo (eje horizontal) de la curva para lograr la fuerza
            transform.position = startPosition + Random.insideUnitSphere * strength;    //Se suma la posicion de la camara por un punto aleatorio por la fuerza y se añade a la posicion actual de la camara (genera el movimiento de la camara)
            yield return null;
        }

        transform.position = startPosition;
        ResetShake();
    }

    private void ResetShake()
    {
        duration = 1f;
    }
}
