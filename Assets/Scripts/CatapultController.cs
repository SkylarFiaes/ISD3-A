using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public class CatapultController : MonoBehaviour
{
    [SerializeField] private InputAction movement = new InputAction();
    [SerializeField] private LayerMask layerMask = new LayerMask();
    private Camera cam = null;
    private NavMeshAgent agent = null;
    void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }
    private void OnEnable()
    {
        movement.Enable();    
    }
    private void OnDisable()
    {
        movement.Disable();    
    }

    private void Update()
    {
        HandleInput();
    }
    private void HandleInput()
    {
        if (movement.ReadValue<float>() == 1)
        {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, layerMask))
            {
                PlayerMove(hit.point);
            }
        }
    }
    private void PlayerMove(Vector3 location)
    {
        agent.SetDestination(location);
    }
}