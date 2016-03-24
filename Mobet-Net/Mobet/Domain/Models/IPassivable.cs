﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Domain.Models
{
    /// <summary>
    /// This interface is used to make an entity active/passive.
    /// </summary>
    public interface IPassivable
    {
        bool IsActive { get; set; }
    }
}
