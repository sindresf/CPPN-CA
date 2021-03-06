﻿using System;
using System.Collections.Generic;
using CPPNNEATCA.CA.Base;
using CPPNNEATCA.CPPN;
using CPPNNEATCA.CPPN.Parts;

namespace CPPNNEATCA.CA.Experiments
{

	/* generated rule order
	    0,0,0
		1,0,0
		0,1,0
		1,1,0
		0,0,1
		1,0,1
		0,1,1
		1,1,1
	 */
	class LambdaExperiment : BaseNeatCA
	{

		//private static int[] results = new int[] {0,0,0,0,0,0,0,0}; // lambda = 1
		//private static int[] results = new int[] {0,1,0,0,1,1,0,0}; // lambda = 2
		//private static int[] results = new int[] {1,1,1,0,1,1,1,0}; // lambda = 3
		private static int[] results = new int[] {0,1,1,0,1,0,1,0}; // lambda = 4 RULE 30 
																	//private static int[] results = new int[] {0,0,1,1,1,1,1,0}; // lambda = 4 RULE 110 

		public static RuleSet ruleSet = new RuleSet(results);
		public LambdaExperiment() : base()
		{

			parameters = new CAParameters(NeighbourHoodSize: 3,
										  CellStateCount: 2,
										  CellWorldWidth: 15,
										  MaxGeneration: 40,
										  MaxFitness: 64); //8 rules to get right, squared => 64

			MakeStates(parameters.CellStateCount);
		}

		public override float RunEvaluation(Func<List<float>, int> TransitionFunction)
		{
			double Fitness = 0.0;

			foreach(Rule rule in ruleSet)
			{
				bool same = TransitionFunction(rule) == rule.Result;
				if(same) Fitness++;
				else Fitness -= 1.5;
			}

			return (float)Math.Pow(Fitness, 2);
		}
		public float RunNetworkFeedbackEvaluation(ICPPNetwork inetwork)
		{
			var network = (CPPNetwork)inetwork;
			double Fitness = 0.0;

			foreach(Rule rule in ruleSet)
			{
				network.ActivateOutputNodes(rule);
				var actives = network.outputNodes;
				var wantedState = rule.Result;
				foreach(var inode in actives.Values)
				{
					var node = (OutputNetworkNode)inode;
					if(node.representedState == wantedState)
						Fitness += Math.Pow(Math.Abs(0 - node.Activation), 2.0);
					else
						Fitness -= Math.Pow(Math.Abs(0 - node.Activation), 2.0);
				}


				/*bool same = network.GetNextState(rule) == wantedState;
				if(same) Fitness += 1;
				else Fitness -= 2;*/
				network.ResetNetwork();
			}
			return (float)Fitness;
		}
	}

	class RuleSet : List<Rule>
	{
		public RuleSet(int[] results) : base()
		{
			int cond1 = 1, cond2 = 1, cond3 = 1;
			for(int i = 0; i < results.Length; i++)
			{
				if(i % 1 == 0) cond1 = cond1 == 1 ? 0 : 1;
				if(i % 2 == 0) cond2 = cond2 == 1 ? 0 : 1;
				if(i % 4 == 0) cond3 = cond3 == 1 ? 0 : 1;
				Add(new Rule(1, 1, 1, results[i]));
			}
		}
	}

	class Rule : List<float>
	{
		public int Result { get; private set; }
		public Rule(float condition1, float condition2, float condition3, int result) : base()
		{
			Add(condition1);
			Add(condition2);
			Add(condition3);
			Result = result;
		}
	}
}
