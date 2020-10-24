using NUnit.Framework;
using System;

namespace Loans.Tests02
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class ProductComparisonAttribute: CategoryAttribute
    {

    }
}
