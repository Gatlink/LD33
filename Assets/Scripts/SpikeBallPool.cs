using UnityEngine;
using System.Collections;

public class SpikeBallPool : Pool<SpikeBall>
{
#region singleton
	private static SpikeBallPool _instance;
    public static SpikeBallPool Instance
    {
        get
        {

#if UNITY_EDITOR
            if(_instance == null)
                _instance = GameObject.FindObjectOfType<SpikeBallPool>();
#endif

            return _instance;
        }
    }

    public override void Awake()
    {
    	base.Awake();

        if(_instance == null)
        {
            _instance = this as SpikeBallPool;
        }
        else
        {
            Debug.Log("Cannot have two instances of " + typeof(SpikeBallPool).ToString() + " : " + _instance);
            Destroy(this.gameObject);
        }
    }
#endregion
};