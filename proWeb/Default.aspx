<%@ Page Title="Products management" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="proWeb.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function onlyLetters(input) {
            input.value = input.value.replace(/[^A-Za-zÁÉÍÓÚáéíóúÑñÜü ]/g, '');
        }

        function onlyIntegers(input) {
            input.value = input.value.replace(/[^0-9]/g, '');
        }
    </script>

    <style type="text/css">
        .title {
            font-size: 26px;
            font-weight: bold;
            margin-bottom: 25px;
        }

        .form-table {
            border-collapse: collapse;
        }

        .form-table td {
            padding: 6px 10px;
            vertical-align: middle;
        }

        .form-table td:first-child {
            font-weight: bold;
            width: 110px;
        }

        .txt {
            width: 220px;
            padding: 4px 6px;
        }

        .ddl {
            width: 180px;
            padding: 4px 6px;
        }

        .btn-row {
            margin-top: 14px;
            margin-bottom: 12px;
        }

        .btn-row input[type="submit"] {
            margin-right: 6px;
            padding: 4px 10px;
        }

        .message {
            margin-top: 8px;
            font-weight: bold;
        }
    </style>

    <div class="title">Products management</div>

    <table class="form-table">
        <tr>
            <td>Code</td>
            <td>
                <asp:TextBox ID="txtCode" runat="server" CssClass="txt"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>Name</td>
            <td>
                <asp:TextBox ID="txtName" runat="server" CssClass="txt"
                    oninput="onlyLetters(this)"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>Amount</td>
            <td>
                <asp:TextBox ID="txtAmount" runat="server" CssClass="txt"
                    TextMode="Number" min="0" step="1"
                    oninput="onlyIntegers(this)"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>Category</td>
            <td>
                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="ddl"></asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>Price</td>
            <td>
                <asp:TextBox ID="txtPrice" runat="server" CssClass="txt"
                    TextMode="Number" min="0" step="1"
                    oninput="onlyIntegers(this)"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>Creation Date</td>
            <td>
                <asp:TextBox ID="txtCreationDate" runat="server" CssClass="txt"
                    TextMode="DateTimeLocal"></asp:TextBox>
            </td>
        </tr>
    </table>

    <div class="btn-row">
        <asp:Button ID="btnCreate" runat="server" Text="Create" OnClick="btnCreate_Click" />
        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
        <asp:Button ID="btnRead" runat="server" Text="Read" OnClick="btnRead_Click" />
        <asp:Button ID="btnReadFirst" runat="server" Text="Read First" OnClick="btnReadFirst_Click" />
        <asp:Button ID="btnReadPrev" runat="server" Text="Read Prev" OnClick="btnReadPrev_Click" />
        <asp:Button ID="btnReadNext" runat="server" Text="Read Next" OnClick="btnReadNext_Click" />
    </div>

    <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>

</asp:Content>