using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{

    [SerializeField]
    private GameObject GlassShard;
    [SerializeField]
    private Collider2D c;

    private float randomRotation;
    private float randomPositionx;
    private float randomPositionz;

    private void Start()
    {
        c = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 7)
        {
            for (int i = 0; i < 20; i++)
            {
                randomRotation = Random.Range(1,91);
                Quaternion rotation = Quaternion.Euler(0, 0, randomRotation);

                randomPositionx = Random.Range(-1f, 1f); 
                randomPositionz = Random.Range(-1f, 1f);
                Vector3 position = new Vector3(transform.position.x + randomPositionx, transform.position.y + randomPositionz, 0.2f);
                Instantiate(GlassShard, position, rotation);
            }
            Destroy(gameObject);
        }
    }
    

}
