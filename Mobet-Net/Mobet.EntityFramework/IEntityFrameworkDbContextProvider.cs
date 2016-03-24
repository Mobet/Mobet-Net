﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.EntityFramework
{
    /// <summary>
    /// IDbContextProvider
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IEntityFrameworkDbContextProvider<out TDbContext>
        where TDbContext : DbContext
    {
        TDbContext DbContext { get; }
    }
}
