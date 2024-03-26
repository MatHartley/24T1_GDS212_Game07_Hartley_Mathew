using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEmission : MonoBehaviour
{
    [SerializeField] float radius = 2;
    List<Collider2D> collisions = new List<Collider2D>();

    // Update is called once per frame
    void Update()
    {
        var contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(LayerMask.GetMask("Enemy"));
        contactFilter.useLayerMask = true;
        Physics2D.OverlapCircle(transform.position, radius, contactFilter, collisions);

        foreach (Collider2D nearbyObject in collisions)
        {
            Debug.Log("Detected");
        }
    }
}
