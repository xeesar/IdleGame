using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conf_Par : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 2);
        StartCoroutine(ie_A());        
    }

    // Update is called once per frame
   IEnumerator ie_A()
    {
        this.transform.localPosition = new Vector3(0, 8, 0);// new Vector3( Random.Range(-5.0f, +5.0f), Random.Range(+0.0f, +10.0f),0);
        Vector3 _v_spd = new Vector3( Random.Range(-10.0f, +10.0f), 0, Random.Range(-3.0f, +3.0f) );
        Vector3 _v_rot = new Vector3(Random.Range(-360,360), Random.Range(-360, 360), Random.Range(-360, 360));
        float _t = 0;
        float _tt = 0;
        float _speed = Random.Range(0.50f, 4.00f);

        while (_tt < 1)
        {
            _t += Time.deltaTime* Confeti._._cur_Speed.Evaluate(_t)* _speed;
            _tt += Time.deltaTime*0.5f;
           // float _sp = Confeti._._cur_Speed.Evaluate(_t);

            this.transform.localPosition = new Vector3(  _v_spd.x * _t, Confeti._._cur_Y.Evaluate(_t) *20-11, 0);// + new Vector3(0, -Time.deltaTime*4, 0);
            this.transform.rotation *= Quaternion.Euler( _v_rot * Time.deltaTime*2 );
            this.transform.localScale = new Vector3(1, 1, 1) * Confeti._._cur_Scale.Evaluate(_tt);

            yield return null;
        }
        
        Destroy(this.gameObject);

    }
}
