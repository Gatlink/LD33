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
	private Monster _monster;
	private Collider2D _monsterCollider;
	private Animator _anim;

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
    	_monster = GameObject.FindObjectsOfType<Monster>()[0];
    	_monsterCollider = _monster.GetComponent<Collider2D>();
    	_anim = GetComponent<Animator>();
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
		Vector3 axis = Vector3.zero;		
		axis.x = Input.GetAxis("Horizontal");
		axis.y = Input.GetAxis("Vertical");

		var speed = axis.magnitude;
		_anim.SetFloat("Speed", speed);
		Move(axis);

		if (Input.GetAxis("Fire1") != 0f)
			Attack();
	}

	public override void Attack() 
	{
		var direction = (Cursor.position - _feet.position).normalized;
		if (IsFacingMonster() && _feet.GetComponent<Collider2D>().IsTouching(_monsterCollider))
		{
			_monster.GetKicked(direction * KickStrength);
		}
	}
	
	public override void Die()
	{
		GetComponent<SpriteRenderer>().color = Color.red;
	}

	private bool IsFacingMonster()
	{
		return (_monster.transform.position.x < transform.position.x && transform.localScale.x == -1)
				|| (_monster.transform.position.x > transform.position.x && transform.localScale.x == 1);
	}
}
