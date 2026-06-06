using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu]
public class Fireball : SpellBase
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float damage;
    [SerializeField] private float speed;

    public override void CastSpell(Transform transformOrigin)
    {
        //Obetenemos la direccion en la que se va a disparar el hechizo
        Vector2 direction = (transformOrigin.position - transformOrigin.parent.position).normalized;

        GameObject go = Instantiate(prefab, transformOrigin.position, transformOrigin.rotation);
        go.GetComponent<FireballBehaviour>().Initialize(direction, damage, speed);
    }
}
