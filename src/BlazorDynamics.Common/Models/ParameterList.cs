﻿using System.Collections;

namespace BlazorDynamics.Common.Models
{
    public class ParameterList : IEnumerable<KeyValuePair<string, object>>
    {
        readonly Dictionary<string, object> parameters = new Dictionary<string, object>();
        public ParameterList(Dictionary<string, object> parameters)
        {
            foreach (var item in parameters)
            {
                Add(item.Key, item.Value);
            }
        }

        public Dictionary<string, object> Entries
        {
            get { return parameters; }
        }

        public ParameterList() { }

        public ParameterList Add(string key, object value)
        {
            if (!parameters.TryAdd(key, value))
            {
                parameters[key] = value;
            }
            return this;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return parameters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void AddRange(ParameterList customParameters)
        {
            foreach (var item in customParameters)
            {
                parameters.Add(item.Key, item.Value);
            }
        }

        public object this[string key]
        {
            get
            {
                if (parameters.TryGetValue(key, out var value))
                {
                    return value;
                }
                throw new KeyNotFoundException($"The key '{key}' was not found in the parameters.");
            }
            set
            {
                if (!parameters.TryAdd(key, value))
                {
                    parameters[key] = value;
                }
            }
        }
    }
}
