using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

  public int score1 = 0;
  public int score2 = 0;

  void Start() {
    
  }

  void Update() {
    if (transform.position.x < -11 || transform.position.x > 11) {
      resetPosition();
    }
  }

  public void OnTriggerEnter(Collider collider) {
    if (collider.gameObject.tag == "Goal2") {
      score1++;
      GameObject.Find("localScore").GetComponent<TextMesh>().text = score1 + "";
    } else if (collider.gameObject.tag == "Goal1") {
      score2++;
      GameObject.Find("visitScore").GetComponent<TextMesh>().text = score2 + "";
    }
    resetPosition();
  }

  public void resetPosition() {
    transform.position = new Vector3(0, 1, 0);
    GameObject Ball = GameObject.Find("Ball");
    Rigidbody rb = Ball.GetComponent<Rigidbody>();
    rb.velocity = Vector3.zero;
    rb.angularVelocity = Vector3.zero;
  }
}
