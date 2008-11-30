<%@ Control language="C#" Inherits="Shift8Read.Dnn.CommunityCreditSubmit.View" AutoEventWireup="True" CodeBehind="View.ascx.cs" %>

<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/labelControl.ascx" %>

<asp:UpdateProgress ID="upArticleListProgress" runat="server" AssociatedUpdatePanelID="upnlArticleList">
    <ProgressTemplate>
        <div class="progressWrap">
            <div class="progressUpdateMessage">
                <asp:Label ID="lblProgressUpdate" runat="server" resourcekey="lblProgressUpdate"></asp:Label>
            </div>
        </div>
        <div class="progressUpdate">
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
    	
<asp:UpdatePanel ID="upnlArticleList" runat="server" UpdateMode="Conditional">
    <ContentTemplate>

        <table border="0" class="Normal">
            <tr valign="top">
                <td><dnn:Label ID="lblItemType" ResourceKey="lblItemType" Runat="server" CssClass="Normal" ControlName="cboCategories"></dnn:Label></td>
                <td><asp:DropDownList ID="cboCategories" Runat="server" AutoPostBack="True" CssClass="Normal"></asp:DropDownList></td>
                <td><asp:DropDownList ID="cboCCCategories" Runat="server" AutoPostBack="false" CssClass="Normal"></asp:DropDownList></td>
            </tr>
        </table>
        <div id="divArticleRepeater">
            <asp:GridView ID="dgItems" 
                Visible="false" 
                runat="server" 
                EnableViewState="true" 
                AlternatingRowStyle-CssClass="DataGrid_AlternatingItem Normal"
                HeaderStyle-CssClass="DataGrid_Header"
                RowStyle-CssClass="DataGrid_Item Normal"
                PagerStyle-CssClass="Normal"
                CssClass="Normal" 
                AutoGenerateColumns="false" 
                width="100%"
                AllowPaging="true"
                PagerSettings-Visible="true" 
                PageSize="15"
                OnPageIndexChanging="dgItems_PageIndexChanging"
                AllowSorting="true"
                OnSorting="dgItems_Sorting"
                >
                <Columns>
                
                    <asp:TemplateField ShowHeader="true"  HeaderText="SelectText" ItemStyle-CssClass="Publish_CheckBoxColumn">
                        <ItemTemplate>
                               <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField ShowHeader="true"  HeaderText="ID" SortExpression="ItemId">
                        <ItemTemplate>
                                <asp:HyperLink ID="hlId" runat="server" CssClass="Normal" NavigateUrl='<%# GetItemLinkUrl(DataBinder.Eval(Container.DataItem,"ItemId")) %>'
                                Text='<%# DataBinder.Eval(Container.DataItem,"ItemId") %>'></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField ShowHeader="true"  HeaderText="Name" SortExpression="Name">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem,"Name") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField ShowHeader="true"  HeaderText="Description" SortExpression="Name">
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" CssClass="Normal" Text='<%# GetDescription(DataBinder.Eval(Container.DataItem,"Description")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DisplayName" HeaderText="DisplayName" SortExpression="DisplayName" ItemStyle-CssClass="Normal" />
                    <asp:BoundField DataField="CreatedDate" HeaderText="CreatedDate" SortExpression="CreatedDate" ItemStyle-CssClass="Normal" />
                    <asp:BoundField DataField="LastUpdated" HeaderText="LastUpdated" SortExpression="LastUpdated" ItemStyle-CssClass="Normal" />
                </Columns>
            </asp:GridView>
        </div>
        <asp:label id="lblMessage" runat="server" CssClass="Subhead"></asp:label>
        <br />
        <asp:hyperlink id="lnkAddNewArticle" Runat="server" ResourceKey="lnkAddNewArticle" CssClass="CommandButton"></asp:hyperlink>
        <div style="text-align:center;">
        
            <asp:linkbutton cssclass="CommandButton" id="cmdSubmit" resourcekey="cmdSubmit" runat="server" text="Submit" causesvalidation="False" OnClick="cmdSubmit_Click"></asp:linkbutton>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

