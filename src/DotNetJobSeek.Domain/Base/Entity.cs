using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetJobSeek.Domain
{
    /// Entity class 
    public abstract class Entity : IEntity
    {
        private object _key;

        public virtual object Key
        {
            get 
            {
                return this._key;
            }
            set 
            {
                if (value == null) 
                {
                    throw new ArgumentNullException("Key", "Key property should be null");
                }
                this._key = value;
            }
        }

    }
}