using System;
using System.Collections.Generic;
using System.Text;

namespace VideoGameOrderSystem.Library
{
    public class VideoGameRepository
    {
        private readonly ICollection<Location> _data;

        public VideoGameRepository(ICollection<Location> data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }


    }
}
