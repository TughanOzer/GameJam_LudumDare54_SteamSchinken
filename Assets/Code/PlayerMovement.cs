using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool isMoving;
    Vector3 origPos, targetPos;
    public float timeToMove = 0.2f;
    public float movementRange = .5f;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.W) && !isMoving) {
            StartCoroutine(MovePlayer(new Vector3(0, movementRange, 0)));
        }
        if (Input.GetKeyDown(KeyCode.A) && !isMoving) {
            StartCoroutine(MovePlayer(new Vector3(-movementRange, 0, 0)));
        }
        if (Input.GetKeyDown(KeyCode.S) && !isMoving) {
            StartCoroutine(MovePlayer(new Vector3(0, -movementRange, 0)));
        }
        if (Input.GetKeyDown(KeyCode.D) && !isMoving) {
            StartCoroutine(MovePlayer(new Vector3(movementRange, 0, 0)));
        }
    }



    private IEnumerator MovePlayer(Vector3 direction) {
        isMoving = true;

        float elapsedTime = 0;

        origPos = transform.position;
        targetPos = origPos + direction;

        while(elapsedTime < timeToMove) {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime/timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        //To reduce inprecise coordinates
        transform.position = targetPos;

        isMoving = false;
    }





}
