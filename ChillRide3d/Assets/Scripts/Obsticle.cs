using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticle : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);

        if(transform.position.z < -10f)
        {
            GameManager.instance.ScoreUp();
            Destroy(gameObject);
        }
    }
}
