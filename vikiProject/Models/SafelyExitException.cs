using System;

namespace vikiProject.Models
{
    public class SafelyExitException : Exception
    {
        public SafelyExitException(string? message) : base(message)
        {
        }
    }
}