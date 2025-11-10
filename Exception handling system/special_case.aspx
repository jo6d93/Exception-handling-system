<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="special_case.aspx.vb" Inherits="Exception_handling_system.special_case" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>進貨異常處理系統</title>

     <script src="jquery-1.3.2-vsdoc.js"></script>
    <script type="text/javascript">
        (function () {
            var div1 = $("#left");
            var div2 = $("#right");

            var h = GetMaxHeight(div1, div2);
            div1.css("height", h);
            div2.css("height", h);
        });
        function GetMaxHeight(div1, div2) {
            var h1 = $(div1).attr("offsetHeight");
            var h2 = $(div2).attr("offsetHeight");
            return Math.max(h1, h2);
        }
    </script>
 <style >
        body{background: url(Image/point.bmp) left top  repeat #ffffff ;}
      html {
          min-width :1050px;
          }
</style>
</head>   
<body>
    <form id="form1" runat="server" style="font-family:Meiryo ">      
             
        
        
        <asp:Panel ID="Panel3" runat="server" Height="102px">
 <div id="left" style=" float: left; width: 431px; overflow: hidden; height: 90px;">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/Denso_Logo_Tagline_Red_HiRes.png" Width="200px" />
     <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="25px" ForeColor="Black"  Text="進貨異常處理系統" Width="216px"  Height="27px"></asp:Label>
            
        </div>
        <div id="right" style=" margin-right :0px; float :right; ">
             <br />
             <br />
<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/1.bmp"  OnClick ="ImageButton1_Click"  Height="45px" Width="130px"/>           
<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/2.bmp" OnClick="ImageButton2_Click" Height="45px" Width="130px" />             
<asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/3.bmp" OnClick="ImageButton3_Click" Height="45px" Width="130px" />
<asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Image/4.bmp" OnClick="ImageButton4_Click"  Height="45px" Width="130px"/>   
            
        </div>                       
        </asp:Panel>   
      

        <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Right" Width="100%" >
   <asp:Table ID="Table4" runat="server" HorizontalAlign="Right">
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server"><asp:Label ID="user_name" runat="server" Text="" Font-Size="X-Large"></asp:Label>
</asp:TableCell>
                    <asp:TableCell runat="server"><asp:Label ID="Label4" runat="server" Text="您好:" Font-Size="X-Large"></asp:Label>
</asp:TableCell>
                    <asp:TableCell runat="server"><asp:LinkButton ID="LinkButton1" runat="server" Font-Size="X-Large" OnClick ="LinkButton1_Click" >登出</asp:LinkButton>
</asp:TableCell>
                </asp:TableRow>             
            </asp:Table>
        </asp:Panel>
         <br /> <br />
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">

    
        <asp:Label ID="Label1" runat="server" Text="供應商編號:"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Label ID="Label2" runat="server" Text="延遲時間(S):"></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="修改" />
             <br />
            <asp:Label ID="Label5" runat="server" Visible="False"></asp:Label>
             <br />



        <asp:GridView ID="GridView1"  runat="server" Width="60%" CellPadding="4" ForeColor="Black" GridLines="Horizontal" 
            HorizontalAlign="Center" AutoGenerateColumns="False"  BackColor="White" BorderColor="#333333" BorderStyle="Solid" BorderWidth="1px" >
            <Columns>
                <asp:TemplateField HeaderText="No" ItemStyle-Width="60px">
                    <ItemTemplate >
                        <asp:Label ID ="no" runat ="server" Text='<%#Eval("no")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                   <asp:TemplateField HeaderText="供應商" ItemStyle-Width="60px">
                    <ItemTemplate >
                        <asp:Label ID ="no" runat ="server" Text='<%#Eval("供應商")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="40px" HorizontalAlign="left" VerticalAlign="Middle" />
                </asp:TemplateField>
                   <asp:TemplateField HeaderText="供應商編號" ItemStyle-Width="60px">
                    <ItemTemplate >
                        <asp:Label ID ="no" runat ="server" Text='<%#Eval("供應商編號")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                   <asp:TemplateField HeaderText="延遲時間" ItemStyle-Width="60px">
                    <ItemTemplate >
                        <asp:Label ID ="no" runat ="server" Text='<%#Eval("handle_time")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                 </Columns>


            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
    </asp:Panel>
 
    </form>
</body>
</html>
