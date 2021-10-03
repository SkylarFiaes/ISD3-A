using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MovementManager : MonoBehaviour
{
    public float tankSpeed, destinationFireSpot; // Speed of the tank; Distance to the fire spot.
    public bool elementChoosen = false; // Variable that checks if player have choosen an element to attack.
    public bool isInTransit = false; // Variables that checks if tank is still moving.
    public GameObject fireSpot, currentTankSelected, elementToAttack; // Sets the destination of the tanks; Selects the tank, selects the element to attack.
    GameObject tankMovementManagerGO;

    public void SelectTankDestination(GameObject elementToAttack) // When player clicks a Destination, the event trigger on every outpost triggers this code:
    {  
        if (elementChoosen == true) // (This variable turns into true on the tankMovementManager Script, after player choosing a destination). If the elementChoosen is true: 
        {            
            NavMeshAgent agent = transform.GetComponent<NavMeshAgent>(); // Activates the navigation of the tank.
            agent.speed = tankSpeed; // Looks for the speed of the tank choosen and uses it.
            agent.destination = elementToAttack.GetComponent<TankFireDestination>().GetTankDestination().transform.position; // Moves the tank to the fire spot of the element that was clicked.
            agent.stoppingDistance = destinationFireSpot; // Place where the tank should stop.
            fireSpot = elementToAttack; // Changes the current destination to destination of the fire spot of the element that was clicked.      
            StartCoroutine(WaitForAgent());             
        }
    }
    public void NewDestinationSelected(GameObject elementToAttack) // When player clicks the element that they want to attack.
    {
        if(elementChoosen)
        {
            //currentTankSelected.SelectTankDestination(elementToAttack); // Activates the destination of the tank.
            elementChoosen = false;
        }
    }
    public void SetNewCurrentTank(GameObject newTank) // When player clicks a tank:
    {
        if(!elementChoosen)
        {
            currentTankSelected = newTank; // Sets that to the current tank.
        }
    }    
    //we wait for one second because the remaining distance value on nav mesh agent does not update in time for the update check
    IEnumerator WaitForAgent()
    {
        yield return new WaitForSeconds(1);
        isInTransit = true; // Turns the variable that checks if the tank is moving true (so player can't touch a tank while this is flying).  
    }
    void Update() // Checking if tank reached the destination.
    {
        if(transform.GetComponent<NavMeshAgent>().remainingDistance <= destinationFireSpot && isInTransit) // If the remaining distance is equal or smaller than the localization of the fire spot
        // and the tank is moving, this code happens:
        {
            elementChoosen = false; // Variable that allows player to choose the element to attack goes to false, again.
            isInTransit = false; // Variable that allows tank to move, turns false and tank stops moving.
        }
    }
}