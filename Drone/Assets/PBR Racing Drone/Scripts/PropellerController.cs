using UnityEngine;
using UnityEngine.InputSystem;
public class PropellerController : MonoBehaviour
{
    [Header("Engine Config")]
    public float maxSpinSpeed = 2500f;
    public float spinAcceleration =1500f;
    private Transform [] props;
    private float currentSpinSpeed = 0f;
    public static bool isEngineOn = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int propCount = 0;
        props = new Transform[4];
        foreach(Transform child in transform){
            if(child.name.ToLower().Contains("motor")){
                props[propCount] = child.GetChild(0);
                propCount++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.iKey.wasPressedThisFrame){
            isEngineOn = !isEngineOn;
        }
        if(isEngineOn)
        {
            currentSpinSpeed = Mathf.MoveTowards(currentSpinSpeed,maxSpinSpeed,spinAcceleration*Time.deltaTime);
        }
        else
        {
            currentSpinSpeed = Mathf.MoveTowards(currentSpinSpeed,0f,spinAcceleration*Time.deltaTime);
        }
        if(currentSpinSpeed>0){
            foreach(Transform prop in props){
                prop.Rotate(0,0,currentSpinSpeed*Time.deltaTime);
            }
        }
    }
}
