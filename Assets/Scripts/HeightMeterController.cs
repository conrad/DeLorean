using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class HeightMeterController : MonoBehaviour 
{
	public Slider heightMeterSlider;
	public Image meterFill;

	FlightChecker flightChecker;
	Color initialColor;
	Color flightColor;



	void Start () 
	{
		flightChecker = FlightChecker.Instance;
		initialColor = new Color(meterFill.color.r, meterFill.color.g, meterFill.color.b, meterFill.color.a);
		flightColor = new Color(1f, .01f, .01f);
	}



	void Update () 
	{
		float normalizedMeterHeight = .3f / flightChecker.GetFlightHeight()  * (flightChecker.GetHeight() - flightChecker.GetGroundHeight());
		heightMeterSlider.value = normalizedMeterHeight;

		if (flightChecker.IsFlying()) {
			meterFill.color = flightColor;
		} else {
			meterFill.color = initialColor;
		}
	}
}
