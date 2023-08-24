using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    public GameObject expEffect;
    public Texture[] textures;
    public float radius = 10.0f;
    private new MeshRenderer renderer;

    private Transform tr;
    private Rigidbody rb;

    private int hitCount = 0;

    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        renderer = GetComponentInChildren<MeshRenderer>();

        int idx = Random.Range(0, textures.Length);
        renderer.material.mainTexture = textures[idx];
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BULLET"))
        {
            if (++hitCount == 3)
            {
                ExpBarrel();
            }
        }
    }

    void ExpBarrel()
    {
        GameObject exp = Instantiate(expEffect, tr.position, Quaternion.identity);
        Destroy(exp, 5.0f);

        IndirectDamage(tr.position);

        Destroy(gameObject, 3.0f);
    }

    void IndirectDamage(Vector3 pos)
    {
        Collider[] colls = Physics.OverlapSphere(pos, radius, 1 << 3);

        foreach(var coll in colls)
        {
            rb = coll.GetComponent<Rigidbody>();
            rb.mass = 1.0f;
            rb.constraints = RigidbodyConstraints.None; // freezeRotation Á¦ÇÑ°ªÀ» ÇØÁ¦
            rb.AddExplosionForce(1500.0f, pos, radius, 1200.0f); // (È¾ Æø¹ß·Â, Æø¹ß ¿øÁ¡, Æø¹ß ¹Ý°æ, Á¾ Æø¹ß·Â)
        }
    }
}
