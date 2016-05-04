using Stampit.Entity;
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
    public class StampCodeStorage
    {
        #region Singleton
        private static StampCodeStorage _instance;
        private StampCodeStorage() { }
        public static StampCodeStorage GetStampCodeStorage()
        {
            if (_instance == null)
            {
                lock (typeof(StampCodeStorage))
                {
                    if (_instance == null)
                        _instance = new StampCodeStorage();
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
                            this.activeRedemtionCodes = new Dictionary<string, IDictionary<Product, int>>();
                        return this.activeRedemtionCodes;
                    }
                }
                return this.activeRedemtionCodes;
            }
        }
    }
}
