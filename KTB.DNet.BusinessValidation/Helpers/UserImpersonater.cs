#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : UserImpersonater class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
#endregion

namespace KTB.DNet.BusinessValidation
{
    public class UserImpersonater : IDisposable
    {
        #region Variables
        private string _userName;
        private string _password;
        private string _machineName;
        private IntPtr tokenHandle = IntPtr.Zero;
        private IntPtr duplicateTokenHandle = IntPtr.Zero;
        private WindowsImpersonationContext impersonatedContext;
        bool disposed = false;
        #endregion

        #region Constants
        // constants
        const int LOGON32_PROVIDER_DEFAULT = 0;
        const int LOGON32_LOGON_INTERACTIVE = 9;
        const int SECURITY_IMPERSONATION_LEVEL = 2;
        #endregion

        #region Public Methods
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passwd"></param>
        /// <param name="machine"></param>
        public UserImpersonater(string user, string passwd, string machine)
        {
            _userName = user;
            _password = passwd;
            _machineName = machine;
        }

        /// <summary>
        /// Start the impersonate
        /// </summary>
        /// <returns></returns>
        [PermissionSetAttribute(SecurityAction.Demand, Name = "FullTrust")]
        public bool Start()
        {
            try
            {
                // login the user based on passed credentials
                bool isLogonSucceed = SafeNativeMethods.LogonUser(_userName, _machineName, _password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref tokenHandle);
                if (!isLogonSucceed)
                {
                    int error = Marshal.GetLastWin32Error();
                    throw new Win32Exception(error);
                }

                // duplicate validation
                bool returnValue = SafeNativeMethods.DuplicateToken(tokenHandle, SECURITY_IMPERSONATION_LEVEL, ref duplicateTokenHandle);

                // validate
                if (returnValue && isLogonSucceed)
                {
                    WindowsIdentity newId = new WindowsIdentity(duplicateTokenHandle);
                    impersonatedContext = newId.Impersonate();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed Impersonate : " + ex.Message);
            }
        }

        /// <summary>
        /// Stop the impersonate
        /// </summary>
        [PermissionSetAttribute(SecurityAction.Demand, Name = "FullTrust")]
        public void Stop()
        {
            if (impersonatedContext == null)
            {
                return;
            }
            else
            {
                impersonatedContext.Undo();
            }

            if (!tokenHandle.Equals(IntPtr.Zero))
            {
                SafeNativeMethods.CloseHandle(tokenHandle);
            }

            if (!duplicateTokenHandle.Equals(IntPtr.Zero))
            {
                SafeNativeMethods.CloseHandle(duplicateTokenHandle);
            }

            // reset
            impersonatedContext = null;
        }

        /// <summary>
        /// Public implementation of Dispose pattern callable by consumers.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Dispose Method
        /// <summary>
        /// Protected implementation of Dispose pattern.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                if (impersonatedContext != null)
                {
                    impersonatedContext.Dispose();
                }
            }

            // Free any unmanaged objects here.
            tokenHandle = IntPtr.Zero;
            duplicateTokenHandle = IntPtr.Zero;

            disposed = true;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~UserImpersonater()
        {
            Dispose(false);
        }
        #endregion
    }

    /// <summary>
    /// Safe native methods
    /// </summary>
    [SuppressUnmanagedCodeSecurityAttribute]
    internal static class SafeNativeMethods
    {
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        internal static extern bool CloseHandle(IntPtr handle);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool DuplicateToken(IntPtr ExistingTokenHandle, int SECURITY_IMPERSONATION_LEVEL, ref IntPtr DuplicateTokenHandle);
    }
}