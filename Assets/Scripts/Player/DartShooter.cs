using System.Collections;
using UnityEngine;

public class DartShooter : MonoBehaviour
{
    [SerializeField] GameObject scopeCanvas;
    [SerializeField] RectTransform rectAim;
    [SerializeField] AudioSource audioS;
    [SerializeField] AudioClip shotSound;

    private bool isAiming, canShoot;
    private RectTransform initialPosition;

    private void Start()
    {
        canShoot=true;
        initialPosition = rectAim;
    }
    public void Shoot()    
    {
        if (isAiming && canShoot)
        {
            //Suena el disparo
            //Pequeño shake del aim
            //Disparo
            audioS.PlayOneShot(shotSound);
            print("Se dispara la pistola de dardos");
            StartCoroutine(Utils.ShakeUI(rectAim, -10f, 10f));
            canShoot = false;
            StartCoroutine(CoolDown());
        }
    }

    public void Aim()
    {
        isAiming = true;
        scopeCanvas.SetActive(isAiming);
        rectAim = initialPosition;
    }
    public void ReleaseAim()
    {
        isAiming = false;
        scopeCanvas.SetActive(isAiming);
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }
}
