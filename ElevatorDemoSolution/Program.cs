﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorDemoSolution
{
    class Program
    {
        // more chages add as part of this solution.
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
