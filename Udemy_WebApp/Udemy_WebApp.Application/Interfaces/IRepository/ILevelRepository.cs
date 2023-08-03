﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy_WebApp.Domain.Models;

namespace Udemy_WebApp.Application.Interfaces.IRepository
{
    public interface ILevelRepository : IRepository<Level>
    {
        Task UpdateAsync(Level level);
    }
}
