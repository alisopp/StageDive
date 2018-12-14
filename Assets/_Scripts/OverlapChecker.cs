using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapChecker : MonoBehaviour {

    private Collider2D collider2D;
    private Rigidbody2D rigidbody2D;
    private ContactFilter2D contactFilter2;

    // Use this for initialization
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        contactFilter2 = new ContactFilter2D();
        contactFilter2.SetLayerMask(LayerMask.GetMask("Symbols"));
        contactFilter2.useLayerMask = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Check");
            Collider2D[] results = new Collider2D[5];
            Physics2D.OverlapCollider(collider2D, contactFilter2, results);
            foreach (var collider in results)
            {
                if (collider != null)
                {
                    Debug.Log("Destroy");
                    Destroy(collider.gameObject);
                }
            }

        }
    }
}
