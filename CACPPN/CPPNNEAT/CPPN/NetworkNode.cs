using CPPNNEAT.NEAT;

namespace CPPNNEAT.CPPN
{
	class NetworkNode : ActivationFunction
	{
		public int nodeID { get; set; }
	}

	class SensorNode : NetworkNode
	{
		public static NodeType type = NodeType.Sensor;
	}

	class HiddenNode : NetworkNode
	{
		public static NodeType type = NodeType.Hidden;
	}

	class OutputNode : NetworkNode
	{
		public static NodeType type = NodeType.Output;
	}
}
