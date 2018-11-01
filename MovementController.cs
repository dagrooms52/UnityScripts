using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
    public float speed = 1.0f;

    private Vector2 lastCoords;
    private Vector2 targetCoords;
    private float journeyLength;
    private float distCovered;

    void Start() {
        lastCoords = transform.position;
        targetCoords = transform.position;
    }

    void Update()
    {
        Vector2 currentCoords = transform.position;
        var isMoving = currentCoords != targetCoords;
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (!isMoving) {
            // See if we're getting input and start moving
            if (!input.Equals(Vector2.zero)) {
                Vector2 movement;

                if (input.x != 0) {
                    movement = input.x * new Vector2(1.0f, -0.5f);
                }
                else {
                    movement = input.y * new Vector2(1.0f, 0.5f);
                }

                targetCoords = currentCoords + movement;
                journeyLength = Vector2.Distance(lastCoords, targetCoords);

                Move();
            }
        }
        else {
            Move();
        }
    }

    void Move() {
        // Distance moved = time * speed.
        distCovered += Time.deltaTime * speed;

        // Fraction of journey completed = current distance divided by total distance.
        var fracJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        if (fracJourney >= 0.99) {
            transform.position = targetCoords;
            lastCoords = targetCoords;
            distCovered = 0f;
        }
        else {
            transform.position = Vector2.Lerp(lastCoords, targetCoords, fracJourney);
        }
    }
}
