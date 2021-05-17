using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
        public int mode=0;
        //public GameObject Tama;
	    //この変数にインスペクタ上から玉オブジェを指定します
        public int score=0;

        
        Vector3 MainCameraDef;
        Quaternion TamaRotationDef;

        public GameObject TamaPrefeb;

        float GameTimer;
        float GameTimerMAX;
        
        public GameObject MatoPrefab;

        public int EnemyCount;
        int EnemyCountMax;

        Quaternion CameraRotationDef;

        public AudioClip shootSe;

    // Start is called before the first frame update
    void Start()
    {
        EnemyCount=0;
        EnemyCountMax=10;

        MainCameraDef=transform.position;
        CameraRotationDef=transform.rotation;
        //TamaRotationDef=Tama.transform.rotation;

        //GameTimerMAX=10;
        GameTimerMAX=50;
        GameTimer=GameTimerMAX;
    }

    // Update is called once per frame
    void Update()
    {
        if(mode==0)
        {
            //移動
            GetComponent<Rigidbody>().AddRelativeForce(0,0,Input.GetAxis("Vertical"),ForceMode.Impulse);

            //回転(直に動かす場合)
            transform.Rotate(0,Input.GetAxis("Horizontal"),0);
            //transform.Rotate(0,Input.acceleration.x*5,0);

            //この後、見えない壁を作って、カメラが落ちないようにする
            //サイズは横200、奥行き1、たかさ100とかおおげさにしておく
            //cubeで、壁っぽいオブジェを四方につくってから、「Mesh Renderer」のチェックをはずし見えなくする


            GameTimer-=Time.deltaTime;
            if(GameTimer<0)
            {
                mode=2;
            }

            if(EnemyCount < EnemyCountMax && ( EnemyCount<=0 || Random.Range(0,100)==0 ))
            {
                GameObject Enemy = GameObject.Instantiate(MatoPrefab);
                Vector3 PosTemp = transform.position;//カメラの場所に出現
                PosTemp.z-=Random.Range(50,100);//出現場所をカメラから50~100の間をランダムで離す
                Enemy.transform.position=PosTemp;
                //カメラの周囲でどこかにあらわれる
                Enemy.transform.RotateAround(transform.position,transform.up,Random.Range(0,360));
                EnemyCount++;
            }


            //玉を飛ばす前の処理
            /*
            //マウスの位置を取得
            Vector3 screenPoint = Input.mousePosition;
            //z座標は画面に映るよう固定値にしておく(カメラからの距離)
            screenPoint.z = 10.0f;
            //マウスの座標を今写っているカメラからみたゲーム座標に変換する
            Vector3 worldPoint = GetComponent<Camera>().ScreenToWorldPoint(screenPoint);//スクリプトがカメラについている前提
            //変換した座標を動かしたいオブジェクトの座標に入れる
            Tama.transform.position = worldPoint;
            */

            //if(Input.anyKeyDown)
            if(Input.GetButtonDown("Fire1"))
                //Fire1はもともと左ctrlキーに設定されているので、spaceを指定するといいかも。
                //edit->ProjectSetting->Inputで設定できます
            {
                GameObject bullet = GameObject.Instantiate(TamaPrefeb);
                bullet.transform.position = transform.position;
                bullet.GetComponent<Rigidbody>().useGravity=true;
                bullet.GetComponent<Collider>().isTrigger=false;
                Vector3 speed;
                /*
                speed.x=0;
                speed.y=0;
                speed.z=100;
                */
                speed = transform.forward * 100;
                bullet.GetComponent<Rigidbody>().AddForce(speed,ForceMode.Impulse);

                GetComponent<AudioSource>().PlayOneShot(shootSe);
            }

        }
        else if(mode==1)
        {
        }
        else if(mode==2)
        {
        }
    }

    public GUIStyle style;
    void OnGUI()
    {
        if(mode==0)
        {

            GUI.Label(new Rect(0,0,100,100),"残り時間:"+Mathf.Ceil(GameTimer),style);	
            GUI.Label(new Rect(0,50,100,100),score+"点",style);
        }

        if(mode==2)
        {
            if(score!=0)
                GUI.Label(new Rect(0,0,100,100),"やったね！ "+score+"点",style);
            else
                GUI.Label(new Rect(0,0,100,100),"ひとつもあたらない！！ ",style);

            if(GUI.Button(new Rect(Screen.width-100,Screen.height-50,100,50),"リトライ"))
            {
                mode=0;
                score=0;
                transform.position=MainCameraDef;
                /*
                Tama.GetComponent<Rigidbody>().useGravity=false;
                Tama.GetComponent<Rigidbody>().velocity = Vector3.zero;
                Tama.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                Tama.transform.rotation=TamaRotationDef;
                GameTimer=GameTimerMAX;
                */

                /*
                Mato.GetComponent<Rigidbody>().velocity = Vector3.zero;
                Mato.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                Mato.transform.position=MatoDef;
                Mato.transform.rotation=MatoRotationDef;
                */

                transform.rotation=CameraRotationDef;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                GameObject[] Enes = GameObject.FindGameObjectsWithTag("Enemy");
                foreach(GameObject e in Enes)
                {
                    Destroy(e);
                }
                EnemyCount=0;

            }	
        }
    }

    void OnCollisionEnter(Collision col)
{
	if(mode==0)
	{
		if(col.gameObject.name=="mato")
		{
			MatoMobe EnemyScript;
			EnemyScript=col.gameObject.transform.parent.gameObject.GetComponent<MatoMobe>();

			if(EnemyScript.DeathTimer==-1)
			{
				EnemyScript.DeathTimer=1;
				mode=2;
			}
		}
	}
}


    

}
