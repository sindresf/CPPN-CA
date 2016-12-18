using System;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA.NEAT.Parts
{

	struct MutationChances //check these up against standard NEAT settings
	{
		public const float MutatWeightAmount   = 0.1f;
		public const double CreationMutationChance = 0.05; // <- completely out of my ass, like most of these

		private const double AddNewNode          = 0.03;
		private const double AddNewConnection    = 0.05;
		private const double ChangeNodeFunction  = 0.01;

		private const double MutateWeights       = 0.8;
		private const double MutateWeight        = 0.90;
		private const double RandomResetWeight   = 0.1;
		private const double DisableInheritedGene = 0.75;

		public static double GetMutationChance(MutationType type)
		{
			switch(type)
			{
			case MutationType.AddNode:
				return AddNewNode;
			case MutationType.AddConnection:
				return AddNewConnection;
			case MutationType.ChangeWeight:
				return MutatWeightAmount;
			case MutationType.ChangeFunction:
				return ChangeNodeFunction;
			default:
				return 0.0;
			}
		}
	}

	enum MutationType //by ordering this you can order the mutations in case of several for the same genome
	{// like adding a node and a connection before changing a weight might be nice, so that every weight is present.
		AddNode,
		AddConnection,
		ChangeWeight,
		ChangeFunction
	}

	class Mutator
	{
		public static NeatGenome Mutate(NeatGenome genome, IDCounters IDs)
		{
			NeatGenome newGenome = new NeatGenome(genome);
			foreach(MutationType type in Enum.GetValues(typeof(MutationType)))
				if(Neat.random.DoMutation(type))
					newGenome = MutateOfType(type, newGenome, IDs);

			return newGenome;
		}

		private static NeatGenome MutateOfType(MutationType type, NeatGenome genome, IDCounters IDs)
		{
			switch(type)
			{
			case MutationType.ChangeWeight:
				return ChangeWeight(genome);
			case MutationType.AddConnection:
				return AddConnection(genome, IDs);
			case MutationType.AddNode:
				return AddNode(genome, IDs);
			case MutationType.ChangeFunction: //and then maybe "mutateCurrentFunction type"
				return ChangeNodeFunction(genome, IDs);
			default:
				return genome;
			}
		}

		private static NeatGenome AddNode(NeatGenome genome, IDCounters IDs)
		{
			ConnectionGene connectionToSplitt = Neat.random.ConnectionGene(genome);

			NodeGene newNode = new HiddenNodeGene(IDs.NodeGeneID,
										genome.nodeGenes.Count,
										Neat.random.ActivationFunctionType());

			ConnectionGene firstHalfGene = new ConnectionGene(IDs.ConnectionGeneID,
															connectionToSplitt.fromNodeID,
															newNode.nodeID,
															true,
															1.0f);

			ConnectionGene secondHalfGene = new ConnectionGene(IDs.ConnectionGeneID,
															newNode.nodeID,
															connectionToSplitt.fromNodeID,
															true,
															(float)Neat.random.NextRangedDouble(0.0,CPPNParameters.InitialMaxConnectionWeight));

			connectionToSplitt.isEnabled = false;
			genome.connectionGenes.Add(firstHalfGene);
			genome.connectionGenes.Add(secondHalfGene);
			return genome;
		}

		private static NeatGenome AddConnection(NeatGenome genome, IDCounters IDs)
		{
			//NO RECCURENT BULLSHITT.
			//CHECK HIS PAPER for any good explanations for this
			NodeGene fromNode = Neat.random.NotOutputNodeGene(genome);
			NodeGene toNode = Neat.random.NotInputNodeGene(genome); // is it so simple I can just make a "get node from After fromNode" ?
			ConnectionGene conGene = new ConnectionGene(IDs.ConnectionGeneID,
														fromNode.nodeID,
														toNode.nodeID,
														true, //was this supposed to be weighted random for new ones?
														Neat.random.InitialConnectionWeight());
			genome.connectionGenes.Add(conGene);
			return genome;
		}

		private static NeatGenome ChangeWeight(NeatGenome genome)
		{
			ConnectionGene connGene = Neat.random.ConnectionGene(genome);
			float newWeight = (connGene.connectionWeight + Neat.random.NextFloat() * 2.0f * MutationChances.MutatWeightAmount
																			- MutationChances.MutatWeightAmount).ClampWeight();
			connGene.connectionWeight = newWeight;
			return genome;
		}

		private static NeatGenome ChangeNodeFunction(NeatGenome genome, IDCounters IDs)
		{
			var newType = Neat.random.ActivationFunctionType();
			int geneIndex = Neat.random.Next(Neat.parameters.CPPN.InputSize,
						Neat.parameters.CPPN.InputSize + Neat.parameters.CPPN.OutputSize);

			genome.nodeGenes[geneIndex] = ((InternalNodeGene)genome.nodeGenes[geneIndex]).ChangeFunction(newType, IDs.NodeGeneID);
			return genome;
		}

		public static NeatGenome Crossover(NEATIndividual indie1, NEATIndividual indie2)
		{
			NeatGenome genome1 = indie1.genome;
			NeatGenome genome2 = indie2.genome;
			if(indie1.Fitness.SameWithinReason(indie2.Fitness))
				return SameFitnessRandomCrossOver(genome1, genome2);

			bool parent1HasMostNodes = genome1.nodeGenes.Count > genome2.nodeGenes.Count; //not GENE IDs

			bool mostFitIsIndie1 = indie1.Fitness > indie2.Fitness;
			NeatGenome childGenome = new NeatGenome();
			if(parent1HasMostNodes)
			{
				childGenome.nodeGenes.Add(mostFitIsIndie1 ? genome1.nodeGenes[0] : genome2.nodeGenes[0]); //this type of stuff
			} else
			{

			}

			return childGenome;
		}

		private static NeatGenome SameFitnessRandomCrossOver(NeatGenome genome1, NeatGenome genome2)
		{
			NeatGenome childGenome = new NeatGenome();
			int shortestNodeGeneList = genome1.nodeGenes.Count < genome2.nodeGenes.Count ?
									   genome1.nodeGenes.Count :
									   genome2.nodeGenes.Count;

			for(int i = 0; i < shortestNodeGeneList; i++)
			{
				if(Neat.random.NextBoolean())
					childGenome.nodeGenes.Add(genome1.nodeGenes[i]);
				else
					childGenome.nodeGenes.Add(genome1.nodeGenes[i]);
			}
			//TODO what for the remaining genes?

			//must be heavily redone
			int shortestConnectionGeneList = genome1.connectionGenes.Count < genome2.connectionGenes.Count ?
									   genome1.connectionGenes.Count :
									   genome2.connectionGenes.Count;

			var longestGenome = NeatGenome.GetLonger(genome1, genome2);

			for(int i = 0; i < shortestNodeGeneList; i++)
			{
				if(Neat.random.NextBoolean())
					childGenome.connectionGenes.Add(genome1.connectionGenes[i]);
				else
					childGenome.connectionGenes.Add(genome1.connectionGenes[i]);
			}
			//TODO what for the remaining genes?
			// just add them all from the longestGenome

			return childGenome;
		}
	}
}