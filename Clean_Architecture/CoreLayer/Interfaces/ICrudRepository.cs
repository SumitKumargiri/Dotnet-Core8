﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Interfaces
{
    public interface ICrudRepository
    {
        Task<IEnumerable<object>> FetchCrudDataAsync();
    }
}
