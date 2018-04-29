﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Storeable
{
    public class MemoryStorage : IStorage
    {
        private IDictionary<string, object> mValues;

        public MemoryStorage(IDictionary<string, object> pValues)
        {
            mValues = pValues;
        }

        public object Get(string key)
        {
            return mValues[key];
        }

        public virtual string GetString(string key)
        {
            return Get(key)?.ToString();
        }

        public virtual void SetString(string key, string value)
        {
            Set(key, value);
        }

        public virtual int GetInt(string key)
        {
            return Convert.ToInt32(GetString(key));
        }

        public virtual void SetInt(string key, int value)
        {
            Set(key, value);
        }

        public void Reload()
        {
            throw new NotSupportedException();
        }

        public void Remove(string key)
        {
            mValues.Remove(key);
        }

        public void Save()
        {
            throw new NotSupportedException();
        }

        public void Set<K>(string key, K value)
        {
            mValues.Remove(key);
            mValues.Add(key, value);
        }
    }
}
