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

namespace Shift8Read.Dnn.CommunityCreditSubmit
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Views8rCommunityCredit class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class PublishArticleList : ModuleBase
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
                    if (Settings.Contains("AffiliateKey") && Settings.Contains("AffiliateCode") && UserInfo!=null)
                    {
                        FillDropDown();
                        BindData();
                    }
                    else
                    {
                        //todo:display a configuration message here
                    }
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

        private void FillDropDown()
        {
            ItemRelationship.DisplayCategoryHierarchy(cboCategories, -1, PortalId, false);

            ListItem li = new ListItem(Localization.GetString("ChooseOne", LocalResourceFile), "-1");
            this.cboCategories.Items.Insert(0, li);

            //module settings for CC API
            CommunityCreditService cs = new CommunityCreditService(Settings["AffiliateCode"].ToString(), Settings["AffiliateKey"].ToString());
            CommunityCredit.Components.PointCategoryCollection pcc = cs.GetPointCategories();

            foreach (PointCategory pcat in pcc)
            {
                cboCCCategories.Items.Add(new ListItem(pcat.Code, pcat.ID.ToString()));
            }

        }

        

        private DataTable GetGridData()
        {
            int categoryId = Convert.ToInt32(this.cboCategories.SelectedValue, CultureInfo.InvariantCulture);

            //set the approval status ID to approved by default, if we're using approvals look for the selected value
            int approvalStatusId = ApprovalStatus.Approved.GetId();


            dgItems.DataSourceID = string.Empty;
            DataSet ds = DataProvider.Instance().GetAdminItemListing(categoryId, ItemType.Article.GetId(), RelationshipType.ItemToParentCategory.GetId(), RelationshipType.ItemToRelatedCategory.GetId(), approvalStatusId, " vi.createddate desc ", PortalId);
            return ds.Tables[0];
        }

        private void BindData()
        {

            dgItems.DataSource = GetGridData();
            dgItems.DataBind();

            dgItems.Visible = true;
            lblMessage.Visible = false;

            if (dgItems.Rows.Count < 1)
            {
                this.lblMessage.Text = String.Format(CultureInfo.CurrentCulture, Localization.GetString("NoArticlesFoundNoApproval", LocalResourceFile), cboCategories.SelectedItem.ToString());
                dgItems.Visible = false;
                lblMessage.Visible = true;
            }
        }

        private int CategoryId
        {
            get
            {
                string id = Request.QueryString["categoryid"];
                return (id == null ? -1 : Convert.ToInt32(id, CultureInfo.InvariantCulture));
            }
        }


        private string GridViewSortDirection
        {
            get { return ViewState["SortDirection"] as string ?? "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }

        private string GridViewSortExpression
        {
            get { return ViewState["SortExpression"] as string ?? string.Empty; }
            set { ViewState["SortExpression"] = value; }
        }


        private string GetSortDirection()
        {
            switch (GridViewSortDirection)
            {
                case "ASC":
                    GridViewSortDirection = "DESC";
                    break;

                case "DESC":
                    GridViewSortDirection = "ASC";
                    break;
            }
            return GridViewSortDirection;
        }

        //private string CategoryName
        //{
        //    get	{return (Convert.ToString(Request.QueryString["category"]));}
        //}

        private int TopLevelId
        {
            get
            {
                string s = Request.QueryString["topLevelId"];
                return (s == null ? -1 : Convert.ToInt32(s, CultureInfo.InvariantCulture));
            }
        }

        #endregion

        #region Protected Methods

        protected void dgItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgItems.DataSource = SortDataTable(GetGridData(), true);
            dgItems.PageIndex = e.NewPageIndex;
            dgItems.DataBind();
        }

        protected DataView SortDataTable(DataTable dataTable, bool isPageIndexChanging)
        {
            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                if (!string.IsNullOrEmpty(GridViewSortExpression))
                {
                    if (isPageIndexChanging)
                    {
                        dataView.Sort = string.Format(CultureInfo.InvariantCulture, "{0} {1}", GridViewSortExpression, GridViewSortDirection);
                    }
                    else
                    {
                        dataView.Sort = string.Format(CultureInfo.InvariantCulture, "{0} {1}", GridViewSortExpression, GetSortDirection());
                    }
                }
                return dataView;
            }
            else
            {
                return new DataView();
            }
        }


        protected void dgItems_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridViewSortExpression = e.SortExpression;
            int pageIndex = dgItems.PageIndex;
            dgItems.DataSource = SortDataTable(GetGridData(), true);
            //dgItems.DataSource = SortDataTable(dgItems.DataSource as DataTable, false);
            dgItems.DataBind();
            dgItems.PageIndex = pageIndex;
        }

        protected static string GetDescription(object description)
        {
            if (description != null)
            {
                return HtmlUtils.Shorten(HtmlUtils.Clean(description.ToString(), true), 200, string.Empty) + "&nbsp";
            }
            return string.Empty;
        }
        
        protected void cmdSubmit_Click(object sender, EventArgs e)
        {
            //parse through the checked items in the list and approve them.
            try
            {
                foreach (GridViewRow gvr in dgItems.Rows)
                {
                    try
                    {
                        HyperLink hlId = (HyperLink)gvr.FindControl("hlId");
                        CheckBox cb = (CheckBox)gvr.FindControl("chkSelect");
                        if (hlId != null && cb != null && cb.Checked)
                        {
                            //approve
                            Article a = (Article)Item.GetItem(Convert.ToInt32(hlId.Text), PortalId, ItemType.Article.GetId(), false);
                            
                            
                            CommunityCreditService cs = new CommunityCreditService(Settings["AffiliateCode"].ToString(), Settings["AffiliateKey"].ToString());
                            
                            CommunityCredit.Components.Earner ec = new CommunityCredit.Components.Earner(UserInfo.FirstName, UserInfo.LastName, UserInfo.Email);
                            CommunityCredit.Components.PointCategory pc = null;
                            CommunityCredit.Components.PointCategoryCollection pcc = cs.GetPointCategories();

                            foreach (PointCategory pcat in pcc)
                            {
                                //regular blog post
                                if (pcat.ID.ToString() == cboCCCategories.SelectedValue)
                                {
                                    pc = pcat;
                                    break;
                                }
                            }

                            //build up the Publish urls and submit
                            //TODO: localize this text
                            CommunityCredit.Components.Task tc = new CommunityCredit.Components.Task("Blog post: " + a.Name,
                                Utility.GetItemLinkUrl(a.ItemId.ToString(),PortalId), pc);
                            if (tc != null)
                                cs.AddCommunityCredit(ec, tc, Convert.ToDateTime(a.StartDate));
                        }
                    }
                    catch (Exception exc)
                    {
                        Exceptions.ProcessModuleLoadException(this, exc);
                    }
                }
                //success
                //TODO: we need to display a friendly submission message here. 
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion

        public string BuildLinkUrl(string qsParameters)
        {
            return DotNetNuke.Common.Globals.NavigateURL(TabId, "", qsParameters);
        }

        public static string ApplicationUrl
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Request.ApplicationPath == "/" ? string.Empty : HttpContext.Current.Request.ApplicationPath;
                }
                return string.Empty;
            }
        }


        public string GetItemLinkUrl(object itemId)
        {
            return Utility.GetItemLinkUrl(Convert.ToInt32(itemId), PortalId, TabId, ModuleId, 0, "");
            #region "old code"

            #endregion
        }


        #region Optional Interfaces





        #endregion

    }

}
