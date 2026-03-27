<%@ Page Title="Products management" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="proWeb.Default" %>

<%-- Estilos en el ContentPlaceHolder head de la página maestra --%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .title      { font-size: 26px; font-weight: bold; margin-bottom: 20px; }
        .form-table { border-collapse: collapse; }
        .form-table td { padding: 6px 10px; vertical-align: middle; }
        .form-table td:first-child { font-weight: bold; width: 120px; }
        .txt  { width: 220px; padding: 4px 6px; }
        .ddl  { width: 180px; padding: 4px 6px; }
        .btn-row { margin-top: 14px; margin-bottom: 12px; }
        .btn-row input[type="submit"] { margin-right: 6px; padding: 4px 10px; }
        .message { margin-top: 10px; font-weight: bold; }
    </style>
</asp:Content>

<%-- Contenido del formulario --%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="title">Products management</div>

    <table class="form-table">
        <tr>
            <td>Code</td>
            <td>
                <asp:TextBox ID="txtCode" runat="server"
                    CssClass="txt" MaxLength="16"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Name</td>
            <td>
                <asp:TextBox ID="txtName" runat="server"
                    CssClass="txt" MaxLength="32"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Amount</td>
            <td>
                <asp:TextBox ID="txtAmount" runat="server"
                    CssClass="txt" MaxLength="4"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Category</td>
            <td>
                <asp:DropDownList ID="ddlCategory" runat="server"
                    CssClass="ddl"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Price</td>
            <td>
                <asp:TextBox ID="txtPrice" runat="server"
                    CssClass="txt" MaxLength="7"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Creation Date</td>
            <td>
                <%-- Texto plano con formato dd/mm/aaaa hh:mm:ss según el enunciado --%>
                <asp:TextBox ID="txtCreationDate" runat="server"
                    CssClass="txt" MaxLength="19"
                    placeholder="dd/mm/aaaa hh:mm:ss"></asp:TextBox>
            </td>
        </tr>
    </table>

    <div class="btn-row">
        <asp:Button ID="btnCreate"    runat="server" Text="Create"     OnClick="btnCreate_Click" />
        <asp:Button ID="btnUpdate"    runat="server" Text="Update"     OnClick="btnUpdate_Click" />
        <asp:Button ID="btnDelete"    runat="server" Text="Delete"     OnClick="btnDelete_Click" />
        <asp:Button ID="btnRead"      runat="server" Text="Read"       OnClick="btnRead_Click" />
        <asp:Button ID="btnReadFirst" runat="server" Text="Read First" OnClick="btnReadFirst_Click" />
        <asp:Button ID="btnReadPrev"  runat="server" Text="Read Prev"  OnClick="btnReadPrev_Click" />
        <asp:Button ID="btnReadNext"  runat="server" Text="Read Next"  OnClick="btnReadNext_Click" />
    </div>

    <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>

</asp:Content>
