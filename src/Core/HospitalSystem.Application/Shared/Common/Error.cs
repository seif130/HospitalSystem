using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HospitalSystem.Application.Shared.Common
{
    public sealed class Error : IEquatable<Error>
    {
        public static readonly Error None = new(string.Empty, string.Empty, ErrorType.None);

        public string Code { get; }
        public string Message { get; }
        public ErrorType Type { get; }

        private Error(string code, string message, ErrorType type)
        {
            Code = code;
            Message = message;
            Type = type;
        }

        public static Error NotFound(string code, string message) => new(code, message, ErrorType.NotFound);
        public static Error Validation(string code, string message) => new(code, message, ErrorType.Validation);
        public static Error Conflict(string code, string message) => new(code, message, ErrorType.Conflict);
        public static Error Failure(string code, string message) => new(code, message, ErrorType.Failure);
        public static Error Unauthorized(string code, string message) => new(code, message, ErrorType.Unauthorized);

        public bool Equals(Error? other) => other is not null && Code == other.Code && Type == other.Type;
        public override bool Equals(object? obj) => Equals(obj as Error);
        public override int GetHashCode() => HashCode.Combine(Code, Type);
        public override string ToString() => $"{Code}: {Message}";
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]

    public enum ErrorType { None = 1, Failure = 2, Validation = 3, NotFound = 4, Conflict = 5, Unauthorized = 6 }
}
