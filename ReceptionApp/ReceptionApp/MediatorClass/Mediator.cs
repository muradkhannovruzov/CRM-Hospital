using System;using System.Collections.Generic;using System.Text;

namespace ReceptionApp.MediatorClass
{
    public class Mediator
    {        
        private readonly Dictionary<Type, List<Action<object>>> subscriptions = new Dictionary<Type, List<Action<object>>>();
        public void Subscribe<T>(Action<object> action)
        {
            var type = typeof(T);
            if (!subscriptions.ContainsKey(type))
            {
                subscriptions[type] = new List<Action<object>>();
            }
            subscriptions[type].Add(action);
        }

        public void Send<T>(T viewModel)
        {
            var type = typeof(T);
            if (subscriptions.ContainsKey(type))
            {
                foreach (var action in subscriptions[type])
                {
                    action.Invoke(viewModel);
                }
            }
        }
        
    }
}
