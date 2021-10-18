using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour {

  public float speed = 4;
  public float rotationSpeed = 80;
  public float rotation = 0f;
  public GameObject screamSound;

  public bool allowMovement = true;

  Vector3 moveDir = Vector3.zero;

  CharacterController controller;
  Animator animation;
  AudioSource audioSource;

  void Start() {
    controller = GetComponent<CharacterController>();
    animation = GetComponent<Animator>();
    audioSource = GetComponent<AudioSource>();
  }

  void Update() {
    if (controller.isGrounded && allowMovement) {
      if (Input.GetMouseButton(0)) {
        stop();
        StartCoroutine("attack");
      } else if (Input.GetKey(KeyCode.Space)) {
        stop();
        StartCoroutine("scream");
      } else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W)) {
        move(speed * 2, 2, 1f);
      } else if (Input.GetKey(KeyCode.W)) {
        move(speed, 1, 1f);
      } else if (Input.GetKey(KeyCode.S)) {
        move(speed, 1, -1f);
      } else if (Input.GetKeyUp(KeyCode.LeftShift)) {
        stop();
      } else if (Input.GetKeyUp(KeyCode.W)) {
        stop();
      } else {
        stop();
      }
    }
    rotation += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
    transform.eulerAngles = new Vector3(0, rotation, 0);

    controller.Move(moveDir * Time.deltaTime);
  }

  IEnumerator attack() {
    allowMovement = false;
    animation.SetInteger("Condition", 3);
    yield return new WaitForSeconds(2.5f);
    allowMovement = true;
  }

  IEnumerator scream() {
    allowMovement = false;
    animation.SetInteger("Condition", 4);
    yield return new WaitForSeconds(0.5f);
    audioSource.Play();
    yield return new WaitForSeconds(2f);
    allowMovement = true;
  }

  void move(float playerSpeed, int conditionState, float forward) {
    animation.SetInteger("Condition", conditionState);
    animation.SetFloat("Speed", forward);
    moveDir = new Vector3(0, 0, 1);
    moveDir *= playerSpeed * forward;
    moveDir = transform.TransformDirection(moveDir);
  }

  void stop() {
    animation.SetInteger("Condition", 0);
    moveDir = new Vector3(0, 0, 0);
  }
}
