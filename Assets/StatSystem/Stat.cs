using System.Threading;
using System.Collections.Generic;
using UnityEngine;

//TODO Change this to ScriptableObject
public class Stat : MonoBehaviour {

	[SerializeField] private StatEnum mStatType;
	[SerializeField] private int mBaseValue;
	private List<StatModifier> mModifiers;

	public int Value
	{
		get
		{
			int modifiersTotalValue = 0;
			foreach(StatModifier modifier in mModifiers)
			{
				modifiersTotalValue += modifier.Value;
			}

			return mBaseValue + modifiersTotalValue;
		}
		set
		{
			mBaseValue = value;
		}
	}

	public void AddModifier(StatModifier modifier)
	{
		mModifiers.Add(modifier);
	}

	private void Start()
	{
		mModifiers = new List<StatModifier>();
	}

	private void Update()
	{
		if(mModifiers.Count > 0)
		{
			foreach (StatModifier modifier in mModifiers)
			{
				//Span == 0 <-> Infinite duration modifier (e.g. gear modifier)
				if (modifier.Span > 0.0f && Time.time >= modifier.InitTime + modifier.Span)
				{
					mModifiers.Remove(modifier);
				}
			}
		}
	}
}
