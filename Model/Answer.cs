using System;
using System.Collections.Generic;
using System.Text;

namespace TransportProblemApp.Model
{
	public class Answer
	{
		double[][] BasePlan { get; set; }
		double[][] OptimalPlan { get; set; }
		int FakeRow { get; set; }
		int FakeColumn { get; set; }
	}
}
