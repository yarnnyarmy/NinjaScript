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
	public class TrendLinesFromFractals : Indicator
	{
		//private List<exposedVariable>;
		private int upStart = 0;
		private int upNext = 0;
		List<UpTrends> up = new List<UpTrends>();
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description = @"Draw trendlines from the indicator Fractals.";
				Name = "TrendLinesFromFractals";
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
			else if (State == State.DataLoaded)
			{

				//AddChartIndicator(Fractals());
			}
		}

		protected override void OnBarUpdate()
		{
			//Add your custom indicator logic here.
			if (CurrentBar < 5)
			{
				return;
			}
			FractalsV2().Update();
			var fracUp = FractalsV2().UpFractals;
			if (fracUp.Count > 1)
			{
				if (!fracUp[fracUp.Count - 1].Date.Equals(fracUp[upNext].Date))
				{
					upNext = fracUp.Count - 1;
					upStart = 0;
				}


				//Print("Start bar is : " + upBarStart);
				//Print("Next bar is : " + upBarNext);

				Print("Start while Statment");
				while (upStart < upNext)
				{
					int upBarStart = CurrentBar - Bars.GetBar(fracUp[upStart].Date);
					int upBarNext = CurrentBar - Bars.GetBar(fracUp[upNext].Date);
					double lowBarStart = Low[upBarStart];
					double lowBarNext = Low[upBarNext];
					//for (int i = upBarStart; i < upBarNext; i++)
					//               {

					Print("Start for loop");
					for (int i = upBarNext + 1; i < upBarStart; i++)
					{

						if (lowBarStart > lowBarNext)
						{
							upStart++;
							break;

						}
						int barI = CurrentBar - Bars.GetBar(Time[i]);
						double lowBarI = Low[barI];
						//Print("The start bar is : " + upBarStart);
						//Print("The low of start bar is : " + Low[upBarStart]);
						//Print("The next bar is : " + upBarNext);
						//Print("The low of next bar is : " + Low[upBarNext]);
						//Print("Bar I is : " + barI);
						//Print("Low of bar I is : " + lowBarI);
						//Print("");

						double upSlope1 = (Low[upBarStart] - Low[upBarNext]) /
										  (upBarStart - upBarNext);

						double upSlope2 = (Low[upBarStart] - Low[barI]) /
						(upBarStart - barI);
						if (upSlope1 < upSlope2)
						{
							upStart++;
							Print("Slope is wrong");
							break;

						}

						if ((upSlope1 >= upSlope2) && (i + 1 == upBarStart))
						{
							Print("Slope is right");
							UpTrends uptrends = new UpTrends();
							uptrends.StartPrice = Low[upBarStart];
							uptrends.StartTime = fracUp[upStart].Date;
							uptrends.EndPrice = Low[upBarNext];
							uptrends.EndTime = fracUp[upNext].Date;
							Print(uptrends.StartPrice);
							Print(uptrends.StartTime);
							Print(uptrends.EndPrice);
							Print(uptrends.EndTime);
							Draw.Line(this, "TrendUpLine" + i, false, uptrends.StartTime, uptrends.StartPrice, uptrends.EndTime, uptrends.EndPrice, Brushes.Aqua, DashStyleHelper.Dot, 2);
							up.Add(uptrends);
							upStart++;
						}
					}

					//upStart++;

				}

				int count = 0;
				foreach (var trend in up)
				{
					Draw.Line(this, "TrendUpLine" + count, false, trend.StartTime, trend.StartPrice, trend.EndTime, trend.EndPrice, Brushes.Aqua, DashStyleHelper.Dot, 2);
					count++;
				}

			}


			//Fractals(Input);
		}

		#region Properties
		//public UpFractals Fracs
		//{
		//	get
		//	{
		//		//call OnBarUpdate before returning tripleValue
		//		Update();
		//		return fracs;
		//	}
		//}
		#endregion
	}

	public class UpTrends
	{
		public DateTime StartTime { get; set; }
		public double StartPrice { get; set; }
		public DateTime EndTime { get; set; }
		public double EndPrice { get; set; }
		public double TrendSlope { get; set; }
	}
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private TrendLinesFromFractals[] cacheTrendLinesFromFractals;
		public TrendLinesFromFractals TrendLinesFromFractals()
		{
			return TrendLinesFromFractals(Input);
		}

		public TrendLinesFromFractals TrendLinesFromFractals(ISeries<double> input)
		{
			if (cacheTrendLinesFromFractals != null)
				for (int idx = 0; idx < cacheTrendLinesFromFractals.Length; idx++)
					if (cacheTrendLinesFromFractals[idx] != null && cacheTrendLinesFromFractals[idx].EqualsInput(input))
						return cacheTrendLinesFromFractals[idx];
			return CacheIndicator<TrendLinesFromFractals>(new TrendLinesFromFractals(), input, ref cacheTrendLinesFromFractals);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.TrendLinesFromFractals TrendLinesFromFractals()
		{
			return indicator.TrendLinesFromFractals(Input);
		}

		public Indicators.TrendLinesFromFractals TrendLinesFromFractals(ISeries<double> input)
		{
			return indicator.TrendLinesFromFractals(input);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.TrendLinesFromFractals TrendLinesFromFractals()
		{
			return indicator.TrendLinesFromFractals(Input);
		}

		public Indicators.TrendLinesFromFractals TrendLinesFromFractals(ISeries<double> input)
		{
			return indicator.TrendLinesFromFractals(input);
		}
	}
}

#endregion
