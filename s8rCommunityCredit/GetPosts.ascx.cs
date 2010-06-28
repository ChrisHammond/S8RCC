/*
 * Copyright (c) 2004-2010 Shift8Read.com
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
using CommunityCredit;
using System.Data;
using System.Web.UI.WebControls;
using Engage.Dnn.Publish;
using DotNetNuke.Services.Localization;
using System.Globalization;
using Engage.Dnn.Publish.Util;
using Engage.Dnn.Publish.Data;
using DotNetNuke.Common.Utilities;
using System.Web;
using CommunityCredit.Components;
using DotNetNuke.Entities.Modules;

namespace Shift8Read.Dnn.CommunityCreditSubmit
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Views8rCommunityCredit class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class GetPosts : ModuleBase
    {

        #region Event Handlers

        override protected void OnInit(EventArgs e)
        {

            InitializeComponent();
            
            if (DotNetNuke.Framework.AJAX.IsInstalled())
            {
                DotNetNuke.Framework.AJAX.RegisterScriptManager();
            }

        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }


        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion

        private void cboCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        private void cboWorkflow_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        #region Private Methods

        private void GetGridData()
        {
            //module settings for CC API
            CommunityCreditService cs = new CommunityCreditService(Settings["AffiliateCode"].ToString(), Settings["AffiliateKey"].ToString());

            DateTime startDate = Convert.ToDateTime(txtStartDate.Text);
            DateTime endDate = Convert.ToDateTime(txtEndDate.Text);

            CommunityCredit.Components.EarnersCreditCollection ecc = cs.GetEarnersByDateRangeAndEmail(EmailAddress, startDate, endDate);
            //dgItems.DataSource = 
            dgView.DataSource = ecc;
            
            dgView.DataBind();
            //dgView.Sort("Date", SortDirection.Ascending);
            CommunityCredit.Components.EarnersCredit ec = new CommunityCredit.Components.EarnersCredit();
        }

        private void BindData()
        {

            GetGridData();
            
        }

        #endregion

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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //check to see if the settings are configured first.
            if (Settings.Contains("AffiliateKey") && Settings.Contains("AffiliateCode") && UserInfo != null)
            {

                BindData();
            }
            else
            {
                //todo:display a configuration message here
            }

        }



        #region Optional Interfaces





        #endregion

    }

}
