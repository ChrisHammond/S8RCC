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
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using DotNetNuke.Entities.Modules;

namespace Shift8Read.Dnn.CommunityCreditSubmit
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Views8rCommunityCredit class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : ModuleBase, IActionable
    {

        #region Event Handlers

        override protected void OnInit(EventArgs e)
        {

            InitializeComponent();
            base.OnInit(e);
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
                    //check to see if the settings are configured first.
                    if (Settings.Contains("AffiliateKey") && Settings.Contains("AffiliateCode") && UserInfo !=null)
                    {
                        FillDropDown();
                        BindData();
                    }
                    else
                    {
                        //todo:display a configuration message here
                        lblError.Text = String.Format(Localization.GetString("ErrorConfig", LocalResourceFile), EditUrl("ModuleId", ModuleId.ToString(CultureInfo.InvariantCulture), "Module"));
                        lblError.Visible = true;
                    }
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion


        #region Private Methods

        private void FillDropDown()
        {
            //module settings for CC API
            CommunityCreditService cs = new CommunityCreditService(Settings["AffiliateCode"].ToString(), Settings["AffiliateKey"].ToString());
            CommunityCredit.Components.PointCategoryCollection pcc = cs.GetPointCategories();
            //generate a list of Areas
            ListItem blank = new ListItem(Localization.GetString("ChooseOne", LocalResourceFile), "-1");
            ddlCategory.Items.Clear();
            ddlCategory.Items.Add(blank);

            foreach (PointCategory pcat in pcc)
            {
                ListItem li = new ListItem(pcat.Area, pcat.Area);
                if (!ddlCategory.Items.Contains(li))
                {
                    ddlCategory.Items.Add(li);
                }
            }
        }


        private void BindData()
        {
        }


        #endregion

        #region Protected Methods




        protected void lbSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                CommunityCreditService cs = new CommunityCreditService(Settings["AffiliateCode"].ToString(), Settings["AffiliateKey"].ToString());
                CommunityCredit.Components.Earner ec = new CommunityCredit.Components.Earner(UserInfo.FirstName, UserInfo.LastName, UserInfo.Email);
                CommunityCredit.Components.PointCategory pc = null;
                CommunityCredit.Components.PointCategoryCollection pcc = cs.GetPointCategories();

                foreach (PointCategory pcat in pcc)
                {
                    if (pcat.ID.ToString() == ddlSubCategory.SelectedValue)
                    {
                        pc = pcat;
                        break;
                    }
                }
                //TODO: localize this text
                CommunityCredit.Components.Task tc = new CommunityCredit.Components.Task(txtDescription.Text.Trim(),
                   txtUrl.Text.Trim(), pc);
                if (tc != null)
                    cs.AddCommunityCredit(ec, tc, Convert.ToDateTime(txtDateEarned.Text.Trim()));

                //add submission confirmation text
                lblError.Text = Localization.GetString("SubmissionConfirmed", LocalResourceFile);
                txtDateEarned.Text = string.Empty;
                txtUrl.Text = string.Empty;
                txtDescription.Text = string.Empty;


            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
        #endregion


        #region Optional Interfaces


        public ModuleActionCollection ModuleActions
        {
            get
            {
                ModuleActionCollection actions = new ModuleActionCollection();
                actions.Add(GetNextActionID(), Localization.GetString("PublishLink", LocalResourceFile), "", "", "", EditUrl("Publish"), false, SecurityAccessLevel.Edit, true, false);
                //actions.Add(GetNextActionID(), Localization.GetString("ClearCache", LocalSharedResourceFile), "", "", "", EditUrl(Utility.AdminContainer), false, SecurityAccessLevel.Edit, true, false);
                return actions;
            }
        }

        #endregion

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSubCategory.Items.Clear();

            //module settings for CC API
            CommunityCreditService cs = new CommunityCreditService(Settings["AffiliateCode"].ToString(), Settings["AffiliateKey"].ToString());
            CommunityCredit.Components.PointCategoryCollection pcc = cs.GetPointCategories();
            //generate a list of Areas

            foreach (PointCategory pcat in pcc)
            {
                if (pcat.Area == ddlCategory.SelectedValue)
                    ddlSubCategory.Items.Add(new ListItem(pcat.Code, pcat.ID.ToString()));
            }

        }

    }

}
