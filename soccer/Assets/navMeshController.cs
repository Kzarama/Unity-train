using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navMeshController : MonoBehaviour {

  public Transform objective;

  private NavMeshAgent agent;

  void Start() {
    agent = GetComponent<NavMeshAgent>();
  }

  void Update() {
    agent.destination = objective.position;
  }
}
