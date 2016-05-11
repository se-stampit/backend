﻿using Stampit.CommonType;
using Stampit.Entity;
using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic
{
    /// <summary>
    /// Singleton for holding all currently active stampcodes
    /// </summary>
    public class LocalStampCodeStorage : IStampCodeStorage
    {
        #region Singleton
        private static LocalStampCodeStorage _instance;
        private LocalStampCodeStorage() { }
        public static LocalStampCodeStorage GetStampCodeStorage()
        {
            if (_instance == null)
            {
                lock (typeof(LocalStampCodeStorage))
                {
                    if (_instance == null)
                        _instance = new LocalStampCodeStorage();
                    return _instance;
                }
            }
            return _instance;
        }
        #endregion

        private IDictionary<string, IDictionary<Product, int>> activeStampCodes;
        private IDictionary<string, Product> activeRedemtionCodes;

        private IDictionary<string, IDictionary<Product, int>> ActiveStampCodes
        {
            get
            {
                if (this.activeStampCodes == null)
                {
                    lock (this)
                    {
                        if (this.activeStampCodes == null)
                            this.activeStampCodes = new Dictionary<string, IDictionary<Product, int>>();
                        return this.activeStampCodes;
                    }
                }
                return this.activeStampCodes;
            }
        }

        private IDictionary<string, Product> ActiveRedemtionCodes
        {
            get
            {
                if(this.activeRedemtionCodes == null)
                {
                    lock(this)
                    {
                        if (this.activeRedemtionCodes == null)
                            this.activeRedemtionCodes = new Dictionary<string, Product>();
                        return this.activeRedemtionCodes;
                    }
                }
                return this.activeRedemtionCodes;
            }
        }

        public void AddStampcode(string stampcode, IDictionary<Product, int> products)
        {
            if(!this.ActiveStampCodes.ContainsKey(stampcode))
                this.ActiveStampCodes.Add(stampcode, products);
        }

        public void AddRedemtioncode(string code, Product product)
        {
            if (!this.ActiveRedemtionCodes.ContainsKey(code))
                this.ActiveRedemtionCodes.Add(code, product);   
        }

        public IDictionary<Product,int> UseStampCode(string code)
        {
            IDictionary<Product, int> value;
            lock(ActiveStampCodes)
            {
                if (ActiveStampCodes.TryGetValue(code, out value))
                {
                    ActiveStampCodes.Remove(code);
                    return value;
                }
            }
            throw new IllegalCodeException(code);
        }

        public Product UseRedemtionCode(string code)
        {
            Product value;
            lock(activeRedemtionCodes)
            {
                if (ActiveRedemtionCodes.TryGetValue(code, out value))
                {
                    ActiveRedemtionCodes.Remove(code);
                    return value;
                }
            }
            throw new IllegalCodeException(code);
        }

        public bool ExistsCode(string code)
        {
            return ActiveStampCodes.ContainsKey(code) 
                || ActiveRedemtionCodes.ContainsKey(code);
        }
    }
}
