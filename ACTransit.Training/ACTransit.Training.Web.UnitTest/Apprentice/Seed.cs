using System;
using System.Collections.Generic;

namespace ACTransit.Training.Web.UnitTest.Apprentice
{
    public sealed class Seed : Dictionary<string, int>
    {
        private static readonly Seed instance = new Seed();
        static Seed() { }
        private Seed() { }
        public static Seed Item { get { return instance; } }
        public int Next(string key)
        {
            if (!ContainsKey(key))
                this[key] = 0;
            var result = this[key] + 1;
            this[key] = result;
            return result;
        }
        public Random Random = new Random();
    }
}
