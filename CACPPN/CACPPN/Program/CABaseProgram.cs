using System;
using System.Collections.Generic;
using System.Linq;
using CACPPN.CA.TypeEnums;
using CACPPN.CA.Interfaces;

namespace CACPPN.Program
{
    class CABaseProgram //Remember, the BASE CA is not evolutionary design, but ALL hardcoded
    {
        static void Main(string[] args)
        {
            CABaseProgram program = new CABaseProgram();
            program.Run();
        }

        public void Run()
        {
            Experiment experiment = new BasicExperiment();
            bool successFinish = false;
            for (int i = 0; i < experiment.hyperParams.generations; i++)
            {
                experiment.NextState();
                if (experiment.IsSuccessState())
                {
                    successFinish = true;
                    Console.WriteLine("done through success");
                    break;
                }
            }
            if (!successFinish)
                Console.WriteLine("all generations done.");
        }
    }
}
