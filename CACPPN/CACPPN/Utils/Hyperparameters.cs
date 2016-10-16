namespace CACPPN.Utils
{
    struct Hyperparameters
    {
        public int generations { get; set; }
        public int spaceSize { get; set; }
        public int neighbourhoodWidth { get; set; }
        public int states { get; set; }
        public double acceptableFitness { get; set; }

        public int timeStep { get; set; }
    }

    //TODO do I need a different hyperparameter thing for the evo of the CPPN?
    // maybe it has it?
}
