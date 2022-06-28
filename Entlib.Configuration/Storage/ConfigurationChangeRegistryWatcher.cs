#define VS2003
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
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Storage
{
	/// <summary>
	/// <para>Represents a <see cref="IConfigurationChangeWatcher"/> for a <see cref="RegistryStorageProvider"/>.</para>
	/// </summary>
	public class ConfigurationChangedRegistryWatcher : IConfigurationChangeWatcher
	{
		private static readonly string eventSourceName = SR.RegistryWatcherEventSource;
		private string configurationSectionName;
		private string strConfigRegistrySubkey;
		private AllowedRegistryHive configRegistryRoot;
		private int pollDelayInMilliseconds = defaultPollDelayInMilliseconds;
		private static int defaultPollDelayInMilliseconds = 15000;
		private static readonly object configurationChangedKey = new object();
		private Thread pollingThread;
		private EventHandlerList eventHandlers = new EventHandlerList();

		private class RegistryNotification
		{
			[Flags]
				public enum RegNotifyFilterFlags
			{
				REG_NOTIFY_CHANGE_ATTRIBUTES = 2,
				REG_NOTIFY_CHANGE_LAST_SET = 4,
				REG_NOTIFY_CHANGE_NAME = 1,
				REG_NOTIFY_CHANGE_SECURITY = 8
			}

			public const int ERROR_KEY_DELETED = 0x3fa;

			[DllImport("advapi32.dll", CharSet=CharSet.Unicode, SetLastError=true, ExactSpelling=true)]
			public static extern int RegNotifyChangeKeyValue(IntPtr key, bool watchSubtree, RegNotifyFilterFlags notifyRegFilter, IntPtr notifyEvent, bool asynchronous);
		}

		// testing only methods
		internal static void SetDefaultPollDelayInMilliseconds(int newDefaultPollDelayInMilliseconds)
		{
			defaultPollDelayInMilliseconds = newDefaultPollDelayInMilliseconds;
		}

		internal static void ResetDefaultPollDelay()
		{
			defaultPollDelayInMilliseconds = 15000;
		}

		internal void SetPollDelayInMilliseconds(int newDelayInMilliseconds)
		{
			pollDelayInMilliseconds = newDelayInMilliseconds;
		}

		/// <summary>
		/// <para>Initialize an instance of the <see cref="ConfigurationChangedRegistryWatcher"/> class with the <see cref="RegistryHive"/>, the sub key, and the name of the section.</para>
		/// </summary>
		/// <param name="configRegRoot">The root <see cref="AllowedRegistryHive"/>.</param>
		/// <param name="configRegSubKey">The sub key where the configuration data is stored.</param>
		/// <param name="configurationSectionName">The configuration section name.</param>
		public ConfigurationChangedRegistryWatcher(AllowedRegistryHive configRegRoot, string configRegSubKey, string configurationSectionName)
		{
			this.configurationSectionName = configurationSectionName;
			this.configRegistryRoot = configRegRoot;
			this.strConfigRegistrySubkey = configRegSubKey;
		}

		/// <summary>
		/// <para>Releases the unmanaged resources used by the <see cref="ConfigurationChangeFileWatcher"/> and optionally releases the managed resources.</para>
		/// </summary>
		~ConfigurationChangedRegistryWatcher()
		{
			Disposing(false);
		}

		/// <summary>
		/// Event raised when the underlying persistence mechanism for configuration notices that
		/// the persistent representation of configuration information has changed.
		/// </summary>
		public event ConfigurationChangedEventHandler ConfigurationChanged
		{
			add { eventHandlers.AddHandler(configurationChangedKey, value); }
			remove { eventHandlers.RemoveHandler(configurationChangedKey, value); }
		}

		/// <summary>
		/// <para>Gets the name of the configuration section being watched.</para>
		/// </summary>
		/// <value>
		/// <para>The name of the configuration section being watched.</para>
		/// </value>
		public string SectionName
		{
			get { return configurationSectionName; }
		}

		/// <summary>
		/// <para>Starts watching the configuration file.</para>
		/// </summary>
		public void StartWatching()
		{
			if (pollingThread == null)
			{
				pollingThread = new Thread(new ThreadStart(Poller));
				pollingThread.IsBackground = true;
				pollingThread.Start();
			}
		}

		/// <summary>
		/// <para>Stops watching the configuration file.</para>
		/// </summary>
		public void StopWatching()
		{
			if (pollingThread != null)
			{
				pollingThread.Interrupt();
				pollingThread = null;
			}
		}

		/// <summary>
		/// <para>Releases the unmanaged resources used by the <see cref="ConfigurationChangeFileWatcher"/> and optionally releases the managed resources.</para>
		/// </summary>
		public void Dispose()
		{
			Disposing(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// <para>Releases the unmanaged resources used by the <see cref="ConfigurationChangeFileWatcher"/> and optionally releases the managed resources.</para>
		/// </summary>
		/// <param name="isDisposing">
		/// <para><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.</para>
		/// </param>
		protected virtual void Disposing(bool isDisposing)
		{
			if (isDisposing)
			{
				eventHandlers.Dispose();
				StopWatching();
			}
		}

		private RegistryKey GetRegistrySubKey(AllowedRegistryHive root, string subKey)
		{
			RegistryKey subKeyObject = null;
			switch (root)
			{
				case AllowedRegistryHive.CurrentUser:
					subKeyObject = Registry.CurrentUser.OpenSubKey(subKey, false);
					break;
				case AllowedRegistryHive.LocalMachine:
					subKeyObject = Registry.LocalMachine.OpenSubKey(subKey, false);
					break;
				case AllowedRegistryHive.Users:
					subKeyObject = Registry.Users.OpenSubKey(subKey, false);
					break;
				default:
					//  if they ask for a disallowed Hive, throw here...limit to HKCU, HKU, HKLM
					throw new Exception(SR.ExceptionConfigurationRegistryKeyDisallowed(this.configRegistryRoot.ToString()));
			}

			return subKeyObject;
		}

		private void Poller()
		{
			AutoResetEvent evtNotify = new AutoResetEvent(false);
			using(RegistryKey configKey = GetRegistrySubKey(this.configRegistryRoot, this.strConfigRegistrySubkey))
			{
				if (configKey != null)			// do nothing if the key is not available
				{					
					FieldInfo infoField = configKey.GetType().GetField("hkey", BindingFlags.Instance | BindingFlags.NonPublic);
				
#if VS2003					
					IntPtr keyHandle = (IntPtr)infoField.GetValue(configKey);
#endif

#if VS2005B2	
					SafeHandle handle = infoField.GetValue(configKey) as SafeHandle;
					IntPtr keyHandle = handle.DangerousGetHandle();
#endif

					int iError = RegistryNotification.RegNotifyChangeKeyValue(keyHandle, true,
						RegistryNotification.RegNotifyFilterFlags.REG_NOTIFY_CHANGE_ATTRIBUTES |
						RegistryNotification.RegNotifyFilterFlags.REG_NOTIFY_CHANGE_LAST_SET |
						RegistryNotification.RegNotifyFilterFlags.REG_NOTIFY_CHANGE_NAME |
						RegistryNotification.RegNotifyFilterFlags.REG_NOTIFY_CHANGE_SECURITY, evtNotify.Handle, true);
					
					if (iError == 0)
					{
						while (true)
						{
							try
							{
								evtNotify.WaitOne();
							}
							catch (ThreadInterruptedException)
							{
								return;
							}
							OnConfigurationChanged();
						}
					}
					else
					{
						throw new ConfigurationException(SR.ExceptionConfigurationRegistryKeyInvalid(strConfigRegistrySubkey));
					}
				}
			}
		}

		/// <summary>
		/// <para>Raises the <see cref="ConfigurationChanged"/> event.</para>
		/// </summary>
		protected virtual void OnConfigurationChanged()
		{
			ConfigurationChangedEventHandler callbacks = (ConfigurationChangedEventHandler)eventHandlers[configurationChangedKey];
			ConfigurationChangedEventArgs eventData = new ConfigurationRegistryChangedEventArgs(configRegistryRoot, strConfigRegistrySubkey, configurationSectionName);

			try
			{
				if (callbacks != null)
				{
					callbacks(this, eventData);
				}
			}
			catch (Exception e)
			{
				EventLog.WriteEntry(eventSourceName, SR.ExceptionEventRaisingFailed + ":" + e.Message);
			}
		}
	}
}
