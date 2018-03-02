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
        private string _id;

        public virtual string Id
        {
            get 
            {
                return this._id;
            }
            set 
            {
                if (value == null) 
                {
                    this._id = System.Guid.NewGuid().ToString();
                } else {
                    this._id = value;
                }
            }
        }
    }
}