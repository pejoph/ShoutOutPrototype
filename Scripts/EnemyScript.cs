using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform player;
    public float speed = .02f;

    private float deathTimer = 0f;

	void Update ()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed);

        deathTimer += Time.deltaTime;
        if (deathTimer > .2f / speed)
        {
            Destroy(gameObject);
        }
	}
}
