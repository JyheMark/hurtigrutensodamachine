using System;
using System.Collections.Generic;
using System.Text;

namespace SodaMachine
{
    class Soda
    {
        public string Id { get; private set; }

        public string Name 
        {
            get { return Name; }
            set
            {
                Name = value;
                Id = value.ToUpper();
            }
        }

        public Soda(string sodaName)
        {
            this.Name = sodaName;
        }
    }
}
