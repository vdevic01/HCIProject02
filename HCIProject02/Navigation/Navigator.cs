using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Navigation
{
    public class Navigator
    {
        private static readonly IDictionary<ViewType, IList<Action>> _eventHandlers;
        private static readonly IDictionary<ViewType, IList<Action<object>>> _eventHandlersWithParameters;

        static Navigator()
        {
            _eventHandlers = new Dictionary<ViewType, IList<Action>>();
            _eventHandlersWithParameters = new Dictionary<ViewType, IList<Action<object>>>();
        }
        public static void RemoveHandler(ViewType view)
        {
            _eventHandlers.Remove(view);
            _eventHandlersWithParameters.Remove(view);
        }
        public static void RegisterHandler(ViewType viewType, Action handler)
        {
            if (!_eventHandlers.TryGetValue(viewType, out var handlers))
            {
                _eventHandlers[viewType] = new List<Action>();
                handlers = _eventHandlers[viewType];
            }
            handlers.Add(handler);
        }

        public static void FireEvent(ViewType viewType)
        {
            if (!_eventHandlers.ContainsKey(viewType)) return;

            foreach (var handler in _eventHandlers[viewType])
            {
                handler.Invoke();
            }
        }

        public static void RegisterHandler(ViewType viewType, Action<object> handler)
        {
            if (!_eventHandlersWithParameters.TryGetValue(viewType, out var handlers))
            {
                _eventHandlersWithParameters[viewType] = new List<Action<object>>();
                handlers = _eventHandlersWithParameters[viewType];
            }
            handlers.Add(handler);
        }

        public static void FireEvent(ViewType viewType, object parameter)
        {
            if (!_eventHandlersWithParameters.ContainsKey(viewType)) return;

            foreach (var handler in _eventHandlersWithParameters[viewType])
            {
                handler.Invoke(parameter);
            }
        }
        public static void Clear()
        {
            _eventHandlers.Clear();
            _eventHandlersWithParameters.Clear();
        }
    }
}
