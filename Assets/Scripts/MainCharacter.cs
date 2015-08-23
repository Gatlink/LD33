using UnityEngine;
using System.Collections;

public class MainCharacter : Character
{
	[Space(5)]
	public Transform Cursor;

	[Space(5)]
	public float KickDistance = 1f;
	public float KickStrength = 5f;

	private Transform _feet;

#region singleton
	private static MainCharacter _instance;
    public static MainCharacter Instance
    {
        get
        {

	#if UNITY_EDITOR
            if(_instance == null)
                _instance = GameObject.FindObjectOfType<MainCharacter>();
	#endif

            return _instance;
        }
    }

    public virtual void Awake()
    {
        if(_instance == null)
        {
            _instance = this as MainCharacter;
        }
        else
        {
            Debug.Log("Cannot have two instances of " + typeof(MainCharacter).ToString() + " : " + _instance);
            Destroy(this.gameObject);
        }
    }
#endregion

    public override void Start()
    {
    	base.Start();
    	_feet = transform.GetChild(0);
    }

    public static void Destroy()
    {
        Destroy(Instance.gameObject);
        _instance = null;
    }

	public virtual void FixedUpdate ()
	{
		if(Dead)
			return;
			
		ProcessInput();

		var scale = transform.localScale;
		scale.x = Cursor.position.x < transform.position.x ? -1 : 1;
		transform.localScale = scale;
	}

	private void ProcessInput() 
	{
		if(Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
		{
			Vector3 axis = Vector3.zero;		
			axis.x = Input.GetAxis("Horizontal");
			axis.y = Input.GetAxis("Vertical");

			Move(axis);
		}

		if (Input.GetAxis("Fire1") != 0f)
			Attack();
	}

	public override void Attack() 
	{
		var direction = (Cursor.position - _feet.position).normalized;
		RaycastHit2D hit = Physics2D.Raycast(_feet.position, direction, 64f, 1 << LayerMask.NameToLayer("Monster"));
		if (hit.collider != null)
		{
			var monster = hit.collider.GetComponent<Monster>();
			monster.GetKicked(direction * KickStrength);
		}
	}
	
	public override void Die()
	{
		GetComponent<SpriteRenderer>().color = Color.red;
	}
}
