#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Gui.Tools;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators
{
	public class BarCountV2 : Indicator
	{
		List<BarNumTwo> barTwo = new List<BarNumTwo>();
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description = @"Count the number of bars on the chart.";
				Name = "BarCountV2";
				Calculate = Calculate.OnPriceChange;
				IsOverlay = true;
				DisplayInDataBox = true;
				DrawOnPricePanel = true;
				DrawHorizontalGridLines = true;
				DrawVerticalGridLines = true;
				PaintPriceMarkers = true;
				ScaleJustification = NinjaTrader.Gui.Chart.ScaleJustification.Right;
				//Disable this property if your indicator requires custom values that cumulate with each new market data event. 
				//See Help Guide for additional information.
				IsSuspendedWhileInactive = true;
			}
			else if (State == State.Configure)
			{
			}
		}

		protected override void OnBarUpdate()
		{
			//Add your custom indicator logic here.
			if (CurrentBar < 2)
			{
				return;
			}

			for (int i = 0; i < CurrentBar; i++)
			{
				Draw.Text(this, "Barcount" + i, i.ToString(), i, High[i] + TickSize, Brushes.Green);
			}
		}
	}

	public class BarNumTwo
	{
		public DateTime Time { get; set; }
		public double Low { get; set; }

	}
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private BarCountV2[] cacheBarCountV2;
		public BarCountV2 BarCountV2()
		{
			return BarCountV2(Input);
		}

		public BarCountV2 BarCountV2(ISeries<double> input)
		{
			if (cacheBarCountV2 != null)
				for (int idx = 0; idx < cacheBarCountV2.Length; idx++)
					if (cacheBarCountV2[idx] != null && cacheBarCountV2[idx].EqualsInput(input))
						return cacheBarCountV2[idx];
			return CacheIndicator<BarCountV2>(new BarCountV2(), input, ref cacheBarCountV2);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.BarCountV2 BarCountV2()
		{
			return indicator.BarCountV2(Input);
		}

		public Indicators.BarCountV2 BarCountV2(ISeries<double> input)
		{
			return indicator.BarCountV2(input);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.BarCountV2 BarCountV2()
		{
			return indicator.BarCountV2(Input);
		}

		public Indicators.BarCountV2 BarCountV2(ISeries<double> input)
		{
			return indicator.BarCountV2(input);
		}
	}
}

#endregion