using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracontrol : MonoBehaviour {

    public Transform cameraTarget;
    public Transform resetCamera;
    public Transform cameraPos;
    public Transform player;

    //public Vector3 setCameraPos;

    Vector3 temp;
    bool isCamera;

    float time;
    float v;
    float h;

    Quaternion rotateAngle;
    Vector3 direction;
 
    // Use this for initialization
    void Start()
    {
        temp = player.position - resetCamera.position;
        cameraPos.position = resetCamera.position;
        v= 77f;
        h = 185f;
        

    }

    // Update is called once per frame
    void Update()
    {
        SetCamera();
    }

    void SetCamera()
    {

        h = transform.eulerAngles.y;
        

        if(Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.Joystick1Button4)){
             isCamera = true;

             temp = player.position - cameraPos.position;
             transform.LookAt(cameraTarget);
             v = 77f;
             transform.position = player.position + rotateAngle * direction;
             time=0;
        }


        if (isCamera==false){
            
            transform.position = player.position - temp;
            

            if(Input.GetAxis("RightVertical") != 0 || Input.GetAxis("RightHorizontal") != 0){
                h += Input.GetAxisRaw("RightHorizontal") * 0.5f;
                v -= Input.GetAxisRaw("RightVertical") * 0.5f;
    
                v = ClampAngle(v, 45, 130);
    
                rotateAngle = Quaternion.Euler(v, h, 0);
    
                direction = new Vector3(0f, -3f, -3.5f);
                transform.position = player.position + rotateAngle * direction;
                
                v = ClampAngle(v, 45, 130);
                
                resetCamera.position = transform.position;
                temp = player.position - resetCamera.position;
                
            }

            transform.LookAt(cameraTarget);
            
        }else {

            time+=Time.deltaTime;
            
            if(time<0.1f){
              transform.position = Vector3.Lerp(transform.position, resetCamera.position, 0.6f);
            }
            if (time > 0.1f)
            {
                transform.position = player.position - temp;

                isCamera=false;
                
            }

            

            transform.LookAt(cameraTarget);

        }

        
        
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

}
