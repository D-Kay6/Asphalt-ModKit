﻿using Asphalt.Service;
using Asphalt.Util;
using System;
using System.Collections.Generic;
using System.IO;
using Eco.Shared.Utils;

namespace Asphalt.Storeable.Json
{
    public class JsonFileStorageCollection : IStorageCollection
    {
        private string dir;

        private IDictionary<string, IStorage> mStorageCache = new Dictionary<string, IStorage>();
        private IStorage mDefaultStorage;
        private KeyDefaultValue[] defaultValues;

        public JsonFileStorageCollection(string dir, KeyDefaultValue[] defaultValues = null)
        {
            this.dir = dir;
            this.defaultValues = defaultValues;

            //create DefaultFile
            if (mDefaultStorage == null && defaultValues != null)
                mDefaultStorage = new JsonFileStorage(GetFilePath("_default"), defaultValues.ToDictionaryNonNullKeys(k => k.Key, k => (object)k.DefaultValue), true);
        }

        public virtual IStorage GetDefaultStorage()
        {
            return mDefaultStorage;
        }

        public virtual IStorage GetStorage(string pStorageName)
        {
            if (!mStorageCache.ContainsKey(pStorageName))
            {
                JsonFileStorage file = new JsonFileStorage(GetFilePath(pStorageName), GetDefaultStorage()?.GetContent());
                mStorageCache.Add(pStorageName, file);
                return file;
            }

            return mStorageCache[pStorageName];
        }

        public virtual void Reload()
        {
            mStorageCache.Clear();
        }

        public string GetFilePath(string fileName)
        {
            return Path.Combine(dir, fileName+".json");
        }
    }
}
