/*
 * Copyright (c) 2004-2008 Shift8Read.com
 * All rights reserved.
 * 
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
 * documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
 * to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions 
 * of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
 * TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
 * THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 * DEALINGS IN THE SOFTWARE.
 * 
 * 
*/

using System;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using System.Globalization;

namespace Shift8Read.Dnn.CommunityCreditSubmit
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Settings class manages Module Settings
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Settings : SettingsBase
    {

        #region Base Method Implementations

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// LoadSettings loads the settings from the Database and displays them
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void LoadSettings()
        {
            try
            {
                if (Page.IsPostBack == false)
                {
                    txtAffiliateCode.Text = AffiliateCode.ToString(CultureInfo.CurrentCulture);
                    txtAffiliateKey.Text = AffiliateKey.ToString(CultureInfo.CurrentCulture);
                    txtLastName.Text = LastName.ToString(CultureInfo.CurrentCulture);
                    txtFirstName.Text = FirstName.ToString(CultureInfo.CurrentCulture);
                    txtEmailAddress.Text = EmailAddress.ToString(CultureInfo.CurrentCulture);


                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpdateSettings saves the modified settings to the Database
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void UpdateSettings()
        {
            try
            {

                ModuleController modules = new ModuleController();

                //modules.UpdateTabModuleSetting(this.TabModuleId, "LogBreadCrumb", (chkLogBreadcrumb.Checked ? "true" : "false"));
                AffiliateCode = txtAffiliateCode.Text.Trim().ToString();
                AffiliateKey = txtAffiliateKey.Text.Trim().ToString();
                EmailAddress= txtEmailAddress.Text.Trim().ToString();
                LastName = txtLastName.Text.Trim().ToString();
                FirstName = txtFirstName.Text.Trim().ToString();

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion


        private string AffiliateCode
        {
            set
            {
                ModuleController modules = new ModuleController();
                modules.UpdateTabModuleSetting(this.TabModuleId, "AffiliateCode", value.ToString(CultureInfo.InvariantCulture));
            }
            get
            {
                object o = Settings["AffiliateCode"];
                if (o != null)
                    return (o.ToString());
                return string.Empty;
            }
        }
        private string AffiliateKey
        {
            set
            {
                ModuleController modules = new ModuleController();
                modules.UpdateTabModuleSetting(this.TabModuleId, "AffiliateKey", value.ToString(CultureInfo.InvariantCulture));
            }
            get
            {
                object o = Settings["AffiliateKey"];
                if (o != null)
                    return (o.ToString());
                return string.Empty;
            }
        }
        private string FirstName
        {
            set
            {
                ModuleController modules = new ModuleController();
                modules.UpdateTabModuleSetting(this.TabModuleId, "FirstName", value.ToString(CultureInfo.InvariantCulture));
            }
            get
            {
                object o = Settings["FirstName"];
                if (o != null)
                    return (o.ToString());
                return string.Empty;
            }
        }
        private string LastName
        {
            set
            {
                ModuleController modules = new ModuleController();
                modules.UpdateTabModuleSetting(this.TabModuleId, "LastName", value.ToString(CultureInfo.InvariantCulture));
            }
            get
            {
                object o = Settings["LastName"];
                if (o != null)
                    return (o.ToString());
                return string.Empty;
            }
        }
        private string EmailAddress
        {
            set
            {
                ModuleController modules = new ModuleController();
                modules.UpdateTabModuleSetting(this.TabModuleId, "EmailAddress", value.ToString(CultureInfo.InvariantCulture));
            }
            get
            {
                object o = Settings["EmailAddress"];
                if (o != null)
                    return (o.ToString());
                return string.Empty;
            }
        }
    }

}

