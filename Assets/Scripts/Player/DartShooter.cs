using UnityEngine;

public class DartShooter : MonoBehaviour
{
    [SerializeField] GameObject scopeCanvas;

    private bool isAiming;
    public void Shoot()    
    {
        if (isAiming)
        {
            //Suena el disparo
            //Pequeño shake del aim
            //Disparo
            print("Se dispara la pistola de dardos");
        }
    }

    public void Aim()
    {
        isAiming = true;
        scopeCanvas.SetActive(isAiming);
    }
    public void ReleaseAim()
    {
        isAiming = false;
        scopeCanvas.SetActive(isAiming);
    }
}
