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
	public class FractalsV2 : Indicator
	{
		private List<UpFractals> upFractals = new List<UpFractals>();
		private List<DownFractals> downFractals = new List<DownFractals>();
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description = @"Up and down fractals on the chart.";
				Name = "FractalsV2";
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
			if (CurrentBar < 5)
			{
				return;
			}
			if (High[2] > High[1] && High[2] > High[3])
			{
				DownFractals dfrac = new DownFractals();
				dfrac.Price = High[2];
				dfrac.Date = Time[2];
				downFractals.Add(dfrac);
				//Draw.ArrowDown(this, "Arrow_Down", false, Time[2], High[2] + TickSize, Brushes.Red);
			}
			if (Low[2] < Low[1] && Low[2] < Low[3])
			{
				UpFractals ufrac = new UpFractals();
				ufrac.Price = Low[2];
				ufrac.Date = Time[2];
				upFractals.Add(ufrac);
			}

			if (downFractals.Count > 0)
			{
				for (int i = 0; i < downFractals.Count; i++)
				{
					Draw.ArrowDown(this, "Arrow_Down" + i.ToString(), false, downFractals[i].Date,
						downFractals[i].Price + TickSize, Brushes.Red);

					//Print("Down Frac date is : " + downFractals[i].Date);
					//Print("");
				}
			}
			if (upFractals.Count > 0)
			{
				for (int j = 0; j < upFractals.Count; j++)
				{
					Draw.ArrowUp(this, "Arrow_Up" + j.ToString(), false, upFractals[j].Date,
						upFractals[j].Price - TickSize, Brushes.Green);
				}
			}



		}

		#region Properties
		[Browsable(false)]
		[XmlIgnore]
		public List<UpFractals> UpFractals
		{
			get { return upFractals; }
		}

		[Browsable(false)]
		[XmlIgnore]
		public List<DownFractals> DownFractals
		{
			get { return downFractals; }
		}
		#endregion
	}


	public class DownFractals
	{
		public DateTime Date { get; set; }
		public double Price { get; set; }
	}

	public class UpFractals
	{
		public DateTime Date { get; set; }
		public double Price { get; set; }
	}
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private FractalsV2[] cacheFractalsV2;
		public FractalsV2 FractalsV2()
		{
			return FractalsV2(Input);
		}

		public FractalsV2 FractalsV2(ISeries<double> input)
		{
			if (cacheFractalsV2 != null)
				for (int idx = 0; idx < cacheFractalsV2.Length; idx++)
					if (cacheFractalsV2[idx] != null && cacheFractalsV2[idx].EqualsInput(input))
						return cacheFractalsV2[idx];
			return CacheIndicator<FractalsV2>(new FractalsV2(), input, ref cacheFractalsV2);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.FractalsV2 FractalsV2()
		{
			return indicator.FractalsV2(Input);
		}

		public Indicators.FractalsV2 FractalsV2(ISeries<double> input)
		{
			return indicator.FractalsV2(input);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.FractalsV2 FractalsV2()
		{
			return indicator.FractalsV2(Input);
		}

		public Indicators.FractalsV2 FractalsV2(ISeries<double> input)
		{
			return indicator.FractalsV2(input);
		}
	}
}

#endregion
