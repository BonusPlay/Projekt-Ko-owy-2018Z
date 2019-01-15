using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleEnemy : MonoBehaviour {

    public SpriteRenderer SR;
    public Vector2 PatrolPoints;
    private Vector2 _points;
    public float Speed;
    bool changedDir = false;

    public int Health = 1;

	// Update is called once per frame
	void Start () {
        _points.x = Mathf.Min(PatrolPoints.x, PatrolPoints.y);
        _points.y = Mathf.Max(PatrolPoints.x, PatrolPoints.y);
        SR.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
	}

    private void Update()
    {
        transform.position = new Vector3(transform.position.x + (Speed * Time.deltaTime), transform.position.y);
        if (transform.position.x < _points.x || transform.position.x > _points.y)
        {
            if (!changedDir)
            {
                Speed = Speed * (-1f);
                changedDir = true;
            }
            
        }
        else
        {
            changedDir = false;
        }
    }


    public void TakeDamage(int damage)
    {
        this.gameObject.name = ("took damage: " + damage);
        Health -= damage;
        if (Health <=0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
    }
}
