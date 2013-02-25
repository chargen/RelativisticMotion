using System;

namespace RelativisticMotion
{
    public class CausalityViolationException
        :InvalidOperationException
    {
        public CausalityViolationException(string msg)
            : base(msg)
        {
        }
    }
}
