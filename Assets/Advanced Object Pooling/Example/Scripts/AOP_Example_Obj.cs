using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AOP_Example_Obj : MonoBehaviour {
    
    private Rigidbody rig;

    private void OnEnable() {
        if(rig == null) 
            rig = this.GetComponent<Rigidbody>();
        rig.AddForce(GenerateRandomVector() * 10.0f);
    }

    private void OnDisable() {
        rig.velocity = Vector3.zero;
    }

    private static Vector3 GenerateRandomVector(){
        return new Vector3(Random.value - .5f, Random.value - .5f, Random.value - .5f);
    }
}