using UnityEngine;

public class StatModifier {

	private int mModifierValue;
	private float mSpan;
	private float mInitTime;

	public int Value
	{
		get
		{
			return mModifierValue;
		}
	}

	public float Span
	{
		get
		{
			return mSpan;
		}
	}

	public float InitTime
	{
		get
		{
			return mInitTime;
		}
	}

	/// <summary>
	/// Creates a new modifier for any given stat.
	/// </summary>
	/// <param name="value">Modifier value</param>
	/// <param name="span">Modifier lifespan</param>
	public StatModifier(int value, float span)
	{
		mModifierValue = value;
		mSpan = span;
		mInitTime = Time.time;
	}

	/// <summary>
	/// Overloaded constructor for modifiers with an undefined lifespan
	/// (e.g., gear modifiers)
	/// </summary>
	/// <param name="value">Modifier value</param>
	public StatModifier(int value) : this(value, 0.0f){}
}
