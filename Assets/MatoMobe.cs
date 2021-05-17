using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatoMobe : MonoBehaviour
{
    GameObject TraceTarget;
    MainScript MainSc;
    public float DeathTimer;

    // Start is called before the first frame update
    void Start()
    {
        TraceTarget=GameObject.Find("Main Camera");
        MainSc=TraceTarget.GetComponent<MainScript>();
        DeathTimer=-1;
    }

    // Update is called once per frame
    void Update()
    {

        if(DeathTimer==-1)
        {
            Vector3 SetPos=TraceTarget.transform.position;

            //y軸は無視してカメラのほうをむく
            SetPos.y=transform.position.y;
            transform.LookAt(SetPos);//LookAtでカメラの方を向く

            //カメラの方にいく
            SetPos.x=0;
            SetPos.y=0;
            SetPos.z=10*Time.deltaTime;
            transform.Translate(SetPos);

        }
        else
        {
            DeathTimer-=Time.deltaTime;
            if(DeathTimer<=1)
            {
                Vector3 LsTemp = transform.localScale;
                LsTemp.x-=Time.deltaTime;
                LsTemp.y-=Time.deltaTime;
                LsTemp.z-=Time.deltaTime;
                transform.localScale = LsTemp;
            }
            if(DeathTimer<=0)
            {
                Destroy(this.gameObject);
                MainSc.EnemyCount--;
            }
        }


    }
}
