﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigQuery.Linq
{
    public static partial class BqFunc
    {

        [FunctionName("LENGTH")]
        public static long Length(string str)
        {
            throw Invalid();
        }
    }
}