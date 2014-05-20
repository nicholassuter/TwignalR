using System.Collections.Generic;
using System.Linq;

namespace TwitterWithSignalR.Models
{
    public class ConnectionMapping<T>
    {
        private readonly Dictionary<T, string> _connections =
            new Dictionary<T, string>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(T key, string connectionId)
        {
            lock (_connections)
            {
                if (!_connections.ContainsKey(key))
                {
                    _connections.Add(key, connectionId);
                }
            }
        }

        public T Remove(string connectionId)
        {
            var key = GetConnectionFromValue(connectionId);

            if (!EqualityComparer<T>.Default.Equals(key, default(T)))
            {
                lock (_connections)
                {
                    _connections.Remove(key);
                }
            }

            return key;
        }

        public string GetConnectionFromKey(T key)
        {
            if (_connections.ContainsKey(key))
            {
                return _connections[key];
            }

            return null;
        }

        public T GetConnectionFromValue(string value)
        {
            if (_connections.ContainsValue(value))
            {
                return _connections.FirstOrDefault(x => x.Value == value).Key;
            }
            return default(T);
        }

        
    }
}