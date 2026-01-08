using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using Cache1.Models;
using System.Collections.Concurrent;
using Cache1.Extensions;

namespace Cache1.Services
{
    public class PrimeNumberService
    { 
        ConcurrentDictionary<string, PrimeBatch> _primeNumberCache = new ();

        public async Task<int> GetPrimesCountAsync(int start, int count)
        {
            return await NrPrimesInRangeAsync  (start, count); 

            //Exercise 1:
            //Implement the cache logic here
            PrimeBatch pResponse = null;
            var key = new CacheKey(start, count);

            //Check if Cache already contains the value
            if (false)              
            {
                //If not, calculate the value

                //Store the calculated value in the Cache

                //Exercise 2:
                //Persist the Cache to disk
            }

            return pResponse.NrPrimes;
        }

        private Task<int> NrPrimesInRangeAsync(int start, int count) => Task.Run(() => NrPrimesInRange(start, count));
        private int NrPrimesInRange(int start, int count) => 
            Enumerable.Range(start, count).Count(n =>
                Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0));


        public PrimeNumberService()
        {
            //Exercise 2:
            //Load the cache from disk
        }
    }
}
