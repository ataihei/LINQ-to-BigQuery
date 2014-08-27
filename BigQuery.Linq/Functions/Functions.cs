﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BigQuery.Linq.Functions
{
    // expr : object
    // numeric_expr : long, double
    // timestamp: DateTime


    internal class FunctionNameAttribute : Attribute
    {
        public string Name { get; private set; }
        public Type SpecifiedFormatterType { get; set; }

        public FunctionNameAttribute(string name)
        {
            this.Name = name;
        }
    }

    internal interface ISpeficiedFormatter
    {
        string Format(MethodCallExpression node);
    }

    /// <summary>
    /// Internal string resources.
    /// </summary>
    internal class SR
    {
        public const string InvalidMessage = "Function is marker for Query Analyze. Can't call directly.";
    }

    public static class Aggregate
    {
        /// <summary>
        /// AVG(numeric_expr). Returns the average of the values for a group of rows computed by numeric_expr. Rows with a NULL value are not included in the calculation.
        /// </summary>
        [FunctionName("AVG")]
        public static int Average(int numericExpression)
        {
            throw new InvalidOperationException(SR.InvalidMessage);
        }

        /// <summary>
        /// CORR(numeric_expr, numeric_expr). Returns the Pearson correlation coefficient of a set of number pairs.
        /// </summary>
        [FunctionName("CORR")]
        public static int Correlation(long numericExpression1, long numericExpression2)
        {
            throw new InvalidOperationException(SR.InvalidMessage);
        }


        [FunctionName("COVAR_POP")]
        public static int CovariancePop()
        {
            throw new InvalidOperationException(SR.InvalidMessage);
        }

        [FunctionName("COVAR_SAMP")]
        public static int CovarianceSample()
        {
            throw new InvalidOperationException(SR.InvalidMessage);
        }

        /// <summary>
        /// COUNT(*)
        /// </summary>
        [FunctionName("COUNT", SpecifiedFormatterType = typeof(CountAllFormatter))]
        public static int Count()
        {
            throw new InvalidOperationException(SR.InvalidMessage);
        }

        [FunctionName("COUNT")]
        public static int Count(object field)
        {
            throw new InvalidOperationException(SR.InvalidMessage);
        }

        [FunctionName("COUNT(DISTINCT)", SpecifiedFormatterType = typeof(CountDistinctFormatter))]
        public static int CountDistinct(object field)
        {
            throw new InvalidOperationException(SR.InvalidMessage);
        }

        [FunctionName("COUNT(DISTINCT)", SpecifiedFormatterType = typeof(CountDistinctFormatter))]
        public static int CountDistinct(object field, int n)
        {
            throw new InvalidOperationException(SR.InvalidMessage);
        }

        class CountAllFormatter : ISpeficiedFormatter
        {
            public string Format(MethodCallExpression node)
            {
                return "COUNT(*)";
            }
        }

        class CountDistinctFormatter : ISpeficiedFormatter
        {
            public string Format(MethodCallExpression node)
            {
                var innerTranslator = new BigQueryTranslateVisitor(0, 0, FormatOption.Flat);
                var args = string.Join(", ", node.Arguments.Select(x => innerTranslator.VisitAndClearBuffer(x)));

                return "COUNT(DISTINCT " + args + ")";
            }
        }
    }

    public static class Arithmetic
    {
        /// <summary>+</summary>
        public static int Addition(int x, int y)
        {
            throw new InvalidOperationException(SR.InvalidMessage);
        }

        /// <summary>Returns the number of bits that are set in numeric_expr.</summary>
        public static int BitCount(int numericExpression)
        {
            throw new InvalidOperationException(SR.InvalidMessage);
        }
    }

    public static class Comparison
    {
        /// <summary>Returns true if numeric_expr is positive or negative infinity.</summary>
        [FunctionName("IS_INF")]
        public static bool IsInfinity(long numericExpr) { throw new InvalidOperationException(SR.InvalidMessage); }
        /// <summary>Returns true if numeric_expr is positive or negative infinity.</summary>
        [FunctionName("IS_INF")]
        public static bool IsInfinity(double numericExpr) { throw new InvalidOperationException(SR.InvalidMessage); }
        [FunctionName("IS_NAN")]
        /// <summary>Returns true if numeric_expr is the special NaN numeric value.</summary>
        public static bool IsNAN(long numericExpr) { throw new InvalidOperationException(SR.InvalidMessage); }
        /// <summary>Returns true if numeric_expr is the special NaN numeric value.</summary>
        [FunctionName("IS_NAN")]
        public static bool IsNAN(double numericExpr) { throw new InvalidOperationException(SR.InvalidMessage); }
    }

    public static class Window
    {
        public static void CumulativeDistribution()
        {
        }
    }

    public static class DateAndTime
    {
        public static void CurrentDate()
        {
        }

        public static void CurrentTime()
        {
        }
    }

    public static class Mathematical
    {
        [FunctionName("ABS")]
        public static int Abs(long numericExpr) { throw new InvalidOperationException(SR.InvalidMessage); }
        [FunctionName("ABS")]
        public static int Abs(double numericExpr) { throw new InvalidOperationException(SR.InvalidMessage); }
    }

    public static class Other
    {
        // Case => () ? :

        // If => () ? :

        [FunctionName("HASH")]
        public static long Hash(object expr) { throw new InvalidOperationException(SR.InvalidMessage); }

        public static void Position()
        {

        }
    }
}
