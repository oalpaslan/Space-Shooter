using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.0f;

    private void Start()
    {
        transform.Translate(new Vector3(0, 4, 0));
    }

    void MoveEnemy()
    {
        //We move the enemy down with 4 m/sec speed
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);
        //If the enemy goes down through the screen, 
        //it will be transformed to the upside of the screen
        //We transform it with the random value of x, so we don't need to re-create an enemy
        //when that enemy have not been killed.
        if (transform.position.y < -5.6f)
        {
            float randomX = Random.Range(-9.5f, 9.5f);
            transform.position = new Vector3(randomX, 7.5f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.transform.GetComponent<Player>();
        if (other.tag == "Laser")
        {
            GameObject.Destroy(other.gameObject);
            GameObject.Destroy(this.gameObject);
        }
        else if (other.tag == "Player")
        {
            GameObject.Destroy(this.gameObject);
            if (player != null)
            {

                player.Damage();
            }




        }
    }


}
