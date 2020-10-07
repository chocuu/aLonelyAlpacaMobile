using UnityEngine;


public class MoveToTarget : MonoBehaviour
{
    [SerializeField] private Transform movingObject;

    [SerializeField] private Vector3 target;
    [SerializeField] private float moveSpeed;
    public bool go;
    private Vector3 initPos;
    
    private void Awake() 
    {
        if(movingObject == null)
        {
            movingObject = gameObject.transform;
        }    
    }

    // Update is called once per frame
    void Update()
    {
        if(go) {
            movingObject.position = Vector3.MoveTowards(movingObject.position, target, moveSpeed*Time.deltaTime);
            if (Vector3.Distance(movingObject.position, target) < 0.01f) {
                go = false;
            }
        }
    }
}
