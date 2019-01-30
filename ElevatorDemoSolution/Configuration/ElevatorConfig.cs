using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorDemoSolution
{
    public class ElevatorConfig : ConfigurationElement
    {
        [ConfigurationProperty("MaxFloor", DefaultValue = 10, IsRequired = true)]
        public int MaxFloor
        {
            get
            {
                return (int)this["MaxFloor"];
            }
            set
            {
                value = (int)this["MaxFloor"];
            }
        }
        [ConfigurationProperty("MinFloor", DefaultValue = -2, IsRequired = true)]
        public int MinFloor
        {
            get
            {
                return (int)this["MinFloor"];
            }
            set
            {
                value = (int)this["MinFloor"];
            }
        }
        [ConfigurationProperty("MaxElevators", DefaultValue = 3, IsRequired = true)]
        public int MaxElevators
        {
            get
            {
                return (int)this["MaxElevators"];
            }
            set
            {
                value = (int)this["MaxElevators"];
            }
        }
        [ConfigurationProperty("DefaultCurrentFloor")]
        public int DefaultCurrentFloor
        {
            get;
            set;
        }

        [ConfigurationProperty("StoppagePoints")]
        public List<int> StoppagePoints
        {
            get
            {
                return new List<int> { 10, 0, 6 };
            }
        }
    }
}
