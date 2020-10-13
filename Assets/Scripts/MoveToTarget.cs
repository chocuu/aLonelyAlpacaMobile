using UnityEngine;


public class MoveToTarget : MonoBehaviour
{
    [SerializeField] private Transform movingObject;

    public Vector3 target;
    public float moveSpeed;
    public bool go;
    public bool destroyOnArrival;
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
                if(destroyOnArrival)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
