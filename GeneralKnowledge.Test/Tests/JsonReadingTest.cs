using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;

namespace GeneralKnowledge.Test.App.Tests
{
    /// <summary>
    /// Basic data retrieval from JSON test
    /// </summary>
    public class JsonReadingTest : ITest
    {
        public string Name { get { return "JSON Reading Test"; } }
        
        public void Run()
        {
            var jsonData = Resources.SamplePoints;

            // TODO: 
            // Determine for each parameter stored in the variable below, the average value, lowest and highest number.
            // Example output
            // parameter   LOW AVG MAX
            // temperature   x   y   z
            // pH            x   y   z
            // Chloride      x   y   z
            // Phosphate     x   y   z
            // Nitrate       x   y   z

            PrintOverview(jsonData);
        }

        private void PrintOverview(byte[] data)
        {
            var grupedSamples = CastToSample(data);

            Console.WriteLine("{0,-15} {1,6}{2,6}{3,6}\n", "parameter", "LOW", "AVG", "MAX");
            foreach (var item in grupedSamples.GroupBy(g => g.Type))
            {
                var name = item.FirstOrDefault().Type;
                var avg = Math.Round(item.Sum(x => x.Value) / item.Count(), 2);
                var min = item.Min(x => x.Value);
                var max = item.Max(x => x.Value);
                Console.WriteLine("{0,-15} {1,6:N1}{2,6:N1}{3,6:N1}", name, min, avg, max);
            }
        }


        private List<Sample> CastToSample(byte[] data)
        {
            var results = new List<Sample>();
            string jsonStr = Encoding.UTF8.GetString(data);
            var input = JsonConvert.DeserializeObject<RootObject>(jsonStr).Samples;
            foreach (var items in input)
            {
                foreach (var item in items)
                {
                    if (Enum.TryParse(item.Key, out SampeTypes pareseResult))
                    {
                        results.Add(new Sample(pareseResult, double.Parse(item.Value, CultureInfo.InvariantCulture)));
                    }

                }
            }
            return results;
        }
    }

    public enum SampeTypes
    {
        temperature,
        pH,
        phosphate,
        chloride,
        nitrate
    }

    public class Sample
    {
        public SampeTypes Type { get; private set; }
        public double Value { get; private set; }

        public Sample(SampeTypes type, double value)
        {
            Type = type;
            Value = value;
        }
    }
    
    public class RootObject
    {
        public IList<IDictionary<string, string>> Samples { get; set; }
    }
    
}
