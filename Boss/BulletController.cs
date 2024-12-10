using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float deleteTime = 3.0f; //削除されるまでの時間
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,deleteTime); //deleteTime後にDestroy
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject); //何かに衝突したら消す    
    }
}
