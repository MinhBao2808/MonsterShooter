using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAndMoveBoss : MonoBehaviour {
    private Transform playerTransform;
    [SerializeField] private float rotationSpeed;
    private string _targetName = "Player";
    // Use this for initialization

    private void Start() {
        playerTransform = GetTransformFromTagName(_targetName);
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log(AngleBetweenVector2(transform.position, playerTransform.position) + 90.0f);
        if (GameManger.instance.GameOver == false) {
            //playerTransform = GetTransformFromTagName(_targetName);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, AngleBetweenVector2(transform.position, playerTransform.position) + 90.0f), 0.5f);
        }
	}

	private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2) {
		Vector2 diference = vec2 - vec1;
		float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
		return Vector2.Angle(Vector2.right, diference) * sign;
	}
    Transform GetTransformFromTagName(string tagName) {
        if (string.IsNullOrEmpty(tagName)) {
            return null;
        }
        GameObject goTarget = GameObject.FindGameObjectWithTag(tagName);
        if (goTarget == null) {
            return null;
        }
        return goTarget.transform;
    }
}
