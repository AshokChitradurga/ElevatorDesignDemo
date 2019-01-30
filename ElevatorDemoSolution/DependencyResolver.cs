﻿using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorDemoSolution
{
    public sealed class DependencyResolver : NinjectModule
    {
        private IKernel kernel = default(StandardKernel);
        private static readonly Lazy<DependencyResolver> _instance = new Lazy<DependencyResolver>(() => new DependencyResolver());

        private DependencyResolver()
        {
            CreateKernal();
        }
        public override void Load()
        {
            Bind<IElevatorBuilder>().To<PassangerElevator>();
            Bind<IElevatorAction>().To<ElevatorAction>();   
            Bind<IElevatorController>().To<ElevatorController>();
        }
        public static DependencyResolver Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        private IKernel CreateKernal()
        {
            kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }
        private void RegisterServices(IKernel kernel)
        {
            kernel.Load(this);
        }

        public IKernel KernelType
        {
            get { return kernel; }
        }

        public T GetDependency<T>()
        {
            return kernel.Get<T>();
        }
    }
}
