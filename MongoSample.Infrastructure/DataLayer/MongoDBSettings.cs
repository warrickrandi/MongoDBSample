﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoSample.Infrastructure.DataLayer
{
    public class MongoDBSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string Database { get; set; } = null!;
    }
}
