using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorDemoSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            var controller = DependencyResolver.Instance.GetDependency<IElevatorController>();
            controller.Run();

            Console.Read();
        }
    }
}
