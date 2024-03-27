using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEmission : MonoBehaviour
{
    [SerializeField] float sneakRadius;
    [SerializeField] float runRadius;
    [SerializeField] float jumpRadius;
    [SerializeField] float dashRadius;

    List<Collider2D> collisions = new List<Collider2D>();

    public PlayerMovement playerMovement;
    public Enemy enemy;

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.sneak)
        {
            var contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(LayerMask.GetMask("Enemy"));
            contactFilter.useLayerMask = true;
            Physics2D.OverlapCircle(transform.position, sneakRadius, contactFilter, collisions);
        }
        else if (playerMovement.run)
        {
            var contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(LayerMask.GetMask("Enemy"));
            contactFilter.useLayerMask = true;
            Physics2D.OverlapCircle(transform.position, runRadius, contactFilter, collisions);
        }
        else if (playerMovement.jump)
        {
            var contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(LayerMask.GetMask("Enemy"));
            contactFilter.useLayerMask = true;
            Physics2D.OverlapCircle(transform.position, jumpRadius, contactFilter, collisions);
        }
        else if (playerMovement.dash)
        {
            var contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(LayerMask.GetMask("Enemy"));
            contactFilter.useLayerMask = true;
            Physics2D.OverlapCircle(transform.position, dashRadius, contactFilter, collisions);
        }

        foreach (Collider2D nearbyObject in collisions)
        {
            nearbyObject.GetComponent<Enemy>().ToggleAlert();

        }
    }
}
