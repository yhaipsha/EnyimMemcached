﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Enyim.Caching.Memcached
{
	/// <summary>
	/// Implements the default plain text ("PLAIN") Memcached authentication. It expects "userName" and "password" parameters during initalization.
	/// </summary>
	public sealed class PlainTextAuthenticator : ISaslAuthenticationProvider
	{
		private byte[] authData;

		public PlainTextAuthenticator() { }
		public PlainTextAuthenticator(string userName, string password) 
		{
			this.authData = CreateAuthData(userName, password);
		}

		string ISaslAuthenticationProvider.Type
		{
			get { return "PLAIN"; }
		}

		void ISaslAuthenticationProvider.Initialize(Dictionary<string, object> parameters)
		{
			string userName = (string)parameters["userName"];
			string password = (string)parameters["password"];

			this.authData = CreateAuthData(userName, password);
		}

		byte[] ISaslAuthenticationProvider.Authenticate()
		{
			return this.authData;
		}

		byte[] ISaslAuthenticationProvider.Continue(byte[] data)
		{
			return null;
		}

		private static byte[] CreateAuthData(string userName, string password)
		{
			return System.Text.Encoding.UTF8.GetBytes("memcached\0" + userName + "\0" + password);
		}
	}
}

#region [ License information          ]
/* ************************************************************
 *
 * Copyright (c) Attila Kiskó, enyim.com
 *
 * This source code is subject to terms and conditions of 
 * Microsoft Permissive License (Ms-PL).
 * 
 * A copy of the license can be found in the License.html
 * file at the root of this distribution. If you can not 
 * locate the License, please send an email to a@enyim.com
 * 
 * By using this source code in any fashion, you are 
 * agreeing to be bound by the terms of the Microsoft 
 * Permissive License.
 *
 * You must not remove this notice, or any other, from this
 * software.
 *
 * ************************************************************/
#endregion