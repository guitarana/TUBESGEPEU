using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Animator animator;
    public float speed = 5;
    public float rotationSpeed = 10;
    public int maxInventory = 2;
    public List<string> inventory;

	// Use this for initialization
	void Start () {
        inventory = new List<string>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("Run", true);
            Move(Vector3.forward);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Rotate(1 * rotationSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Rotate(-1 * rotationSpeed);
        }

    }

    public void Move(Vector3 dir)
    {
       transform.Translate(dir * speed * Time.deltaTime);
    }

    public void Rotate(float degree)
    {
        transform.Rotate(Vector3.up, degree * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Item>())
        {
            if (inventory.Count < maxInventory)
            {
                inventory.Add(other.gameObject.GetComponent<Item>().value);
                Destroy(other.gameObject);
            }
        }
    }
}
