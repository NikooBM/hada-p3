<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs"
    Inherits="proWeb.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .form-table td { padding: 5px 8px; vertical-align: middle; }
        .form-table td:first-child { font-weight: bold; width: 110px; }
        .btn-row { margin-top: 10px; }
        .btn-row input[type="submit"] { margin-right: 4px; }
        .msg { margin-top: 12px; font-size: 13px; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2>Products management</h2>

    <table class="form-table">
        <tr>
            <td>Code</td>
            <td>
                <asp:TextBox ID="txtCode" runat="server"
                    MaxLength="16" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Name</td>
            <td>
                <asp:TextBox ID="txtName" runat="server"
                    MaxLength="32" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Amount</td>
            <td>
                <asp:TextBox ID="txtAmount" runat="server"
                    Width="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Category</td>
            <td>
                <asp:DropDownList ID="ddlCategory" runat="server"
                    Width="160px"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Price</td>
            <td>
                <asp:TextBox ID="txtPrice" runat="server"
                    Width="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Creation Date</td>
            <td>
                <asp:TextBox ID="txtCreationDate" runat="server"
                    Width="200px" placeholder="dd/mm/aaaa hh:mm:ss"></asp:TextBox>
            </td>
        </tr>
    </table>

    <div class="btn-row">
        <asp:Button ID="btnCreate"    runat="server" Text="Create"
            OnClick="btnCreate_Click" />
        <asp:Button ID="btnUpdate"    runat="server" Text="Update"
            OnClick="btnUpdate_Click" />
        <asp:Button ID="btnDelete"    runat="server" Text="Delete"
            OnClick="btnDelete_Click" />
        <asp:Button ID="btnRead"      runat="server" Text="Read"
            OnClick="btnRead_Click" />
        <asp:Button ID="btnReadFirst" runat="server" Text="Read First"
            OnClick="btnReadFirst_Click" />
        <asp:Button ID="btnReadPrev"  runat="server" Text="Read Prev"
            OnClick="btnReadPrev_Click" />
        <asp:Button ID="btnReadNext"  runat="server" Text="Read Next"
            OnClick="btnReadNext_Click" />
    </div>

    <div class="msg">
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    </div>

</asp:Content>
