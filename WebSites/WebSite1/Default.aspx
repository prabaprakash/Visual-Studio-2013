<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
   <%
       Dim a
       a=Integer.Parse("5")
       Dim b
       b=Integer.Parse("5")
       Dim c
       c=a+b
       Response.Write(c)
       
       %>
    </div>
    </form>
</body>
</html>
