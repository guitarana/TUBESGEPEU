                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Animator animator;
    public float speed = 5;
    public float rotationSpeed = 10;
    public int maxInventory = 2;
    public PlayerInventory playerInventory;

	protected Joystick joystick;
    public Rigidbody Controller;
    public VirtualJoystick VirtualJoystick;

    private Transform _camTransform;

    public float Drag = 0.5f;
    public float TerminalRotationSpeed = 25.0f;

    public TowerSolver UISolver;

    // Use this for initialization
    void Start () {
        
		joystick = FindObjectOfType<Joystick> ();
        animator = GetComponent<Animator>();

        Controller.maxAngularVelocity = TerminalRotationSpeed;
        Controller.drag = Drag;

        _camTransform = Camera.main.transform;
    }

    void MoveWithVirtualJoystick()
    {
        Vector3 dir = Vector3.zero;
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        if (dir.magnitude > 1)
        {
            dir.Normalize();
        }


        if (VirtualJoystick.InputVector != Vector3.zero)
        {
            dir = VirtualJoystick.InputVector;
        }

        Vector3 rotatedDir = _camTransform.TransformDirection(dir);
        rotatedDir = new Vector3(rotatedDir.x, 0, rotatedDir.z);
        rotatedDir = rotatedDir.normalized * dir.magnitude;

        Controller.AddForce(rotatedDir * speed);
    }

	// Update is called once per frame
	void Update () {

		var rigidbody = GetComponent<Rigidbody> ();
		rigidbody.velocity = new Vector3 (joystick.Horizontal * 100f,
			rigidbody.velocity.y,
			joystick.Vertical * 100f);

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

        MoveWithVirtualJoystick();
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
            if (playerInventory.inventory.Count < maxInventory)
            {
                playerInventory.inventory.Add(other.gameObject.GetComponent<Item>().value);
                playerInventory.UpdateInventory();
                other.gameObject.SetActive(false);
                GameObject spawner = new GameObject();
                spawner.AddComponent<ItemSpawner>();
                spawner.GetComponent<ItemSpawner>().itemToSpawn = other.gameObject;
            }
        }
    }


    public Tower currentTower;
    public void ShowSolver(bool bVal,ref int currentValue, int towerValue)
    {
        UISolver.gameObject.SetActive(bVal);
        UISolver.value = towerValue;
        UISolver.currentValueRed = currentValue;
        UISolver.owner = this.gameObject;
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  