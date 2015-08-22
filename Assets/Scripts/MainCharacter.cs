using UnityEngine;
using System.Collections;

public class MainCharacter : Character
{
	public Transform Target;
	public float KickDistance = 1f;
	public float KickStrength = 5f;

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

    public override void Awake()
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

        base.Awake();
    }

    public static void Destroy()
    {
        Destroy(Instance.gameObject);
        _instance = null;
    }

	public override void Update ()
	{
		if(Dead)
			return;
			
		ProcessInput();
		base.Update();

		var scale = transform.localScale;
		scale.x = Target.position.x < transform.position.x ? -1 : 1;
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
		var direction = (Target.position - transform.position).normalized;
		RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 64f, 1 << LayerMask.NameToLayer("Monster"));
		if (hit.collider != null)
		{
			Debug.Log("PAF");
			var monster = hit.collider.GetComponent<Monster>();
			monster.GetKicked(direction, KickStrength);
		}
	}
	
	public override void Die()
	{
		GetComponent<SpriteRenderer>().color = Color.red;
	}
}
