using System.Collections;
using Models.Particles;
using UnityEngine;
using Random = UnityEngine.Random;

public class Confeti : BaseParticle
{
    #region Fields
    
    public static Confeti _;
    public AnimationCurve _cur_Speed;
    public AnimationCurve _cur_Y;
    public AnimationCurve _cur_Scale;
    
    public Renderer _pref_Par;
    public Material[] _mat_Par;

    public float _k_test = 1.0f;
    
    #endregion



    #region Unity lifecycle

    void Awake()
    {
        _ = this;
    }

    #endregion



    #region Public Methods

    public override void Activate()
    {
        base.Activate();
        
        StartCoroutine(StartConfeti());
    }

    #endregion



    #region Private Methods

    private IEnumerator StartConfeti()
    {
        this.transform.localScale = new Vector3(1, 1, 1) * _k_test * Camera.main.orthographicSize;

        for (int i = 0; i < 222; i++)
            Instantiate(_pref_Par, this.transform.position, this.transform.rotation, this.transform).material = _mat_Par[Random.Range(0, _mat_Par.Length)];

        float _t = 0;
        while (_t < 1)
        {
            _t += Time.deltaTime * 0.4f;
            yield return null;
        }
    }
    
    #endregion
}
