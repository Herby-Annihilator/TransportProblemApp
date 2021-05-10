using System;
using System.Collections.Generic;
using System.Text;

namespace TransportProblemApp.Model
{
	public class Answer
	{
		public double[][] BasePlan { get; set; }
		public double BasePlanPrice { get; set; }
		public double[][] OptimalPlan { get; set; }
		public double OptimalPlanPrice { get; set; }
		public int FakeRow { get; set; }
		public int FakeColumn { get; set; }
	}
}
