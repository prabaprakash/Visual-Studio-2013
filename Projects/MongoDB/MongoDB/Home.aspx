<%@ Page Language="C#" MasterPageFile="Site1.Master" Async="true" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="MongoDB.Home" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="juice" Namespace="Juice" Assembly="JuiceUI, Version=1.1.1.0, Culture=neutral, PublicKeyToken=2ba9b13151115713" %>
<%@ Register Assembly="JuiceUI" Namespace="Juice" TagPrefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <script src="Scripts/jquery-1.8.3.js"></script>
    <script src="Scripts/jquery-ui-1.9.2.js"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">

    <div>
        <!-- script manager -->
        <form runat="server">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
            <!-- tab control -->
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">

                <asp:TabPanel runat="server" HeaderText="Search" ID="TabPanel2">
                    <ContentTemplate>
                        <div>
                            Enter Recipe Name .:
                        <asp:TextBox CausesValidation="False" ID="text1" OnTextChanged="TextChanged" runat="server"></asp:TextBox>
                            <asp:Button runat="server" Text="Search" CausesValidation="False" />
                            <asp:GridView ID="GridView1" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowPaging="True" PageSize="25" CellSpacing="2">

                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                <SortedDescendingHeaderStyle BackColor="#93451F" />
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" HeaderText="Add" ID="TabPanel1">
                    <ContentTemplate>

                        <div>
                            &nbsp;Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                <asp:TextBox ID="name" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="name" ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <br />
                            <br />
                            Ingridients&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="ingridients" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="ingridients" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <br />
                            <br />
                            Url&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                <asp:TextBox ID="url" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="url" ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <br />
                            <br />
                            Image Url&nbsp; &nbsp;
                <asp:TextBox ID="imageurl" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="imageurl" ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <br />
                            <br />
                            Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                <juice:Datepicker runat="server" ID="Datepicker1" TargetControlID="dates" DayNames="Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday" DayNamesMin="Su,Mo,Tu,We,Th,Fr,Sa" DayNamesShort="Sun,Mon,Tue,Wed,Thu,Fri,Sat" MonthNames="January,February,March,April,May,June,July,August,September,October,November,December" MonthNamesShort="Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec" />
                            <asp:TextBox ID="dates" runat="server" CssClass="DDTextBox" />
                            <asp:RequiredFieldValidator ControlToValidate="dates" ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <br />
                            <br />
                            Cook Time&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="cooktime" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="cooktime" ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <br />
                            <br />
                            Source&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="source" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="source" ID="RequiredFieldValidator7" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <br />
                            <br />
                            Recipe Yield&nbsp;
            <asp:TextBox ID="recipeyield" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="recipeyield" ID="RequiredFieldValidator8" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <br />
                            <br />
                            Date Published
                 <juice:Datepicker runat="server" ID="Datepicker3" TargetControlID="datepublished" DayNames="Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday" DayNamesMin="Su,Mo,Tu,We,Th,Fr,Sa" DayNamesShort="Sun,Mon,Tue,Wed,Thu,Fri,Sat" MonthNames="January,February,March,April,May,June,July,August,September,October,November,December" MonthNamesShort="Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec" />
                            <asp:TextBox ID="datepublished" runat="server" />
                            <asp:RequiredFieldValidator ControlToValidate="datepublished" ID="RequiredFieldValidator9" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <br />
                            <br />
                            Preparation time&nbsp;&nbsp;
            <asp:TextBox ID="preptime" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="preptime" ID="RequiredFieldValidator10" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <br />
                            <br />
                            Decription&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="description" runat="server"></asp:TextBox>

                            <asp:RequiredFieldValidator ControlToValidate="description" ID="RequiredFieldValidator11" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

                            <br />
                        </div>
                        <asp:Button runat="server" ID="ButtonTest" Text="Add" OnClick="ButtonTest_Click" />
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel3" runat="server" OnLoad="panelonload" HeaderText="Big Data Analytic">
                    <ContentTemplate>
                    
                        <asp:BarChart ID="BarChart1" runat="server"
                           
                            ChartHeight="1000" ChartWidth="1200" ChartType="Column"
                            ChartTitle="Total Number Recipe Per Preparation Time"
                            CategoriesAxis=""
                            ChartTitleColor="#0E426C" CategoryAxisLineColor="#D08AD9"
                            ValueAxisLineColor="#D08AD9" BaseLineColor="#A156AB">
                        
                        </asp:BarChart>
                       
                        <asp:GridView ID="GridView2" runat="server" AlternatingRowStyle-BackColor="#FFCCCC" BorderStyle="Groove">
                           
                        </asp:GridView>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </form>
    </div>
</asp:Content>
