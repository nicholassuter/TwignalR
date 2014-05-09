using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public string GetConnection(T key)
        {
            if (_connections.ContainsKey(key))
            {
                return _connections[key];
            }

            return null;
        }

        public void Remove(string connectionId)
        {
            lock (_connections)
            {
                if (_connections.ContainsValue(connectionId))
                {
                    T key = _connections.FirstOrDefault(x => x.Value == connectionId).Key;
                    _connections.Remove(key);
                }
            }
        }
    }
}