<%@ Control language="C#" Inherits="Shift8Read.Dnn.CommunityCreditSubmit.GetPosts" AutoEventWireup="True" CodeBehind="GetPosts.ascx.cs" %>

<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/labelControl.ascx" %>

<dnn:label id="lblStartDate" runat="server" class="Head"></dnn:label> <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
<br />
<dnn:label id="lblEndDate" runat="server" class="Head"></dnn:label> <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
<br />
<asp:LinkButton id="btnSubmit" runat="server" resourcekey="btnSubmit" 
    onclick="btnSubmit_Click" />


<asp:GridView ID="dgView" runat="server" AutoGenerateColumns="true">
<Columns>
    <asp:BoundField DataField="Name" />
</Columns>
</asp:GridView>