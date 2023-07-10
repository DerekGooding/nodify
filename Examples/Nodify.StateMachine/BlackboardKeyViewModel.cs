using Nodify.Shared;
using System.Collections.Generic;

namespace Nodify.StateMachine
{
    public class BlackboardKeyViewModel : ObservableObject
    {
        // Cache the key and the input value so we can restore them when swapping input types
        private readonly Dictionary<bool, object?> _values = new();

        public string? PropertyName { get; set; }

        private string _name = "New key";
        public string Name
        {
            get => _name;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    SetProperty(ref _name, value);
                }
            }
        }

        private BlackboardKeyType _type;
        public BlackboardKeyType Type
        {
            get => _type;
            set
            {
                if (SetProperty(ref _type, value))
                {
                    Value = GetDefaultValue(_type);
                }
            }
        }

        private object? _value = BoxValue.False;
        public object? Value
        {
            get => _value;
            set => SetProperty(ref _value, GetRealValue(value)).Then(() => _values[ValueIsKey] = Value);
        }

        private bool _valueIsKey;
        public bool ValueIsKey
        {
            get => _valueIsKey;
            set
            {
                if (SetProperty(ref _valueIsKey, value) && _values.TryGetValue(_valueIsKey, out object? existingValue))
                {
                    Value = existingValue;
                }
            }
        }

        private bool _canChangeType = true;
        public bool CanChangeType
        {
            get => _canChangeType;
            set => SetProperty(ref _canChangeType, value);
        }

        private object? GetRealValue(object? value)
        {
            if (value is not string str) return value;
            switch (Type)
            {
                case BlackboardKeyType.Boolean:
                    _ = bool.TryParse(str, out bool b);
                    return b;
                case BlackboardKeyType.Integer:
                    _ = int.TryParse(str, out int i);
                    return i;
                case BlackboardKeyType.Double:
                    _ = double.TryParse(str, out double d);
                    return d;
                case BlackboardKeyType.Object:
                    return str;
                default:
                    return value;
            }
        }

        public static object? GetDefaultValue(BlackboardKeyType type)
            => type switch
            {
                BlackboardKeyType.Boolean => BoxValue.False,
                BlackboardKeyType.Integer => BoxValue.Int0,
                BlackboardKeyType.Double => BoxValue.Double0,
                BlackboardKeyType.String => null,
                BlackboardKeyType.Object => null,
                _ => null
            };
    }
}
