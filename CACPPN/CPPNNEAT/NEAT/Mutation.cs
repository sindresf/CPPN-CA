using System;
using CPPNNEAT.Utils;

namespace CPPNNEAT.NEAT
{
	class Mutation
	{

		public static Genome Mutate(Genome genome, IDCounters IDs)
		{
			double doIt;
			Random rand = new Random();
			Genome newGenome = null;
			foreach(MutationType type in Enum.GetValues(typeof(MutationType)))
			{
				doIt = rand.NextDouble();
				if(doIt <= MutationChances.GetMutationChance(type))
					genome = MutateOfType(type, genome, IDs, rand);
			}
			newGenome = genome; //TODO a "copy genome type of thing here
			return newGenome;
		}

		private static Genome MutateOfType(MutationType type, Genome genome, IDCounters IDs, Random rand)
		{
			switch(type)
			{
			case MutationType.ChangeWeight:
				return ChangeWeight(genome, rand);
			case MutationType.AddConnection:
				return AddConnection(genome, IDs, rand);
			case MutationType.AddNode:
				return AddNode(genome, IDs, rand);
			case MutationType.ChangeFunction: //and then maybe "mutateCurrentFunction type"
				return ChangeNodeFunction(genome, IDs, rand);
			default:
				return genome;
			}
		}

		private static Genome AddNode(Genome genome, IDCounters IDs, Random rand)
		{
			ConnectionGene connectionToSplitt = rand.ConnectionGene(genome);

			NodeGene newNode = new NodeGene(IDs.NodeGeneID,
										genome.nodeGenes.Count,
										NodeType.Hidden,
										rand.ActivationFunctionType());

			ConnectionGene firstHalfGene = new ConnectionGene(IDs.ConnectionGeneID,
															connectionToSplitt.fromNodeID,
															newNode.nodeID,
															true,
															1.0f);

			ConnectionGene secondHalfGene = new ConnectionGene(IDs.ConnectionGeneID,
															newNode.nodeID,
															connectionToSplitt.fromNodeID,
															true,
															(float)rand.NextDouble()*CPPNetworkParameters.InitialMaxConnectionWeight);

			connectionToSplitt.isEnabled = false; //writeline this to see that it changed
			genome.connectionGenes.Add(firstHalfGene);
			genome.connectionGenes.Add(secondHalfGene);
			return genome;
		}

		private static Genome AddConnection(Genome genome, IDCounters IDs, Random rand)
		{
			//NO RECCURENT BULLSHITT.
			//CHECK HIS PAPER for any good explanations for this
			NodeGene fromNode = rand.NotOutputNodeGene(genome);
			NodeGene toNode = rand.NotInputNodeGene(genome); // is it so simple I can just make a "get node from After fromNode" ?
			ConnectionGene conGene = new ConnectionGene(IDs.ConnectionGeneID,
														fromNode.nodeID,
														toNode.nodeID,
														true, //was this supposed to be weighted random for new ones?
														GetRandom.InitialConnectionWeight(rand));
			genome.connectionGenes.Add(conGene);
			return genome;
		}

		private static Genome ChangeWeight(Genome genome, Random rand)
		{
			//do a writeLine here to see if it changes in the genome to be sure (it should, is all reference)
			rand.ConnectionGene(genome).connectionWeight += (float)rand.NextDouble() * 2.0f * MutationChances.MutatWeightAmount
																			- MutationChances.MutatWeightAmount;
			return genome;
		}

		private static Genome ChangeNodeFunction(Genome genome, IDCounters IDs, Random random) //needs to impact the species placement, cus a sinus function contra a gaussian in the same spot makes a hell of a difference!
		{
			//gotta be a new node with the same nodeID but new nodeGeneID
			random.NotInputNodeGene(genome);
			//genome.nodeGenes[nodeIndex].nodeInputFunction = new CPPN.ActivationFunction(); //"get random function HERE
			return genome;
		}

		public static Genome Crossover(Individual indie1, Individual indie2)
		{
			Genome genome1 = indie1.genome;
			Genome genome2 = indie2.genome;



			return null;
		}
	}

	enum MutationType //by ordering this you can order the mutations in case of several for the same genome
	{// like adding a node and a connection before changing a weight might be nice, so that every weight is present.
		AddNode,
		AddConnection,
		ChangeWeight,
		ChangeFunction
	}
}