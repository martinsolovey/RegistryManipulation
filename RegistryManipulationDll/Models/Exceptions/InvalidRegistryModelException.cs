namespace HirokuScript.RegistryInteraction.Models
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    internal class InvalidRegistryModelException : Exception
    {
        public InvalidRegistryModelException()
        {
        }

        public InvalidRegistryModelException(string message) : base(message)
        {
        }

        public InvalidRegistryModelException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidRegistryModelException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}