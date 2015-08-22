using UnityEngine;
using System.Collections;

public class MainCharacter : Character
{
	public Transform Target;
	private static MainCharacter m_instance;
    public static MainCharacter Instance
    {
        get
        {

#if UNITY_EDITOR
            if(m_instance == null)
                m_instance = GameObject.FindObjectOfType<MainCharacter>();
#endif

            return m_instance;
        }
    }

    public override void Awake()
    {
        if(m_instance == null)
        {
            m_instance = this as MainCharacter;
        }
        else
        {
            Debug.Log("Cannot have two instances of " + typeof(MainCharacter).ToString() + " : " + m_instance);
            Destroy(this.gameObject);
        }

        base.Awake();
    }

    public static void Destroy()
    {
        Destroy(Instance.gameObject);
        m_instance = null;
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
	}

	public override void Attack() 
	{
		Debug.Log("PAF");
	}
	
	public override void Die()
	{
		GetComponent<SpriteRenderer>().color = Color.red;
	}
}
