using UnityEngine;

public class SpellAimer : MonoBehaviour
{
    [SerializeField] private float radius = 2f;      
    [SerializeField] private GameObject aimIndicator; 
    private PlayerInputReader input;

    private void Awake()
    {
        input = GetComponentInParent<PlayerInputReader>();
        aimIndicator.SetActive(false);
    }

    private void Update()
    {
        Vector2 aimInput = input.Look;

        if (aimInput.sqrMagnitude > 0.01f)
        {
            aimIndicator.SetActive(true);
            // Normalizar garantiza que siempre esté en el borde del círculo
            Vector2 direction = aimInput.normalized;
            aimIndicator.transform.localPosition = direction * radius;
        }
        else
        {
            aimIndicator.SetActive(false);
        }
    }
}
