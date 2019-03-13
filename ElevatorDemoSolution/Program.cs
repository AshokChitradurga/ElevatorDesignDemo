using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorDemoSolution
{
    class Program
    {
<<<<<<< HEAD
        // more chages add as part of this solution.
=======
        //  Main program changes.
>>>>>>> e2bc007f2a73a5af3b13719e1789afbc653c57e4
        static void Main(string[] args)
        {

             Singleton.Instance.Y();
             Singleton.Instance.Y();

            DependencyResolver.Instance.SessionT.Test();
            DependencyResolver.Instance.SessionT.Test();

            return;
            var controller = DependencyResolver.Instance.GetDependency<IElevatorController>();
            controller.Run();

            Console.Read();
        }
    }
}
