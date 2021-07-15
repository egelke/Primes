using System;
using System.Collections.Generic;

namespace PrimeCSharp.Sieves
{
    class PrimeSieveInvBoolB : ISieve
    {
        public string QuickName => "dbool";
        public string Name => "Inverted Bool Array, Direct";
        public bool IsBaseAlgorithm => true;

        public int SieveSize { get; }
        private readonly bool[] boolArray;

        public PrimeSieveInvBoolB(int sieveSize)
        {
            if (sieveSize < 3)
            {
                throw new ArgumentOutOfRangeException(nameof(sieveSize), sieveSize,
                    $"Sieve size must be a positive integer greater than 2.");
            }

            SieveSize = sieveSize;
            boolArray = new bool[(sieveSize + 1) / 2];
        }

        public int CountPrimes()
        {
            int count = 0;
            for (int i = 0; i < boolArray.Length; i++)
                if (boolArray[i] == false)
                    count++;
            return count;
        }

        public IEnumerable<int> GetFoundPrimes()
        {
            yield return 2;

            for (int num = 3; num <= SieveSize; num += 2)
            {
                if (boolArray[num >> 1] == false)
                {
                    yield return num;
                }
            }
        }

        public void Run()
        {
            int factor = 3;
            int q = (int)Math.Sqrt(SieveSize);

            while (factor <= q)
            {
                while (boolArray[factor >> 1]) factor += 2;

                int max = SieveSize >> 1;
                int increment = (factor * 2) >> 1;
                for (int num = (factor * factor) >> 1; num < max; num += increment)
                    boolArray[num] = true;

                factor += 2;
            }
        }
    }
}
