﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Wheel
{
	private string m_Manufacturer
	{
		get;
		set;
	}

	private float m_AirPressure
	{
		get;
		set;
	}

	private float m_MaxAirPressure
	{
		get;
		private set;
	}

	public virtual float Inflation(float i_AirPressure)
	{
		throw new System.NotImplementedException();
	}

}

