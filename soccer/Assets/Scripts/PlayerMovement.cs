using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public float speed = 1;
  public float rotationSpeed = 1;
  public float jumpForce = 1;

  private Rigidbody player;

  void Start() {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;

    player = GetComponent<Rigidbody>();
  }

  void Update() {
    Move();

    kickBall();
  }

  void Move() {
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");

    transform.Translate(new Vector3(horizontal, 0f, vertical) * Time.deltaTime * speed);

    float rotationY = Input.GetAxis("Mouse X");
    transform.Rotate(new Vector3(0, rotationY * Time.deltaTime * rotationSpeed));

    if (Input.GetKeyDown(KeyCode.Space)) {
      player.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }
  }

  private void kickBall() {
    RaycastHit hit;
    Vector3 playerPosition = transform.position - new Vector3(0, 0.75f, 0);
    // Debug.DrawRay(playerPosition, transform.TransformDirection(Vector3.forward) * 1f, Color.red);
    if (Physics.Raycast(playerPosition, transform.TransformDirection(Vector3.forward), out hit, 1f) && 
        hit.collider.gameObject.name == "Ball" &&
        Input.GetKeyDown(KeyCode.LeftControl)) {
      GameObject Ball = GameObject.Find("Ball");
      Rigidbody rb = Ball.GetComponent<Rigidbody>();
      rb.velocity = (Camera.main.transform.forward * 10f + new Vector3(1, -10, 1));
    }
  }
}
