﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace UserMicroservice.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User not found")
        {
        }

        public UserNotFoundException(string message) : base(message)
        {
        }

    }
}
