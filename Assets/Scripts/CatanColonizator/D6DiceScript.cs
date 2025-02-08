using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D6DiceScript : MonoBehaviour
{
    public Transform[] Edges;
    //[SerializeField]
    //Rigidbody rigidbody;
    [SerializeField]
    bool isActive = true;
    //[SerializeField]
    //float timer = 10f;
    //[SerializeField]
    //float currentTime = 10f;

    private void Start() {
        //rigidbody = GetComponent<Rigidbody>();
    }

    public int checkResult() {
        int result = 0;
        float Height = -1;

        for (int i = 0; i < Edges.Length; i++) {
            if (Edges[i].position.y > Height) {
                result = i + 1;
                Height = Edges[i].position.y;
            }
        }
        

        return result;
    }

    private void FixedUpdate() {
        if (isActive == true && GetComponent<Rigidbody>().velocity.magnitude < 1) {
            Debug.Log(this.gameObject.transform.name + " is result " + checkResult());
            isActive = false;
        }
    }

    public void throwDice() {
        isActive = true;
        Random.InitState(Time.frameCount + 2);
        GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(3, 5), Random.Range(5, 10), Random.Range(-5, -3)), ForceMode.VelocityChange);
        Random.InitState(Time.frameCount + 5);
        GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(30, 60), Random.Range(30, 60), Random.Range(30, 60)), ForceMode.VelocityChange);
    }
}
