﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public sealed record UpdateUserDetailsResponse
    {
        public bool IsUpdated { get; set; }
    }
}