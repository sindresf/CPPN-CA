using System;
using System.Collections.Generic;
using CPPNNEATCA.NEAT.Base;
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

		public static double GetChanceFor(MutationType type)
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
		public static NeatGenome Mutate(NeatGenome genome, Population population)
		{
			NeatGenome newGenome = new NeatGenome(genome);
			foreach(MutationType type in Enum.GetValues(typeof(MutationType)))
				if(Neat.random.DoMutation(type))
				{
					newGenome = MutateOfType(type, newGenome, population);
					newGenome.hasMutated = true;
				}

			return newGenome;
		}

		private static NeatGenome MutateOfType(MutationType type, NeatGenome genome, Population population)
		{
			switch(type)
			{
			case MutationType.ChangeWeight:
				return ChangeWeight(genome);
			case MutationType.AddConnection:
				return AddConnection(genome, population);
			case MutationType.AddNode:
				return AddNode(genome, population);
			case MutationType.ChangeFunction: //and then maybe "mutateCurrentFunction type"
				return ChangeNodeFunction(genome);
			default:
				return genome;
			}
		}

		private static NeatGenome AddNode(NeatGenome genome, Population population)
		{
			var connectionToSplitt = Neat.random.ConnectionGene(genome);
			int fromGeneID = -1;
			int newNodeID = -1;
			int toGeneID = -1;
			if(population.SplittConnectionGeneIDsThisGeneration.Contains(connectionToSplitt.geneID))
			{
				var IDs = population.newConnectionGenesThisGenerationFromConnectionSplit[connectionToSplitt.geneID];
				fromGeneID = IDs.Item1;
				newNodeID = IDs.Item2;
				toGeneID = IDs.Item3;
			} else
			{
				population.SplittConnectionGeneIDsThisGeneration.Add(connectionToSplitt.geneID);
				fromGeneID = population.IDs.ConnectionGeneID;
				newNodeID = population.IDs.NodeGeneID;
				toGeneID = population.IDs.ConnectionGeneID;
				population.newConnectionGenesThisGenerationFromConnectionSplit.Add(connectionToSplitt.geneID, Tuple.Create(fromGeneID, newNodeID, toGeneID));
			}

			connectionToSplitt.isEnabled = false;
			var newNode = new HiddenNodeGene(newNodeID,
										genome.nodeGenes.Count,
										Neat.random.ActivationFunctionType());

			var firstHalfGene = new ConnectionGene(fromGeneID,
															connectionToSplitt.fromNodeID,
															newNode.nodeID,
															true,
															1.0f);

			var secondHalfGene = new ConnectionGene(toGeneID,
															newNode.nodeID,
															connectionToSplitt.toNodeID,
															true,
															(float)Neat.random.NextRangedDouble(0.0,CPPNParameters.InitialMaxConnectionWeight));

			genome.nodeGenes.Add(newNode);
			genome.connectionGenes.Add(firstHalfGene);
			genome.connectionGenes.Add(secondHalfGene);
			return genome;
		}

		private static NeatGenome AddConnection(NeatGenome genome, Population population)
		{
			NodeGene toNode = Neat.random.NotInputNodeGene(genome);
			if(toNode.nodeID <= 2) throw new Exception("was in addConnection; nodeGene:" + toNode.geneID + " nodeID:" + toNode.nodeID);



			var connGenes = genome.connectionGenes;
			NodeGene fromNode = Neat.random.NotOutputNodeGene(genome);

			bool baseCase = fromNode.type == NodeType.Sensor && toNode.type == NodeType.Output; //already exist all of these
			baseCase |= connGenes.Contains(fromNode.nodeID, toNode.nodeID); //existing "internal" connection

			if(baseCase)
				return genome;

			int allottedTries = genome.nodeGenes.Count-Neat.parameters.CPPN.InputSize - 1; //allowed to try every other node that's not input or itself.
																						   //making a minimal linked-list graph for checking
			var graph = new Dictionary<int,List<int>>();

			foreach(var node in genome.nodeGenes)
				graph.Add(node.nodeID, new List<int>());
			foreach(var connection in genome.connectionGenes)
				graph[connection.fromNodeID].Add(connection.toNodeID);

			while(allottedTries > 0)
			{
				if(graph.CanReachGoalFromRoot(toNode.nodeID, fromNode.nodeID))
				{
					allottedTries--;
					toNode = Neat.random.NotInputNodeGene(genome);

					if(toNode.type == NodeType.Output)
						if(fromNode.type == NodeType.Sensor)
							return genome;
						else if(connGenes.Contains(fromNode.nodeID, toNode.nodeID))
							continue;
						else
							break;
				} else
				{
					break; //the connection is none-cyclic
				}
			}

			var conGene = new ConnectionGene(population.IDs.ConnectionGeneID,
														fromNode.nodeID,
														toNode.nodeID,
														true, //was this supposed to be weighted random for new ones?
														Neat.random.InitialConnectionWeight());
			genome.connectionGenes.Add(conGene);
			return genome;
		}

		private static NeatGenome ChangeWeight(NeatGenome genome)
		{
			var oldConnGene = Neat.random.ConnectionGene(genome);
			float newWeight = (oldConnGene.connectionWeight + Neat.random.NextFloat() * 2.0f * MutationChances.MutatWeightAmount
																			- MutationChances.MutatWeightAmount).ClampWeight();
			oldConnGene.connectionWeight = newWeight;

			var newConnGene = new ConnectionGene(oldConnGene);
			genome.connectionGenes.Remove(oldConnGene);
			genome.connectionGenes.Add(newConnGene);
			return genome;
		}

		private static NeatGenome ChangeNodeFunction(NeatGenome genome)
		{
			if(genome.HasInternalNodes())
			{
				var newType = Neat.random.ActivationFunctionType();
				var nodeGene = Neat.random.InternalNodeGene(genome);
				nodeGene = ((InternalNodeGene)nodeGene).ChangeFunction(newType, nodeGene.geneID);
			}
			return genome;
		}

		public static NeatGenome Crossover(NEATIndividual indie1, NEATIndividual indie2)
		{
			var genome1 = indie1.genome;
			var genome2 = indie2.genome;

			var mostFitGenome = indie1.Fitness > indie2.Fitness ? indie1.genome : indie2.genome; //doesn't matter if they're equal

			var childGenome = new NeatGenome();

			int geneIndex = 0;
			var InvolvedNodes = new List<int>();
			var nodes1 = genome1.nodeGenes;
			var conns1 = genome1.connectionGenes;

			var nodes2 = genome2.nodeGenes;
			var conns2 = genome2.connectionGenes;

			bool moreInBoth = geneIndex < conns1.Count && geneIndex < conns2.Count;
			bool equalFitness = indie1.Fitness.SameWithinReason(indie2.Fitness);
			while(moreInBoth)
			{
				HandleMatchingAndDisjointConnectionGenes(childGenome, mostFitGenome, conns1, conns2, equalFitness, InvolvedNodes, geneIndex);
				geneIndex++;
				moreInBoth = geneIndex < conns1.Count && geneIndex < conns2.Count;
			}

			AddExcessGenes(childGenome, conns1, conns2, InvolvedNodes, geneIndex);

			childGenome.nodeGenes.AddRange(GetInvolvedNodesFromConnections(InvolvedNodes, nodes1, nodes2));
			return childGenome;
		}
		private static List<int> HandleMatchingAndDisjointConnectionGenes(NeatGenome childGenome, NeatGenome mostFitGenome, ConnectionGeneSequence conns1, ConnectionGeneSequence conns2, bool equalFitness, List<int> InvolvedNodes, int geneIndex)
		{
			ConnectionGene gene1,gene2;
			gene1 = conns1[geneIndex];
			gene2 = conns2[geneIndex];
			bool equalID = gene1.geneID == gene2.geneID;
			if(equalID)
			{
				var gene = Neat.random.NextBoolean() ? gene1 : gene2;
				InvolvedNodes = AddInvolvedConnNodes(InvolvedNodes, gene);
				childGenome.connectionGenes.Add(gene);
			} else
			{
				if(equalFitness)
				{
					var gene = Neat.random.NextBoolean() ? gene1 : gene2;
					InvolvedNodes = AddInvolvedConnNodes(InvolvedNodes, gene);
					childGenome.connectionGenes.Add(gene);
				} else
				{
					var gene = mostFitGenome.connectionGenes[geneIndex];
					InvolvedNodes = AddInvolvedConnNodes(InvolvedNodes, gene);
					childGenome.connectionGenes.Add(gene);
				}
			}
			InvolvedNodes.Sort();
			return InvolvedNodes;
		}
		private static List<NodeGene> GetInvolvedNodesFromConnections(List<int> InvolvedNodes, NodeGeneSequence nodes1, NodeGeneSequence nodes2)
		{
			var childNodes = new List<NodeGene>();
			bool in1 = false;
			bool in2 = false;
			bool inBoth = in1 && in2;
			foreach(var nodeID in InvolvedNodes)
			{
				in1 = nodes1.Contains(nodeID);
				in2 = nodes2.Contains(nodeID);
				inBoth = in1 && in2;
				if(inBoth)
					childNodes.Add(Neat.random.NextBoolean() ? nodes2.Get(nodeID) : nodes2.Get(nodeID));
				else
				{
					if(in1)
						childNodes.Add(nodes1.Get(nodeID));
					else if(in2)
						childNodes.Add(nodes2.Get(nodeID));
					else
						throw new Exception("an InvolvedNode was in neither parent nodeGeneSequence!");
				}
			}
			return childNodes;
		}
		private static List<int> AddExcessGenes(NeatGenome childGenome, ConnectionGeneSequence conns1, ConnectionGeneSequence conns2, List<int> InvolvedNodes, int geneIndex)
		{

			if(geneIndex == conns1.Count)
			{
				HandleExcessGenes(conns2, InvolvedNodes, geneIndex, childGenome);
			} else if(geneIndex == conns2.Count)
			{
				HandleExcessGenes(conns1, InvolvedNodes, geneIndex, childGenome);
			} else
			{
				throw new Exception("genIndex did not catch up to either parent genome!");
			}
			return InvolvedNodes;
		}
		private static void HandleExcessGenes(ConnectionGeneSequence connsLong, List<int> InvolvedNodes, int geneIndex, NeatGenome childGenome)
		{
			for(int i = geneIndex; i < connsLong.Count; i++)
			{
				InvolvedNodes = AddInvolvedConnNodes(InvolvedNodes, connsLong[i]);
				childGenome.connectionGenes.Add(connsLong[i]);
			}
		}
		private static List<int> AddInvolvedConnNodes(List<int> involvedNodes, ConnectionGene connGene)
		{
			AddInvolvedNode(involvedNodes, connGene.fromNodeID);
			AddInvolvedNode(involvedNodes, connGene.toNodeID);
			return involvedNodes;
		}
		private static List<int> AddInvolvedNode(List<int> involvedNodes, int nodeID)
		{
			if(!involvedNodes.Contains(nodeID))
				involvedNodes.Add(nodeID);
			return involvedNodes;
		}
	}
}