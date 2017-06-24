﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.ExceptionHandling
{
    public interface IExceptionHandler
    {
        void HandleException(Exception ex);
    }
}
