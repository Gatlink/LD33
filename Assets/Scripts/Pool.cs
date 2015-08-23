using UnityEngine;
using System.Collections.Generic;

public abstract class Poolable : MonoBehaviour 
{
	public abstract void Reset();
}

public class Pool<PooledType> : MonoBehaviour where PooledType : Poolable
{
	public PooledType PooledPrefab;
	public int InitialSize = 10;

	private Queue<PooledType> m_pool;
	private List<PooledType> m_inUse;

	public virtual void Awake() 
	{
		if(PooledPrefab == null) 
		{
			Debug.LogError("Pool has no prefab to build items from.");
			return;
		}

		m_pool = new Queue<PooledType>(InitialSize + 1);
		for(int i = 0; i < InitialSize; ++i)
		{
			PooledType item = CreateItem();
			if(item == null) { continue; }
			m_pool.Enqueue(item);
		}

		m_inUse = new List<PooledType>(InitialSize);
	}

	public PooledType CreateItem(bool activate = false) 
	{
		GameObject ball = Instantiate(PooledPrefab.gameObject);
		if(ball != null) 
		{
			ball.transform.SetParent(transform, false);
			ball.gameObject.SetActive(activate);
		}
		return ((ball != null) ? ball.GetComponent<PooledType>() : null);
	}

	public PooledType GetNext(bool activate = false) 
	{
		PooledType ball = ((m_pool.Count <= 0) ? CreateItem() : m_pool.Dequeue());
		m_inUse.Add(ball);
		ball.transform.SetParent(null, false);
		ball.gameObject.SetActive(activate);
		return ball;
	}

	public void Recycle(PooledType ball) 
	{
		if(ball == null) { return; }

		if(m_inUse.Remove(ball)) 
		{
			ball.Reset();
			ball.gameObject.SetActive(false);
			ball.transform.SetParent(transform, false);
			m_pool.Enqueue(ball);
		} 
		else 
		{
			Destroy(ball.gameObject);
		}
	}
}