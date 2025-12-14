using System;

namespace ComputerProject
{
    public class CPU
    {
        public double Frequency { get; set; }    
        public int Cores { get; set; }           

        public CPU(double frequency, int cores)
        {
            Frequency = frequency;
            Cores = cores;
        }

        public override string ToString()
        {
            return $"Процессор: {Cores} ядер, {Frequency} GHz";
        }
    }
}