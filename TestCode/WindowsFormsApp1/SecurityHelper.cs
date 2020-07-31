using Cmf.Foundation.BusinessOrchestration.SecurityManagement.InputObjects;
using Cmf.Foundation.BusinessOrchestration.SecurityManagement.OutputObjects;
using Cmf.Foundation.Security;
using Cmf.Proxy.LightweightBusinessObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.ServiceModel;
using System.Windows;

namespace TestForm
{
	public class SecurityHelper
	{
		public static string DefaultDomain = "pssl";

		public static FunctionalityCollection GetUserFunction(string userName, out string error)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Expected O, but got Unknown
			//IL_006d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Expected O, but got Unknown
			FunctionalityCollection result = null;
			SecurityManagementClient val = (SecurityManagementClient)(object)new SecurityManagementClient();
			((ClientBase<ISecurityManagement>)(object)val).ClientCredentials.Windows.ClientCredential.UserName = Authentication.UserName;
			((ClientBase<ISecurityManagement>)(object)val).ClientCredentials.Windows.ClientCredential.Password = Authentication.Password;
			((ClientBase<ISecurityManagement>)(object)val).ClientCredentials.Windows.ClientCredential.Domain = Authentication.Domain;
			RoleCollection userRole = GetUserRole(userName, out error);
			if (userRole != null && ((List<Role>)(object)userRole).Count > 0)
			{
				GetDataGroupsAndFunctionalitiesForRolesInput val2 = (GetDataGroupsAndFunctionalitiesForRolesInput)(object)new GetDataGroupsAndFunctionalitiesForRolesInput();
				val2.Roles=userRole;
				GetDataGroupsAndFunctionalitiesForRolesOutput val3 = null;
				try
				{
					val3 = val.GetDataGroupsAndFunctionalitiesForRoles(val2);
					result = val3.FunctionalityCollection;
					return result;
				}
				catch (TimeoutException)
				{
					error = "WariningMessage_ConnectServiceTimeOutError";
					return result;
				}
				catch (FaultException)
				{
					error = "WariningMessage_ServiceNotEnabledError";
					return result;
				}
				catch (Exception ex3)
				{
					error = ex3.Message;
					return result;
				}
			}
			return result;
		}

		public static RoleCollection GetUserRole(string userName, out string error)
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Expected O, but got Unknown
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Expected O, but got Unknown
			RoleCollection result = null;
			error = string.Empty;
			SecurityManagementClient val = (SecurityManagementClient)(object)new SecurityManagementClient();
			((ClientBase<ISecurityManagement>)(object)val).ClientCredentials.Windows.ClientCredential.UserName = Authentication.UserName;
			((ClientBase<ISecurityManagement>)(object)val).ClientCredentials.Windows.ClientCredential.Password = Authentication.Password;
			((ClientBase<ISecurityManagement>)(object)val).ClientCredentials.Windows.ClientCredential.Domain = Authentication.Domain;
			GetUserByUserAccountInput val2 = (GetUserByUserAccountInput)(object)new GetUserByUserAccountInput();
			if (!userName.Contains("\\"))
			{
				userName = DefaultDomain + "\\" + userName;
			}
			val2.UserAccount=userName;
			GetUserByUserAccountOutput val3 = null;
			try
			{
				val3 = val.GetUserByUserAccount(val2);
				result = val3.User.Roles;
				return result;
			}
			catch (TimeoutException)
			{
				//error = (string)Application.Current.FindResource("WariningMessage_ConnectServiceTimeOutError");
				return result;
			}
			catch (FaultException ex2)
			{
				if (!ex2.Message.Contains("not found in"))
				{
					if (!ex2.Message.Contains("LocalInstance1/SecurityManagement/SecurityManagement"))
					{
						error =ex2.Message;
						return result;
					}
					error = "WariningMessage_ServiceNotEnabledError";
					return result;
				}
				error = "WariningMessage_UserNotFoundError";
				return result;
			}
			catch (Exception ex3)
			{
				error = ex3.Message;
				return result;
			}
		}

		public static string IsValidatedUser(string userName, string password)
		{
			if (!userName.Contains("\\"))
			{
				userName = DefaultDomain + "\\" + userName;
			}
			string empty = string.Empty;
			string text = ConfigurationManager.AppSettings["LDAPPath"];
			if (string.IsNullOrEmpty(text))
			{
				return "Config: [LDAPPath] is null or empry.";
			}
			try
			{
				DirectoryEntry directoryEntry = new DirectoryEntry(text, userName, password, AuthenticationTypes.None);
				directoryEntry.RefreshCache();
				return empty;
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}
	}
}
