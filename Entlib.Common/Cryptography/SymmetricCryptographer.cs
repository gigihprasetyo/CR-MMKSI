//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Enterprise Library Shared Library
//===============================================================================
// Copyright ? Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.IO;
using System.Security.Cryptography;

namespace Microsoft.Practices.EnterpriseLibrary.Common.Cryptography
{
    /// <summary>
    /// <para>Represents basic cryptography services for a <see cref="SymmetricAlgorithm"/>.</para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// Because the IV (Initialization Vector) has the same distribution as the resulting 
    /// ciphertext, the IV is randomly generated and prepended to the ciphertext.
    /// </para>
    /// </remarks>
    public class SymmetricCryptographer
    {
        private SymmetricAlgorithm algorithm;
        private byte[] key;

        /// <summary>
        /// <para>Initalize a new instance of the <see cref="SymmetricCryptographer"/> class with a <see cref="SymmetricAlgorithm"/> and key.</para>
        /// </summary>
        /// <param name="algorithm"><para>The algorithm in which to perform crypographic functions.</para></param>
        /// <param name="key"><para>The key for the algorithm.</para></param>
        public SymmetricCryptographer(SymmetricAlgorithm algorithm, byte[] key)
        {
            this.algorithm = algorithm;
            this.key = key;
        }

        /// <summary>
        /// <para>Initalize a new instance of the <see cref="SymmetricCryptographer"/> class with an algorithm type and a key.</para>
        /// </summary>
        /// <param name="algorithmType"><para>The qualified assembly name of a <see cref="SymmetricAlgorithm"/>.</para></param>
        /// <param name="key"><para>The key for the algorithm.</para></param>
        public SymmetricCryptographer(string algorithmType, byte[] key)
        {
            this.key = key;
            this.algorithm = GetSymmetricAlgorithm(algorithmType);
        }

        /// <summary>
        /// <para>Encrypts bytes with the initialized algorithm and key.</para>
        /// </summary>
        /// <param name="plaintext"><para>The plaintext in which you wish to encrypt.</para></param>
        /// <returns><para>The resulting ciphertext.</para></returns>
        public byte[] Encrypt(byte[] plaintext)
        {
            byte[] output = null;
            byte[] cipherText = null;

            this.algorithm.Key = key;

            using (ICryptoTransform transform = this.algorithm.CreateEncryptor())
            {
                cipherText = Transform(transform, plaintext);
            }

            output = new byte[IVLength + cipherText.Length];
            Buffer.BlockCopy(this.algorithm.IV, 0, output, 0, IVLength);
            Buffer.BlockCopy(cipherText, 0, output, IVLength, cipherText.Length);

            return output;
        }

        /// <summary>
        /// <para>Decrypts bytes with the initialized algorithm and key.</para>
        /// </summary>
        /// <param name="encryptedText"><para>The text which you wish to decrypt.</para></param>
        /// <returns><para>The resulting plaintext.</para></returns>
        public byte[] Decrypt(byte[] encryptedText)
        {
            byte[] output = null;
            byte[] data = ExtractIV(encryptedText);

            using (ICryptoTransform transform = this.algorithm.CreateDecryptor())
            {
                output = Transform(transform, data);
            }

            return output;
        }

        private static byte[] Transform(ICryptoTransform transform, byte[] buffer)
        {
            byte[] transformBuffer = null;

            using (MemoryStream ms = new MemoryStream())
            {
                CryptoStream cs = null;
                try
                {
                    cs = new CryptoStream(ms, transform, CryptoStreamMode.Write);
                    cs.Write(buffer, 0, buffer.Length);
                    cs.FlushFinalBlock();
                    transformBuffer = ms.ToArray();
                }
                finally
                {
                    if (cs != null)
                    {
                        cs.Close();
                        ((IDisposable)cs).Dispose();
                    } // Close is not called by Dispose
                }
            }

            return transformBuffer;
        }

        private int IVLength
        {
            get
            {
                if (this.algorithm.IV == null)
                {
                    this.algorithm.GenerateIV();
                }
                return this.algorithm.IV.Length;
            }
        }

        private byte[] ExtractIV(byte[] encryptedText)
        {
            byte[] initVector = new byte[IVLength];

            if (encryptedText.Length < IVLength + 1)
            {
                throw new CryptographicException(SR.ExceptionDecrypting);
            }

            byte[] data = new byte[encryptedText.Length - IVLength];

            Buffer.BlockCopy(encryptedText, 0, initVector, 0, IVLength);
            Buffer.BlockCopy(encryptedText, IVLength, data, 0, data.Length);

            this.algorithm.IV = initVector;
            this.algorithm.Key = this.key;

            return data;
        }

        private static SymmetricAlgorithm GetSymmetricAlgorithm(string algorithmType)
        {
            SymmetricAlgorithm sa = null;
            try
            {
                Type type = Type.GetType(algorithmType, true);
                sa = Activator.CreateInstance(type) as SymmetricAlgorithm;
            }
            catch (Exception ex)
            {
                // We want to supress any type of exception here for security reasons.
                CryptographyUtility.LogCryptographyException(ex);
                throw new CryptographicException(SR.ExceptionCreatingSymmetricAlgorithmInstance);
            }

            if (sa == null)
            {
                throw new CryptographicException(SR.ExceptionCastingSymmetricAlgorithmInstance);
            }

            return sa;
        }
    }
}