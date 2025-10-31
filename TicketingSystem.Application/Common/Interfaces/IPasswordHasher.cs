﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystem.Application.Common.Interfaces
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string hash, string password);
    }

}
