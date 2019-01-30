using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorDemoSolution
{
    public class BuildingSettings : ConfigurationSection
    {
        [ConfigurationProperty("ElevatorSettings")]
        public ElevatorConfig ElevatorConfig
        {
            get
            {
                return (ElevatorConfig)this["ElevatorSettings"];
            }
            set
            {
                value = (ElevatorConfig)this["ElevatorSettings"];
            }
        }
    }
}
