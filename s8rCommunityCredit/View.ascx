<%@ Control language="C#" Inherits="Shift8Read.Dnn.CommunityCreditSubmit.View" AutoEventWireup="True" CodeBehind="View.ascx.cs" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/labelControl.ascx" %>
<div class="Normal">
    <div>
        <asp:Label ID="lblError" runat="server" Visible="false" CssClass="NormalRed"></asp:Label>
        <asp:Label ID="lblUserInfo" runat="server" Visible="true" resourcekey="lblUserInfo"></asp:Label>
    </div>

    <div id="s8rMaster" >
        <div class="s8rRow">
                <div class="s8rLabelCell">
                    <dnn:label ID="lblCategory" runat="server" />
                </div>
                <div class="s8rLabelCell">
                    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="ddlCategory_SelectedIndexChanged" />
                </div>
        </div>
        <div class="s8rRow">
                <div class="s8rLabelCell">
                    <dnn:label ID="lblSubCategory" runat="server" />
                </div>
                <div class="s8rLabelCell">
                    <asp:DropDownList ID="ddlSubCategory" runat="server" />
                </div>
        </div>

        <div class="s8rRow">
                <div class="s8rLabelCell">
                    <dnn:label ID="lblDescription" runat="server" />
                </div>
                <div class="s8rLabelCell">
                    <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                </div>
        </div>

        <div class="s8rRow">
                <div class="s8rLabelCell">
                    <dnn:label ID="lblDateEarned" runat="server" />
                </div>
                <div class="s8rLabelCell">
                    <asp:TextBox ID="txtDateEarned" runat="server"></asp:TextBox>
                </div>
        </div>
        <div class="s8rRow">
                <div class="s8rLabelCell">
                    <dnn:label ID="lblUrl" runat="server" />
                </div>
                <div class="s8rLabelCell">
                    <asp:TextBox ID="txtUrl" runat="server"></asp:TextBox>
                </div>
        </div>
        <asp:LinkButton ID="lbSubmit" runat="server" onclick="lbSubmit_Click" resourcekey="lbSubmit" />
    </div>

</div>