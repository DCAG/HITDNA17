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
    private string m_Manufacturer;
    private float m_AirPressure;
    private float m_MaxAirPressure;

	public void Inflation(float i_AirPressure)
	{
        m_AirPressure += i_AirPressure;
        if(m_AirPressure > m_MaxAirPressure)
        {
            m_AirPressure = m_MaxAirPressure;
        }
	}

}
