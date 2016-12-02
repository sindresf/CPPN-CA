﻿using CPPNNEAT.CA;
using CPPNNEAT.NEAT.EABase;
using CPPNNEAT.Utils;

namespace CPPNNEAT.EA.Base
{
	abstract class Individual
	{
		public readonly int individualID;
		public float Fitness { get; protected set; }

		public Individual(int individualID)
		{
			this.individualID = individualID;
		}

		public abstract void Initialize(IDCounters IDs);
		public abstract void Evaluate(IEvaluator CARunner, int speciesCount);

	}
}