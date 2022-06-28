//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Configuration Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Security.Cryptography;
using System.Security.Permissions;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Protection;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Protection
{
    /// <summary>
    /// <para>Creates keys for a <see cref="KeyedHashAlgorithm"/>.</para>
    /// </summary>
    public class KeyedHashAlgorithmKeyCreator : IKeyCreator
    {
        private KeyedHashAlgorithm algorithm;
        private RandomNumberGenerator randomNumberGenerator;

        private KeyedHashAlgorithmKeyCreator()
        {}

        /// <summary>
        /// <para>Initializes an instance of the <see cref="KeyedHashAlgorithmKeyCreator"/> class for a specific <see cref="KeyedHashAlgorithm"/>.</para>
        /// </summary>
        /// <param name="typeName">The assembly qualified name of a class which derives from a <see cref="KeyedHashAlgorithm"/>.</param>
        public KeyedHashAlgorithmKeyCreator(string typeName)
        {
            this.algorithm = CreateAlgorithm(typeName);
            randomNumberGenerator = RNGCryptoServiceProvider.Create();
        }

        /// <summary>
        /// <para>Gets the length of the key.</para>
        /// </summary>
        /// <value>The length of the key.</value>
        public int KeyLength
        {
            get { return algorithm.Key.Length; }
        }

        /// <summary>
        /// <para>Generates a random key.</para>
        /// </summary>
        /// <returns><para>A random key.</para></returns>
        public byte[] GenerateKey()
        {
            byte[] key = new byte[KeyLength];
            randomNumberGenerator.GetBytes(key);
            return key;
        }

        /// <summary>
        /// <para>Determines if the <paramref name="key"/> is valid.</para>
        /// </summary>
        /// <param name="key">The key to test.</param>
        /// <returns><para><see langword="true"/> if the key is valid; otherwise <see langword="false"/>.</para></returns>
        public bool KeyIsValid(byte[] key)
        {
            return (key != null) && (key.Length != 0);
        }

        [ReflectionPermission(SecurityAction.Demand, TypeInformation = true)]
        private static KeyedHashAlgorithm CreateAlgorithm(string typeName)
        {
            Type algorithmType = Type.GetType(typeName, true);
            return (KeyedHashAlgorithm)Activator.CreateInstance(algorithmType);
        }
    }
}
