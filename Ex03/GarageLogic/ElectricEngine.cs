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

public class ElectricEngine : Engine
{
	public virtual float GetBatteryTimeLeft()
	{
        return m_CurrentEnergyAmount;
	}

	public virtual float GetMaxBatteryTime()
	{
        return m_MaxEnergyAmount;
	}

	public virtual void RechargeBattery(float i_HoursToAdd)
	{
        m_CurrentEnergyAmount += i_HoursToAdd;
        if(m_CurrentEnergyAmount > m_MaxEnergyAmount)
        {
            m_CurrentEnergyAmount = m_MaxEnergyAmount;
        }
	}

}
