using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoGameOrderSystem.Models
{
    public class Location
    {
        private int _locationId;
        public string Name { get; set; }

        public int LocationId
        {
            get => _locationId;

            set
            {
                _locationId = value;
            }
        }
    }
}
