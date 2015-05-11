// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Any.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring.Test
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     A utility class for getting random values for various data types.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Any
    {
        #region Constants

        /// <summary>
        ///     The chars from 0 to a.
        /// </summary>
        private const string CharsFrom0ToA = @":;<=>?@";

        /// <summary>
        ///     The chars from 0 to z.
        /// </summary>
        private const string CharsFrom0ToZ = CharsFrom0ToA + CharsFromZtoA;

        /// <summary>
        ///     The chars from z to a.
        /// </summary>
        private const string CharsFromZtoA = @"[\]^`";

        /// <summary>
        ///     The largest char.
        /// </summary>
        private const int LargestChar = 250;

        /// <summary>
        ///     The max string.
        /// </summary>
        private const int MaxString = 3000;

        /// <summary>
        ///     The smallest char.
        /// </summary>
        private const int SmallestChar = 32;

        #endregion

        #region Static Fields

        /// <summary>
        ///     The seed.
        /// </summary>
        private static readonly int Seed = (int)DateTime.Now.Ticks;

        /// <summary>
        ///     The random.
        /// </summary>
        private static readonly Random Random = new Random(Seed);

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The alpha_.
        /// </summary>
        /// <returns>
        ///     The <see cref="char" />.
        /// </returns>
        public static char Alpha()
        {
            char value;
            do
            {
                value = Char('A', 'z');
            }
            while (CharsFromZtoA.IndexOf(value) != -1);
            return value;
        }

        /// <summary>
        ///     The alpha_ numeric.
        /// </summary>
        /// <returns>
        ///     The <see cref="char" />.
        /// </returns>
        public static char Alphanumeric()
        {
            char value;
            do
            {
                value = Char('0', 'z');
            }
            while (CharsFrom0ToZ.IndexOf(value) != -1);
            return value;
        }

        /// <summary>
        ///     The boolean.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool Boolean()
        {
            return Int(0, 1) == 1;
        }

        /// <summary>
        ///     The byte.
        /// </summary>
        /// <returns>
        ///     The <see cref="byte" />.
        /// </returns>
        public static byte Byte()
        {
            return (byte)Int(byte.MinValue, byte.MaxValue);
        }

        /// <summary>
        /// The char.
        /// </summary>
        /// <param name="first">
        /// The first.
        /// </param>
        /// <param name="last">
        /// The last.
        /// </param>
        /// <returns>
        /// The <see cref="char"/>.
        /// </returns>
        public static char Char(char first, char last)
        {
            return (char)Int(first, last);
        }

        /// <summary>
        ///     The char.
        /// </summary>
        /// <returns>
        ///     The <see cref="char" />.
        /// </returns>
        public static char Char()
        {
            return (char)PositiveInt(255);
        }

        /// <summary>
        /// The char array.
        /// </summary>
        /// <param name="numberOfElements">
        /// The number of elements.
        /// </param>
        /// <returns>
        /// An <see cref="Array"/> of <see cref="char"/>.
        /// </returns>
        public static char[] CharArray(int numberOfElements)
        {
            var charArray = new char[numberOfElements];
            for (var element = 0; element < charArray.Length; element++)
            {
                charArray[element] = Char();
            }

            return charArray;
        }

        /// <summary>
        ///     The decimal.
        /// </summary>
        /// <returns>
        ///     The <see cref="decimal" />.
        /// </returns>
        public static decimal Decimal()
        {
            var double0To1 = Random.NextDouble();
            var randomDouble = (double0To1 * (double)decimal.MaxValue) + ((1 - double0To1) * (double)decimal.MinValue);
            return (decimal)randomDouble;
        }

        /// <summary>
        /// The double.
        /// </summary>
        /// <param name="min">
        /// The min.
        /// </param>
        /// <param name="max">
        /// The max.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", 
            Justification = "This is test code.")]
        public static double Double(double min = double.MinValue, double max = double.MaxValue)
        {
            var double0To1 = Random.NextDouble();
            var randomDouble = (double0To1 * max) + ((1 - double0To1) * min);
            return randomDouble;
        }

        /// <summary>
        /// The double array.
        /// </summary>
        /// <param name="numberOfElements">
        /// The number of elements.
        /// </param>
        /// <param name="min">
        /// The min.
        /// </param>
        /// <param name="max">
        /// The max.
        /// </param>
        /// <returns>
        /// An <see cref="Array"/> of <see cref="double"/>.
        /// </returns>
        public static double[] DoubleArray(int numberOfElements, double min, double max)
        {
            var doubleArray = new double[numberOfElements];
            for (var element = 0; element < doubleArray.Length; element++)
            {
                doubleArray[element] = Double(min, max);
            }

            return doubleArray;
        }

        /// <summary>
        /// The float.
        /// </summary>
        /// <param name="min">
        /// The min.
        /// </param>
        /// <param name="max">
        /// The max.
        /// </param>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", 
            Justification = "This is test code.")]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "float", 
            Justification = "This is test code.")]
        public static float Float(float min = float.MinValue, float max = float.MaxValue)
        {
            return (float)Double(min, max);
        }

        /// <summary>
        /// The float array.
        /// </summary>
        /// <param name="numberOfElements">
        /// The number of elements.
        /// </param>
        /// <returns>
        /// An <see cref="Array"/> of <see cref="float"/>.
        /// </returns>
        public static float[] FloatArray(int numberOfElements)
        {
            var floatArray = new float[numberOfElements];
            for (var element = 0; element < floatArray.Length; element++)
            {
                floatArray[element] = Float();
            }

            return floatArray;
        }

        /// <summary>
        /// The float at least.
        /// </summary>
        /// <param name="min">
        /// The min.
        /// </param>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "float", 
            Justification = "This is test code.")]
        public static float FloatAtLeast(float min)
        {
            return Float(min);
        }

        /// <summary>
        /// The float at most.
        /// </summary>
        /// <param name="max">
        /// The max.
        /// </param>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "float", 
            Justification = "This is test code.")]
        public static float FloatAtMost(float max)
        {
            return Float(max);
        }

        /// <summary>
        /// The integer.
        /// </summary>
        /// <param name="min">
        /// The min.
        /// </param>
        /// <param name="max">
        /// The max.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "max+1", 
            Justification = "This is test code.")]
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", 
            Justification = "This is test code.")]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "int", 
            Justification = "This is test code.")]
        public static int Int(int min = int.MinValue, int max = int.MaxValue - 1)
        {
            var val = Random.Next(min, max + 1);
            return val;
        }

        /// <summary>
        /// The integer at least some minimum.
        /// </summary>
        /// <param name="min">
        /// The min.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "int", 
            Justification = "This is test code.")]
        public static int IntAtLeast(int min)
        {
            return Int(min);
        }

        /// <summary>
        /// The integer at most some maximum.
        /// </summary>
        /// <param name="max">
        /// The max.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "max-1", 
            Justification = "This is test code.")]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "int", 
            Justification = "This is test code.")]
        public static int IntAtMost(int max)
        {
            return Int(int.MinValue, max - 1);
        }

        /// <summary>
        ///     The long.
        /// </summary>
        /// <returns>
        ///     The <see cref="long" />.
        /// </returns>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "long", 
            Justification = "This is test code.")]
        public static long Long()
        {
            return Int() + ((long)Int() >> 32);
        }

        /// <summary>
        /// The name.
        /// </summary>
        /// <param name="length">
        /// The length.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Name(int length)
        {
            var buffer = new char[length];
            for (var position = 0; position < length; position++)
            {
                if (position == 0)
                {
                    buffer[position] = Alpha();
                }
                else
                {
                    buffer[position] = Alphanumeric();
                }
            }

            return new string(buffer);
        }

        /// <summary>
        /// The name array.
        /// </summary>
        /// <param name="numElements">
        /// The number of elements.
        /// </param>
        /// <param name="maxLength">
        /// The maximum length.
        /// </param>
        /// <returns>
        /// An <see cref="Array"/> of <see cref="string"/>.
        /// </returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "num", 
            Justification = "This is test code.")]
        public static string[] NameArray(int numElements, int maxLength)
        {
            var strings = new string[numElements];
            for (var i = 0; i < numElements; i++)
            {
                strings[i] = Name(maxLength);
            }

            return strings;
        }

        /// <summary>
        ///     The one of enumeration.
        /// </summary>
        /// <typeparam name="T">
        ///     The enumeration type.
        /// </typeparam>
        /// <returns>
        ///     A value of the enumeration.
        /// </returns>
        public static T OneOfEnum<T>()
        {
            var enumerationValues = Enum.GetValues(typeof(T));
            var selectedValue = Int(0, enumerationValues.Length - 1);

            return (T)enumerationValues.GetValue(selectedValue);
        }

        /// <summary>
        /// The one of enumeration except a value.
        /// </summary>
        /// <param name="undesiredEnumerationValue">
        /// The undesired enumeration value.
        /// </param>
        /// <typeparam name="T">
        /// The enumeration type.
        /// </typeparam>
        /// <returns>
        /// A value of the enumeration.
        /// </returns>
        public static T OneOfEnumExcept<T>(T undesiredEnumerationValue)
        {
            T enumerationValue;
            do
            {
                enumerationValue = OneOfEnum<T>();
            }
            while (enumerationValue.Equals(undesiredEnumerationValue));
            return enumerationValue;
        }

        /// <summary>
        ///     The positive double.
        /// </summary>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double PositiveDouble()
        {
            return Double(0);
        }

        /// <summary>
        /// The positive double.
        /// </summary>
        /// <param name="max">
        /// The max.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public static double PositiveDouble(double max)
        {
            return Double(0, max);
        }

        /// <summary>
        /// The positive double array.
        /// </summary>
        /// <param name="numberOfElements">
        /// The number of elements.
        /// </param>
        /// <returns>
        /// An <see cref="Array"/> of <see cref="double"/>.
        /// </returns>
        public static double[] PositiveDoubleArray(int numberOfElements)
        {
            return DoubleArray(numberOfElements, 0, double.MaxValue);
        }

        /// <summary>
        ///     The positive float.
        /// </summary>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "float", 
            Justification = "This is test code.")]
        public static float PositiveFloat()
        {
            return Float(0);
        }

        /// <summary>
        /// The positive float.
        /// </summary>
        /// <param name="max">
        /// The max.
        /// </param>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "float", 
            Justification = "This is test code.")]
        public static float PositiveFloat(float max)
        {
            return Float(0, max);
        }

        /// <summary>
        /// The positive float array.
        /// </summary>
        /// <param name="numberOfElements">
        /// The number of elements.
        /// </param>
        /// <returns>
        /// An <see cref="Array"/> of <see cref="float"/>.
        /// </returns>
        public static float[] PositiveFloatArray(int numberOfElements)
        {
            var positiveFloatArray = new float[numberOfElements];
            for (var element = 0; element < positiveFloatArray.Length; element++)
            {
                positiveFloatArray[element] = PositiveFloat();
            }

            return positiveFloatArray;
        }

        /// <summary>
        /// The positive integer.
        /// </summary>
        /// <param name="max">
        /// The maximum value.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", 
            Justification = "This is test code.")]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "int", 
            Justification = "This is test code.")]
        public static int PositiveInt(int max = int.MaxValue - 1)
        {
            return Int(1, max);
        }

        /// <summary>
        /// The range float array.
        /// </summary>
        /// <param name="numberOfElements">
        /// The number of elements.
        /// </param>
        /// <param name="min">
        /// The min.
        /// </param>
        /// <param name="max">
        /// The max.
        /// </param>
        /// <returns>
        /// An <see cref="Array"/> of <see cref="float"/>.
        /// </returns>
        public static float[] RangeFloatArray(int numberOfElements, float min, float max)
        {
            var rangeFloatArray = new float[numberOfElements];
            for (var element = 0; element < rangeFloatArray.Length; element++)
            {
                rangeFloatArray[element] = Float(min, max);
            }

            return rangeFloatArray;
        }

        /// <summary>
        ///     The s byte.
        /// </summary>
        /// <returns>
        ///     The <see cref="sbyte" />.
        /// </returns>
        public static sbyte SByte()
        {
            return (sbyte)Int(sbyte.MinValue, sbyte.MinValue);
        }

        /// <summary>
        ///     The short.
        /// </summary>
        /// <returns>
        ///     The <see cref="short" />.
        /// </returns>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "short", 
            Justification = "This is test code.")]
        public static short Short()
        {
            return (short)Int(short.MinValue, short.MaxValue);
        }

        /// <summary>
        ///     The string.
        /// </summary>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public static string String()
        {
            var length = Int(1, MaxString);
            return String(length);
        }

        /// <summary>
        /// The string.
        /// </summary>
        /// <param name="minLen">
        /// The min len.
        /// </param>
        /// <param name="maxLen">
        /// The max len.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string String(int minLen, int maxLen)
        {
            var length = Int(minLen, maxLen);
            return String(length);
        }

        /// <summary>
        /// The string.
        /// </summary>
        /// <param name="length">
        /// The length.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string String(int length)
        {
            return String(length, (char)SmallestChar, (char)LargestChar);
        }

        /// <summary>
        /// The string.
        /// </summary>
        /// <param name="length">
        /// The length.
        /// </param>
        /// <param name="first">
        /// The first.
        /// </param>
        /// <param name="last">
        /// The last.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Occurs when first is greater than last.
        /// </exception>
        public static string String(int length, char first, char last)
        {
            var buffer = new char[length];
            for (var position = 0; position < length; position++)
            {
                buffer[position] = Char(first, last);
            }

            return new string(buffer);
        }

        /// <summary>
        ///     The unsigned integer.
        /// </summary>
        /// <returns>
        ///     The <see cref="uint" />.
        /// </returns>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "uint", 
            Justification = "This is test code.")]
        public static uint UInt()
        {
            return (uint)Int();
        }

        /// <summary>
        ///     The unsigned long.
        /// </summary>
        /// <returns>
        ///     The <see cref="ulong" />.
        /// </returns>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "ulong", 
            Justification = "This is test code.")]
        public static ulong ULong()
        {
            return (ulong)Long();
        }

        /// <summary>
        ///     The unsigned short.
        /// </summary>
        /// <returns>
        ///     The <see cref="ushort" />.
        /// </returns>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "ushort", 
            Justification = "This is test code.")]
        public static ushort UShort()
        {
            return (ushort)Short();
        }

        #endregion
    }
}