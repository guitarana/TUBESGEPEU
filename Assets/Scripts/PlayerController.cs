                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    private Animator animator;
    public float speed = 5;
    public float rotationSpeed = 10;
    public int maxInventory = 2;
    public PlayerInventory playerInventory;

	public Rigidbody Controller;
	public VirtualJoystick VirtualJoystick;

    private Transform _camTransform;

    public float Drag = 0.5f;
    public float TerminalRotationSpeed = 25.0f;

    public TowerSolver UISolver;

    // Use this for initialization
    void Start () {
	    animator = GetComponent<Animator>();

        Controller.maxAngularVelocity = TerminalRotationSpeed;
        Controller.drag = Drag;

        _camTransform = Camera.main.transform;
    }

    void MoveWithVirtualJoystick()
    {

        Vector3 dir = Vector3.zero;
        dir.x = CrossPlatformInputManager.GetAxis("Horizontal");
        dir.z = CrossPlatformInputManager.GetAxis("Vertical");


        Vector3 rotatedDir = _camTransform.TransformDirection(dir);
        rotatedDir = new Vector3(rotatedDir.x, 0, rotatedDir.z);
        rotatedDir = rotatedDir.normalized * dir.magnitude;
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(dir.x * -speed,
            rigidbody.velocity.y,
            dir.z * -speed);

        Debug.Log("Velocity :" + rigidbody.velocity.magnitude);

        if (dir.x != 0 || dir.z != 0)
        {
            Rotating(-dir.x, -dir.z);
            animator.SetBool("Run", true);
        }
        else {
            animator.SetBool("Run", false);
        }

     

    }

    void Rotating(float horizontal, float vertical)
    {
        // Create a new vector of the horizontal and vertical inputs.
        Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);

        // Create a rotation based on this new vector assuming that up is the global y axis.
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        var rigidbody = GetComponent<Rigidbody>();

        // Create a rotation that is an increment closer to the target rotation from the player's rotation.
        Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, 8f * Time.deltaTime);

        // Change the players rotation to this new rotation.
   //     rigidbody.MoveRotation(newRotation);

        transform.rotation = newRotation;
    }

    // Update is called once per frame
    void Update () {
        // ini
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
            for (int i = 0; i < playerInventory.inventory.Length; i++)
            {
                if (playerInventory.inventory[i] == "")
                {
                    playerInventory.inventory[i] = other.gameObject.GetComponent<Item>().value;
                    playerInventory.UpdateInventory();
                    other.gameObject.SetActive(false);
                    GameObject spawner = new GameObject();
                    spawner.AddComponent<ItemSpawner>();
                    spawner.GetComponent<ItemSpawner>().itemToSpawn = other.gameObject;
                    break;
                }
            }
        }
    }


    public void ShowSolver(bool bVal, int towerValue, Tower currentTower)
    {

        Debug.Log("Tampilkan panel");
        UISolver.gameObject.SetActive(bVal);
        UISolver.value = towerValue;
        UISolver.owner = this.gameObject;
        UISolver.currentTower = currentTower;

    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  