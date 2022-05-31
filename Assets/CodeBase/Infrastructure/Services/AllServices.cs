using CodeBase.Infrastructure.Factory;
using System;

namespace CodeBase.Infrastructure.Services
{
    public class AllServices
    {
        private static AllServices _instance;
        public static AllServices Container => _instance ?? (_instance =  new AllServices());

        internal void RegisterSingle<TService>(TService implementation) where TService : IService
        {
            Implementation<TService>.ServiceInstance = implementation;
        }

        internal TService Single<TService>() where TService : IService
        {
            return Implementation<TService>.ServiceInstance;
        }

        public static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}
