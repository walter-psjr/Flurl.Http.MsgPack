using System;
using System.Diagnostics.CodeAnalysis;

namespace Flurl.Http.MsgPack.Test.Models
{
    [ExcludeFromCodeCoverage]
    internal class TypelessModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}